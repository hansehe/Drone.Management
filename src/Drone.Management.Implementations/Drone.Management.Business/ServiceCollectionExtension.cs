using Drone.Management.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Business
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IDroneBusiness, DroneBusiness>();
        }
    }
}
