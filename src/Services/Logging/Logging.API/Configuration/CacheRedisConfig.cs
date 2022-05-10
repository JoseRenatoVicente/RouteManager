using Microsoft.Extensions.Configuration;

namespace Logging.API.Configuration;

public static class CacheRedisConfig
{
    public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Cache");
            options.InstanceName = "Logging.API";
        });
    }
}