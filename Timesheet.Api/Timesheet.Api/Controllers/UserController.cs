using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.Interfaces;
using Timesheet.Model;
using Timesheet.Model.APIModel;

namespace Timesheet.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(UserController));

        private readonly IUserLoginDP _userDP;
        private readonly IEmployeeDP _employeeDP;

        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        private readonly IUserService _userService;

        private readonly AppSettings _appSettings;

        public UserController(IUserLoginDP userDP,
                              IEmployeeDP employeeDP,
                              IHashService hashService,
                              ITokenService tokenService,
                              IUserService userService,
                              IOptions<AppSettings> appSettings)
        {
            _userDP = userDP;
            _employeeDP = employeeDP;
            _hashService = hashService;
            _tokenService = tokenService;
            _userService = userService;
            _appSettings = appSettings.Value;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest registrationRequest)
        {
            UserLogin registeredUser = await _userDP.GetUserByEmail(registrationRequest.Email)
                                                    .ConfigureAwait(false);

            if (registeredUser != null)
            {
                return Ok(new BaseResponse
                {
                    Success = false,
                    Message = "This user is already registered"
                });
            }

            await _userService.Register(registrationRequest)
                              .ConfigureAwait(false);

            return Ok(new BaseResponse
            {
                Success = true,
                Message = "Registration successful"
            });

        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();

            try
            {
                string cryptedPass = $"{loginRequest.Password}{_appSettings.PasswordSalt}";
                string hashPass = _hashService.GetMd5Hash(cryptedPass);

                Employee existingEmployee = await _employeeDP.GetEmployee(loginRequest.Email,
                                                                          hashPass)
                                                             .ConfigureAwait(false);

                if (existingEmployee == null)
                {
                    response.Success = false;
                    response.Message = "Invalid password";
                }

                response.Token = _tokenService.TokenCreate(loginRequest.Email,
                                                           existingEmployee.RoleId.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR in LoginController.Login(). Details {ex}");
            }
            return Ok(response);
        }

    }
}
