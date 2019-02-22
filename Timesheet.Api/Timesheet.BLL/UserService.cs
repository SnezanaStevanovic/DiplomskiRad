using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using Timesheet.BLL.Interfaces;
using Timesheet.Common;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL
{
    public class UserService : IUserService
    {

        private readonly ILogger<UserService> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeProjectService _employeeProjectService;

        private readonly IUserLoginDP _userLoginDP;
        private readonly IEmployeeDP _employeeDP;

        public UserService(IOptions<AppSettings> appSettings,
                           IHashService hashService,
                           ITokenService tokenService,
                           IEmployeeService employeeService,
                           IEmployeeProjectService employeeProjectService,
                           IUserLoginDP userLoginDP,
                           IEmployeeDP employeeDP, ILogger<UserService> logger)
        {
            _appSettings = appSettings.Value;
            _hashService = hashService;
            _tokenService = tokenService;
            _employeeService = employeeService;
            _employeeProjectService = employeeProjectService;
            _userLoginDP = userLoginDP;
            _employeeDP = employeeDP;
            _logger = logger;
        }



        public async Task<BaseResponse> Register(RegistrationRequest registrationRequest)
        {
            BaseResponse baseResponse = new BaseResponse();

            try
            {
                UserLogin registeredUser = await _userLoginDP.GetUserByEmailAsync(registrationRequest.NewUser.Email)
                                                             .ConfigureAwait(false);

                if (registeredUser != null)
                {
                    baseResponse.Success = false;
                    baseResponse.Message = "This user is already registered";
                    return baseResponse;
                }

                string cryptedPass = $"{registrationRequest.NewUser.Password}{_appSettings.PasswordSalt}";
                string hashPass = _hashService.GetMd5Hash(cryptedPass);

                registeredUser = new UserLogin
                {
                    Email = registrationRequest.NewUser.Email,
                    Password = hashPass,
                };


                using (TransactionScope transaction = TransactionScopeCreator.Create())
                {
                    await this._userLoginDP.InsertAsync(registeredUser)
                        .ConfigureAwait(false);

                    Employee newEmployee = new Employee
                    {
                        FirstName = registrationRequest.NewEmployee.FirstName,
                        LastName = registrationRequest.NewEmployee.LastName,
                        Adress = registrationRequest.NewEmployee.Adress,
                        DateOfBirth = registrationRequest.NewEmployee.DateOfBirth,
                        Gender = registrationRequest.NewEmployee.Gender,
                        Role = registrationRequest.NewEmployee.Role,
                        UserId = registeredUser.Id
                    };

                    await _employeeService.AddNewAsync(newEmployee)
                                          .ConfigureAwait(false);

                    foreach (int projectId in registrationRequest.ProjectIds)
                    {
                        await _employeeProjectService.AddNewAsync(newEmployee.Id,
                                                                  projectId)
                                                     .ConfigureAwait(false);
                    }

                    transaction.Complete();
                }

 
                baseResponse.Success = true;
                baseResponse.Message = "Registration successful";

                return baseResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserService)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

        }

       
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();

            try
            {
                string cryptedPass = $"{loginRequest.Password}{_appSettings.PasswordSalt}";
                string hashPass = _hashService.GetMd5Hash(cryptedPass);

                Employee existingEmployee = await _employeeDP.GetEmployeeAsync(loginRequest.Email,
                                                                               hashPass)
                                                             .ConfigureAwait(false);

                if (existingEmployee == null)
                {
                    loginResponse.Success = false;
                    loginResponse.Message = "Invalid password";
                }

                loginResponse.Success = true;
                loginResponse.Message = "Login successful";
                loginResponse.Token = _tokenService.TokenCreate(loginRequest.Email,
                                                                existingEmployee.Role,
                                                                existingEmployee.Id);
            }
            catch (Exception ex)
            {
                loginResponse.Success = false;
                loginResponse.Message = "Login failed";

                _logger.LogError(ex, $"{nameof(UserService)}.{MethodBase.GetCurrentMethod().Name}");
            }

            return loginResponse;
        }
    }
}
