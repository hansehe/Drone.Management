using Drone.Management.AdHoc.APIClient.BusinessClients;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.APIClient
{
    public static class ServiceCollectionExtension
    {
        public static void AddAdHocAPIClientServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
            services.AddScoped<IBusinessClient, BusinessClient>();
            services.AddScoped<IDroneBusinessClient, DroneBusinessClient>();
            services.AddScoped<IDroneStatusBusinessClient, DroneStatusBusinessClient>();
        }
    }
}
