using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Services;
using System;
using Teams.API.Services;
using Teams.Domain.Contracts.v1;
using Teams.Infra.Data.Repositories.v1;

namespace Teams.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton(new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

        //services
        services.AddHttpClient<GatewayService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IPersonService, PersonService>();

        //repositories
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        
        //Identity
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();

    }
}