using FluentValidation;
using Identity.API.Services;
using Identity.Domain.Contracts.v1;
using Identity.Infra.Data.Repositories.v1;
using MediatR;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Pipelines;
using RouteManager.WebAPI.Core.Notifications;
using System;

namespace Identity.API.Configurations;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddAutoMapper(assemblies)
            .AddMediatR(assemblies);

        services.AddSingleton(new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

        //services
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRoleService, RoleService>();
        services.AddSingleton<AuthService>();
        services.AddSingleton<SeederService>();

        //repositories
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IRoleRepository, RoleRepository>();

        //identity
        services.AddSingleton<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();

        //notification
        services.AddSingleton<INotifier, Notifier>();

    }
}