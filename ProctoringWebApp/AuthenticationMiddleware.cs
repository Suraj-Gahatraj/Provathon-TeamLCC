using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProctoringWebApp
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public Task Invoke(HttpContext http)
        {
            var path = http.Request.Path;

            if (path.HasValue && !path.Value.StartsWith("/Account"))
            {
                if(http.Session.GetString("userId") == null)
                {
                    http.Response.Redirect("/Account/Login");
                }
            }
            return _next(http);
        }

    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
