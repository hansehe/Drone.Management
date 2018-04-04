using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.Console
{
    internal static class APIClientExecutor
    {
        private const string DefaultBaseAddress = "http://localhost:9718/";

        private const long DefaultNumberOfTestDatas = 10;

        internal static async Task ExecuteAPIClientsAsync(IServiceProvider serviceProvider)
        {
            var numberOfTestDatas = DefaultNumberOfTestDatas;
            if (long.TryParse(Environment.GetEnvironmentVariable("N_TEST_DATAS"), out var nDatas))
            {
                numberOfTestDatas = nDatas;
            }
            var envBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS");
            var baseAddress = new Uri(envBaseAddress ?? DefaultBaseAddress);

            var apiClientManager = serviceProvider.GetService<IAPIClientManager>();
            await apiClientManager.ExecuteAPIClients(numberOfTestDatas, baseAddress);
        }
    }
}
