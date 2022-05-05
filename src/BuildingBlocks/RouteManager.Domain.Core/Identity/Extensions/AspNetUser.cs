using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace RouteManager.Domain.Core.Identity.Extensions;

public class AspNetUser : IAspNetUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public string Name => _accessor.HttpContext?.User.Identity?.Name;

    public string GetUserId()
    {
        return IsAuthenticated() ? _accessor.HttpContext?.User.GetUserId() : string.Empty;
    }

    public string GetName()
    {
        return IsAuthenticated() ? _accessor.HttpContext?.User.GetName() : "";
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext?.User.GetUserEmail() : "";
    }

    public string GetToken()
    {
        if (IsAuthenticated())
        {
            return _accessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var token) ?
                 token.ToString().Split(" ")[1] : _accessor.HttpContext.User.GetToken();
        }
        return string.Empty;
    }

    public string GetUserRefreshToken()
    {
        return IsAuthenticated() ? _accessor.HttpContext?.User.GetUserRefreshToken() : "";
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext!.User.Identity!.IsAuthenticated;
    }

    public bool HasRole(string role)
    {
        return _accessor.HttpContext!.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetClaims()
    {
        return _accessor.HttpContext!.User.Claims;
    }

    public HttpContext GetHttpContext()
    {
        return _accessor.HttpContext;
    }
}
