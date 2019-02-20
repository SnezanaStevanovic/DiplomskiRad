using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;


        private readonly IUserService _userService;


        public UserController(IUserService userService,ILogger<UserController> logger)
        {

            _userService = userService;

            _logger = logger;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest registrationRequest)
        {
            try
            {
                BaseResponse response = await _userService.Register(registrationRequest)
                                          .ConfigureAwait(false);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)}.{MethodBase.GetCurrentMethod().Name}");
                BaseResponse errorResponse = new BaseResponse();
                errorResponse.Success = false;
                errorResponse.Message = "Registration failed";
                return Ok(errorResponse);

            }
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            LoginResponse response = await _userService.Login(loginRequest);
          
            return Ok(response);
        }

    }
}
