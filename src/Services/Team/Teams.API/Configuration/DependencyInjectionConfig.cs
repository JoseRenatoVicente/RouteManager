using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.Domain.Core.Services;
using RouteManager.WebAPI.Core.Notifications;
using System;
using Teams.API.Services;
using Teams.Domain.Contracts.v1;
using Teams.Infra.Data.Repositories.v1;

namespace Teams.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(s =>
            new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

            //services
            services.AddHttpClient<GatewayService>();
            services.AddSingleton<ICityService, CityService>();
            services.AddSingleton<ITeamService, TeamService>();
            services.AddSingleton<IPersonService, PersonService>();

            //repositories
            services.AddSingleton<ICityRepository, CityRepository>();
            services.AddSingleton<ITeamRepository, TeamRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

            //Identity
            services.AddSingleton<IAspNetUser, AspNetUser>();
            services.AddHttpContextAccessor();

        }
    }
}