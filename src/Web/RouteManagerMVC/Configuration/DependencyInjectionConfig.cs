using Microsoft.Extensions.DependencyInjection;
using RouteManager.Domain.Identity.Extensions;
using RouteManager.Domain.Services;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Services;
using System;

namespace RouteManagerMVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services
            services.AddHttpClient<GatewayService>();
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddSingleton<ICityService, CityService>();
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<ITeamService, TeamService>();
            services.AddSingleton<IRouteService, RouteService>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

            //Identity
            services.AddSingleton<IAspNetUser, AspNetUser>();
            services.AddHttpContextAccessor();           

        }
    }
}