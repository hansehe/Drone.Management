using System;
using System.Threading.Tasks;
using Drone.Management.Factory;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Migrator
{
    internal static class Startup
    {
        internal static IServiceProvider GetConfiguredServiceProvider()
        {
            var services = new ServiceCollection();
            var serviceProvider = ConfigureAndBuildServices(services);
            return serviceProvider;
        }

        internal static async Task ExecuteMigration(IServiceProvider serviceProvider)
        {
            var settings = serviceProvider.GetService<ISettings>();
            var migratorBuilder = serviceProvider.GetService<IMigratorBuilder>();
            await SafeMigration.ExecuteMigration(migratorBuilder, settings);
        }

        private static IServiceProvider ConfigureAndBuildServices(IServiceCollection services)
        {
            ServiceCollectionFactory.AddMigratorServiceCollection(services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}