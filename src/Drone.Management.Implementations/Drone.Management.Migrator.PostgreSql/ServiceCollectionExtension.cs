using Drone.Management.Migrator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Migrator.PostgreSql
{
    public static class ServiceCollectionExtension
    {
        public static void AddPostgeSqlMigratorServices(IServiceCollection services)
        {
            services.AddScoped<IMigratorResolver, PostgreSqlMigrator>();
        }
    }
}
