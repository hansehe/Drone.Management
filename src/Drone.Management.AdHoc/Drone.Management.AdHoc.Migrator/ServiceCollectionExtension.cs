using Drone.Management.AdHoc.Migrator.TestDataMigrators;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.Migrator
{
    public static class ServiceCollectionExtension
    {
        public static void AddAdHocMigratorServices(IServiceCollection services)
        {
            services.AddScoped<ITestDataMigratorManager, TestDataMigratorManager>();
            services.AddScoped<ITestDataMigrator, DroneTestDataMigrator>();
            services.AddScoped<ITestDataMigrator, DroneStatusTestDataMigrator>();
        }
    }
}
