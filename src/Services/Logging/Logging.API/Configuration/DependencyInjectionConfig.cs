using Logging.API.Services;
using Logging.Domain.Contracts.v1;
using Logging.Infra.Data.Repositories.v1;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RouteManager.Domain.Core.Identity.Extensions;
using System;

namespace Logging.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton(new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

        //services
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        //repositories
        services.AddScoped<ILogRepository, LogRepository>();
        
        //Identity
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();

    }
}