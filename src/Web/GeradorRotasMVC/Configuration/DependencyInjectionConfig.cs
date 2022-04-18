using GeradorRotas.Application.Services;
using GeradorRotas.Application.Services.Interfaces;
using GeradorRotas.Infrastructure.Repository;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeradorRotasMVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IEquipeService, EquipeService>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IRotaService, RotaService>();

            //repositories
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IEquipeRepository, EquipeRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IRotaRepository, RotaRepository>();


        }
    }
}