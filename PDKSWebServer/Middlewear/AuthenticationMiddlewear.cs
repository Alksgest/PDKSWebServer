using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PdksBuisness.Dtos;

namespace PDKSWebServer.Middlewear
{
    public class AuthenticationMiddlewear
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<AuthenticationMiddlewear> _logger;

        public AuthenticationMiddlewear(RequestDelegate next, ILogger<AuthenticationMiddlewear> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["authToken"].ToString();
            _logger.LogDebug("In Auth middlewear");
            if (token != null)
            {
                var authToken = JsonConvert.DeserializeObject<AuthToken>(token);
                Console.WriteLine(authToken);
            }

            await _next.Invoke(context);
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
