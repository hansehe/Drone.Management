using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Repository.PostgreSql
{
    public static class ServiceCollectionExtension
    {
        public static void AddPostgeSqlRepositoryServices(IServiceCollection services)
        {
            {
                services.AddScoped<IDroneRepository, PostgreSqlDroneRepository>();
            }
        }
    }
}
