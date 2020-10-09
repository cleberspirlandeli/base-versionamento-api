using DevIO.Business.Intefaces;
using DevIO.Business.Services;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();

            // Service
            services.AddScoped<IFornecedorService, FornecedorService>();

            // Repository
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            return services;
        }
    }
}
