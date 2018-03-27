using Drone.Management.Migrator.Commons.MigratorTools;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Migrator.Commons
{
    public static class ServiceCollectionExtension
    {
        public static void AddCommonMigratorServices(IServiceCollection services)
        {
            services.AddScoped<IMigratorToolResolver, MigratorToolResolver>();
            services.AddScoped<IMigratorTool, DbUpPostgreSqlMigrator>();
        }
    }
}
