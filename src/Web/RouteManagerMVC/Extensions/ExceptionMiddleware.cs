using Microsoft.AspNetCore.Http;
using RouteManager.Domain.Core.Extensions;
using RouteManagerMVC.Services;
using System.Net;
using System.Threading.Tasks;

namespace RouteManagerMVC.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private IAuthService _authService;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAuthService authService)
    {
        _authService = authService;

        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            await HandleRequestExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException httpRequestException)
    {
        if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _authService.LogoutAsync();
            context.Response.Redirect($"/Auth/Login?ReturnUrl={context.Request.Path}");
            return;
        }

        context.Response.StatusCode = (int)httpRequestException.StatusCode;
    }
}