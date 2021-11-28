using AutoMapper;
using Contracts;
using Logistics.Extensions;
using Logistics.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Logistics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            LogManager.LoadConfiguration(GetNlogConfigPath());
            ServiceProvider = services.BuildServiceProvider();
        }

        private string GetNlogConfigPath()
        {
            return Directory.GetCurrentDirectory() + "\\nLog.config";
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.ConfigureSqlContext();
            services.ConfigureLoggerService();
            services.ConfigureRepositoryManager();
            services.AddAutoMapper( c => c.AddProfile(new MappingProfile()));
            services.AddSingleton<ITickTimer, TickTimer>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<Configuration>();
        }
    }
}
