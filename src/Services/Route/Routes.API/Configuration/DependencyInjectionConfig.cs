using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Services;
using Routes.API.Services;
using Routes.Domain.Contracts.v1;
using Routes.Infra.Data.Repositories.v1;
using System;

namespace Routes.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton(new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

        //services
        services.AddHttpClient<GatewayService>();

        //repositories
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<IExcelFileRepository, ExcelFileRepository>();
        services.AddScoped<IExcelFileService, ExcelFileService>();
        
        //Identity
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();

    }
}