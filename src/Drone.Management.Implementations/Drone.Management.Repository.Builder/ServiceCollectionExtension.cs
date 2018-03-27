using Drone.Management.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Repository.Builder
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositoryBuilderServices(IServiceCollection services)
        {
            services.AddScoped<IRepositoryBuilder, RepositoryBuilder>();
        }
    }
}
