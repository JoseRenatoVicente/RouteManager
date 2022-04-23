using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace RouteManagerMVC.Configuration
{
    public class ExceptionMiddleware
    {
        private static IAuthenticationService _authService;

        public async Task InvokeAsync(HttpContext httpContext, IAuthenticationService authService)
        {
            _authService = authService;

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                await _authService.SignOutAsync(
                    httpContext,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    null);

                httpContext.Response.Redirect($"/Auth/Login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

        }
    }
}
