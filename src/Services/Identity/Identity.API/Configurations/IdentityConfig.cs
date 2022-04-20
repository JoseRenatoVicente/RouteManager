using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RouteManager.WebAPI.Core.Configuration;
using RouteManager.WebAPI.Core.Identity;

namespace Identity.API.Configurations
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJwtConfiguration(configuration);

        }
    }
}