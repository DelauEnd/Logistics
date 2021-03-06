using Contracts;
using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Logistics.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
            => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services)
            => services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(ConfigurationManager.
    ConnectionStrings["cs"].ConnectionString), ServiceLifetime.Transient);

        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddTransient<IRepositoryManager, RepositoryManager>();
    }
}
