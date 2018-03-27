using System;
using Drone.Management.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.APIClientConsole
{
    internal static class Startup
    {
        internal static IServiceProvider GetConfiguredServiceProvider()
        {
            var services = new ServiceCollection();
            var serviceProvider = ConfigureAndBuildServices(services);
            AutoMapperInit.AutoMapDtoModels();
            return serviceProvider;
        }

        private static IServiceProvider ConfigureAndBuildServices(IServiceCollection services)
        {
            APIClient.ServiceCollectionExtension.AddAdHocAPIClientServices(services);
            TestData.ServiceCollectionExtension.AddAdHocTestDataServices(services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
