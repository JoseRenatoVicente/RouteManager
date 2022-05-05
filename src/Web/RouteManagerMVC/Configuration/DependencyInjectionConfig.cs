using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Services;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Services;
using System;

namespace RouteManagerMVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        //services
        services.AddHttpClient<GatewayService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<ICityService, CityService>();
        services.AddSingleton<IPersonService, PersonService>();
        services.AddSingleton<ITeamService, TeamService>();
        services.AddSingleton<IReportRouteService, ReportRouteService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRoleService, RoleService>();
        services.AddSingleton<IAccountService, AccountService>();

        //notification
        services.AddSingleton<INotifier, Notifier>();

        //Identity
        services.AddSingleton<IAspNetUser, AspNetUser>();
        services.AddHttpContextAccessor();

    }
}