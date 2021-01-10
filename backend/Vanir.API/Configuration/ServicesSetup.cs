using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vanir.API.Configuration.Services;

namespace Vanir.API.Configuration
{
    public class ServicesSetup
    {
        public static void ConfigureServicesFromAssembly(IServiceCollection services, IConfiguration configuration)
        {
            var servicesConfigurators = typeof(Startup).Assembly.ExportedTypes
                .Where(type => typeof(IServicesConfigurator)
                .IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServicesConfigurator>()
                .ToList();

            servicesConfigurators.ForEach(config => config.ConfigureServices(services, configuration));
        }
    }
}