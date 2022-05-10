using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RouteManager.Domain.Core.Pipelines;
using RouteManager.WebAPI.Core.Notifications;
using System;

namespace RouteManager.Domain.Core;

public static class Bootstrapper
{
    public static IServiceCollection AddDomainContext(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        return services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped<INotifier, Notifier>()
            .AddAutoMapper(assemblies)
            .AddMediatR(assemblies);
    }
}