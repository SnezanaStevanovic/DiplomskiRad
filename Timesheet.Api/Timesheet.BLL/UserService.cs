using log4net;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.BLL
{
    public class UserService : IUserService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(UserService));

        private readonly AppSettings _appSettings;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        private readonly IUserLoginDP _userLoginDP;
        private readonly IEmployeeDP _employeeDP;

        public UserService(IOptions<AppSettings> appSettings,
                           IHashService hashService,
                           ITokenService tokenService,
                           IUserLoginDP userLoginDP,
                           IEmployeeDP employeeDP)
        {
            _appSettings = appSettings.Value;
            _hashService = hashService;
            _tokenService = tokenService;
            _userLoginDP = userLoginDP;
            _employeeDP = employeeDP;
        }
        public async Task<BaseResponse> Register(RegistrationRequest registrationRequest)
        {
            BaseResponse baseResponse = new BaseResponse();

            try
            {
                UserLogin registeredUser = await _userLoginDP.GetUserByEmail(registrationRequest.Email)
                                                             .ConfigureAwait(false);

                if (registeredUser != null)
                {
                    baseResponse.Success = false;
                    baseResponse.Message = "This user is already registered";
                    return baseResponse;
                }

                string cryptedPass = $"{registrationRequest.Password}{_appSettings.PasswordSalt}";
                string hashPass = _hashService.GetMd5Hash(cryptedPass);

                registeredUser = new UserLogin
                {
                    Email = registrationRequest.Email,
                    Password = hashPass,
                };

                await this._userLoginDP.Insert(registeredUser)
                                       .ConfigureAwait(false);
                foreach(int projectId in registrationRequest.ProjectIds)
                {
                    Employee newEmployee = new Employee
                    {
                        FirstName = registrationRequest.FirstName,
                        LastName = registrationRequest.LastName,
                        Adress = registrationRequest.Adress,
                        DateOfBirth = registrationRequest.DateOfBirth,
                        Gender = registrationRequest.Gender,
                        Role = registrationRequest.Role,
                        UserId = registeredUser.Id,
                        ProjectId = projectId
                    };

                    await this._employeeDP.Insert(newEmployee)
                                          .ConfigureAwait(false);
                }
                


                baseResponse.Success = true;
                baseResponse.Message = "Registration successful";
                return baseResponse;

            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: {ex.StackTrace}");
                baseResponse.Success = false;
                baseResponse.Message = "Registration failed";
                return baseResponse; 
            }

           
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();

            try
            {
                string cryptedPass = $"{loginRequest.Password}{_appSettings.PasswordSalt}";
                string hashPass = _hashService.GetMd5Hash(cryptedPass);

                Employee existingEmployee = await _employeeDP.GetEmployee(loginRequest.Email,
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
                                                                existingEmployee.Role.ToString());
            }
            catch (Exception ex)
            {
                loginResponse.Success = false;
                loginResponse.Message = "Login failed";

                Logger.Error($"ERROR: UserService.Login() Details: {ex}");
            }

            return loginResponse;
        }
    }
}
