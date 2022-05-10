using Identity.API.Services;
using Identity.Domain.Contracts.v1;
using Identity.Infra.Data.Repositories.v1;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Services;
using System;

namespace Identity.API.Configurations;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton(new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

        //services
        services.AddHttpClient<GatewayService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<AuthService>();
        services.AddScoped<SeederService>();

        //repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        
        //identity
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();
    }
}