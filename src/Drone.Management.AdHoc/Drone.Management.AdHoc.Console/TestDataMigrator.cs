using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.Migrator;
using Drone.Management.Migrator;
using Drone.Management.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.Console
{
    internal static class TestDataMigrator
    {
        internal static async Task ExecuteTestDataMigrationAsync(IServiceProvider serviceProvider)
        {
            var settings = serviceProvider.GetService<ISettings>();
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
    }
}
