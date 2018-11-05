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
        private readonly IUserLoginDP _userLoginDP;
        private readonly IEmployeeDP _employeeDP;

        public UserService(IOptions<AppSettings> appSettings,
                           IHashService hashService,
                           IUserLoginDP userLoginDP,
                           IEmployeeDP employeeDP)
        {
            _appSettings = appSettings.Value;
            _hashService = hashService;
            _userLoginDP = userLoginDP;
            _employeeDP = employeeDP;
        }
        public async Task Register(RegistrationRequest registrationRequest)
        {
            try
            {
                string cryptedPass = $"{registrationRequest.Password}{_appSettings.PasswordSalt}";
                string hashPass = _hashService.GetMd5Hash(cryptedPass);

                UserLogin registeredUser = new UserLogin
                {
                    Email = registrationRequest.Email,
                    Password = hashPass,
                };

                await this._userLoginDP.Insert(registeredUser)
                                       .ConfigureAwait(false);

                Employee newEmployee = new Employee
                {
                    FirstName = registrationRequest.FirstName,
                    LastName = registrationRequest.LastName,
                    Adress = registrationRequest.Adress,
                    DateOfBirth = registrationRequest.DateOfBirth,
                    Gender = registrationRequest.Gender,
                    RoleId = registrationRequest.RoleId,
                    UserId = registeredUser.Id,
                    ProjectId = registrationRequest.ProjectId
                };

                await this._employeeDP.Insert(newEmployee)
                                      .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: {ex.StackTrace}");
            }
        }

    }
}
