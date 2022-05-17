using Microsoft.Extensions.Configuration;
using Polly;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Services;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Services;
using System;

namespace RouteManagerMVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        //services
        services
            .AddHttpClient<GatewayService>()
            .ConfigureHttpClient(configure => configure.BaseAddress = new Uri(configuration["UrlGateway"]))
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));


        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IReportRouteService, ReportRouteService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAccountService, AccountService>();

        //notification
        services.AddScoped<INotifier, Notifier>();

        //Identity
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();

    }
}