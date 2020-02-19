using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PdksBuisness.Exceptions;
using PdksBuisness.Managers;
using PdksPersistence.Models;

namespace PDKSWebServer.Middlewear
{
    public class AuthenticationMiddlewear
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<AuthenticationMiddlewear> _logger;
        private readonly IAuthorizationManager _manager;

        public AuthenticationMiddlewear(RequestDelegate next, ILogger<AuthenticationMiddlewear> logger)
        {
            _next = next;
            _logger = logger;
            _manager = new AuthorizationManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            var request = context.Request;

            if (token != null)
            {
                UserRole role = UserRole.NotAuthorized;
                try
                {
                    var res = _manager.AllowAction(token, request.Method, ref role);
                    if (res.Item2)
                    {
                        context.Request.Headers.Add("Permissions", role.ToString());
                        context.Response.Headers.Add("Authorization", res.Item1);
                        await _next.Invoke(context);
                    }
                }
                catch(AuthorizationIsNeededException e)
                {
                    await _next.Invoke(context);
                }
            }
        }
    }

    public static class AuthenticationExtensions
    {
        public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddlewear>();
        }
    }
}
