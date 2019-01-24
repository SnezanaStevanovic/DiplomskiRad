using log4net;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timesheet.BLL.Interfaces;
using Timesheet.Model;

namespace Timesheet.BLL
{
    public class TokenService : ITokenService
    {
        private ILog Logger { get; } = LogManager.GetLogger(typeof(TokenService));

        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string TokenCreate(string email, string role)
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
                        new Claim (ClaimTypes.Role, role),
                        new Claim (ClaimTypes.Name, email)

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                retValToken = tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: Error in TokenService.TokenCreate: {ex}");
                throw;
            }

            return retValToken;
        }
    }
}
