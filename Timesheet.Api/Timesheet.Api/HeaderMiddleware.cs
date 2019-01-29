using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Timesheet.BLL.Interfaces;
using Timesheet.Model;

namespace Timesheet.Api
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ITokenService _tokenService;

        public HeaderMiddleware(RequestDelegate next,
                                ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var authenticateInfo = await httpContext.AuthenticateAsync("Bearer");

            ClaimsPrincipal bearerTokenIdentity = authenticateInfo?.Principal;

            if (bearerTokenIdentity != null)
            {
               
                    string userEmail = bearerTokenIdentity.FindFirst(ClaimTypes.Name).Value;
                    string roleIdentifier = bearerTokenIdentity.FindFirst(ClaimTypes.Role).Value;

                    Role role = (Role)Enum.Parse(typeof(Role), roleIdentifier);

                    int employeeId = Convert.ToInt32(bearerTokenIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    string token = _tokenService.TokenCreate(userEmail,
                                                             role,
                                                             employeeId);

                httpContext.Response.OnStarting((state) =>
                {
                    
                     httpContext.Response.Headers.Add("access-control-expose-headers" ,"access-token");

                    httpContext.Response.Headers.Add("access-token",token);

                    return Task.FromResult(0);
                }, null);                     
            }

            await _next(httpContext);
        }
    }

}
