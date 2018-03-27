using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.TestData
{
    public static class ServiceCollectionExtension
    {
        public static void AddAdHocTestDataServices(IServiceCollection services)
        {
            services.AddSingleton<ITestData, TestData>();
        }
    }
}
