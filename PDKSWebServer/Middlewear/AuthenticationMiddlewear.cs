using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using PdksBuisness.Dtos;
using PdksBuisness.Managers;

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

            if (token != null)
            {
                var res = _manager.AllowAction(token);
                if (res != null)
                {
                    context.Response.Headers.Add("Authorization", res.Item1);
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
