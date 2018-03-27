using Drone.Management.Migrator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Migrator.Builder
{
    public static class ServiceCollectionExtension
    {
        public static void AddMigratorBuilderServices(IServiceCollection services)
        {
            services.AddScoped<IMigratorBuilder, MigratorBuilder>();
        }
    }
}
