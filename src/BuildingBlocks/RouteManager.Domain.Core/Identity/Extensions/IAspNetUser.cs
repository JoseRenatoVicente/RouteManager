using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace RouteManager.Domain.Core.Identity.Extensions
{
    public interface IAspNetUser
    {
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
        string GetUserEmail();
        string GetUserId();
        string GetName();
        string GetUserRefreshToken();
        string GetToken();
        bool HasRole(string role);
        bool IsAuthenticated();
    }
}