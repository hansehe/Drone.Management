using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.Migrator;
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
            await ExecuteDbMigration(serviceProvider);
            await ExecuteTestDataMigration(serviceProvider);
        }

        private static async Task ExecuteDbMigration(IServiceProvider serviceProvider)
        {
            var settings = GetSettings(serviceProvider);
            var migratorBuilder = serviceProvider.GetService<IMigratorBuilder>();
            await SafeMigration.ExecuteMigration(migratorBuilder, settings);
        }

        private static async Task ExecuteTestDataMigration(IServiceProvider serviceProvider)
        {
            if (Environment.GetEnvironmentVariable("MIGRATE_TEST_DATA") != "TRUE")
            {
                return;
            }

            var settings = GetSettings(serviceProvider);
            var migratorBuilder = serviceProvider.GetService<ITestDataMigratorManager>();

            if (long.TryParse(Environment.GetEnvironmentVariable("N_TEST_DATAS"), out var nDatas))
            {
                migratorBuilder.NumberOfDataSets = nDatas;
            }

            if (Environment.GetEnvironmentVariable("VERIFY_MIGRATED_TEST_DATA") != "TRUE")
            {
                migratorBuilder.VerifyMigratedTestData = false;
            }

            if (Environment.GetEnvironmentVariable("DELETE_MIGRATED_TEST_DATA") == "TRUE")
            {
                migratorBuilder.DeleteMigratedTestData = true;
            }

            await SafeMigration.ExecuteMigration(migratorBuilder, settings);
        }

        private static IServiceProvider ConfigureAndBuildServices(IServiceCollection services)
        {
            ServiceCollectionFactory.AddMigratorServiceCollection(services);
            AddAdHocServices(services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void AddAdHocServices(IServiceCollection services)
        {
            AdHoc.Migrator.ServiceCollectionExtension.AddAdHocMigratorServices(services);
            AdHoc.TestData.ServiceCollectionExtension.AddAdHocTestDataServices(services);
        }

        private static ISettings GetSettings(IServiceProvider serviceProvider)
        {
            var settings = serviceProvider.GetService<ISettings>();
            return settings;
        }
    }
}