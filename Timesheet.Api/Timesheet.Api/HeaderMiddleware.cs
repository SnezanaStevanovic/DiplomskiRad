using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Timesheet.BLL.Interfaces;

namespace Timesheet.Api
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
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

        public Task Invoke(HttpContext httpContext)
        {
            string authHeader = (string)httpContext.Request.Headers["access-token"];

            if(!string.IsNullOrEmpty(authHeader))
            {
                string userEmail = String.Empty;
                string roleId = String.Empty;

                //var tokenFromHeader = httpContext.GetTokenAsync("access-token")
                //                                 .ConfigureAwait(false);

                var identity = httpContext.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    userEmail = identity.FindFirst(ClaimTypes.Name).Value;
                    roleId = identity.FindFirst(ClaimTypes.Role).Value;
                }

                string token = _tokenService.TokenCreate(userEmail,
                                                         roleId);
                httpContext.Response
                           .Headers
                           .Add("access-token",
                                 token);

            }

            return _next(httpContext);
        }
    }

}
