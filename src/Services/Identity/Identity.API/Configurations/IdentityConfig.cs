using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RouteManager.WebAPI.Core.Configuration;

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