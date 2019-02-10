using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Timesheet.BLL.Interfaces;
using Timesheet.Model;

namespace Timesheet.BLL
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings, ILogger<TokenService> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public string TokenCreate(string email, Role role, int employeeId)
        {
            string retValToken = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim (ClaimTypes.Role, role.ToString()),
                        new Claim (ClaimTypes.Name, email),
                        new Claim (ClaimTypes.NameIdentifier, employeeId.ToString())

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                retValToken = tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TokenService)}.{MethodBase.GetCurrentMethod().Name}");
                throw;
            }

            
            return retValToken;
        }
    }
}
