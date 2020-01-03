using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PdksBuisness.Dtos;

namespace PDKSWebServer.Middlewear
{
    public class AuthenticationMiddlewear
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddlewear(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["authToken"].ToString();
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
