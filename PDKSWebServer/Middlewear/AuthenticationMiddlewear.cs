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
            var token = context.Request.Headers["Auth-Token"].ToString();

            var request = context.Request;
            var requestMethod = request.Method;

            if (token != null)
            {
                UserRole role = UserRole.NotAuthorized;
                try
                {
                    var res = _manager.AllowAction(token, requestMethod, ref role);
                    if (res.Item2)
                    {
                        context.Request.Headers.Add("Permissions", role.ToString());
                        context.Request.Headers.Add("Authorized", "true");
                        context.Response.Headers.Add("Auth-Token", res.Item1);
                        await _next.Invoke(context);
                    }
                }
                catch(AuthorizationIsNeededException)
                {
                    context.Request.Headers.Add("Authorized", "false");
                    await _next.Invoke(context);
                }
            }
            else
            {
                context.Request.Headers.Add("Authorized", "false");
                context.Request.Headers.Add("Permissions", UserRole.NotAuthorized.ToString());
                await _next.Invoke(context);
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
