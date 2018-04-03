using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.APIClientConsole
{
    class Program
    {
        private const string DefaultBaseAddress = "http://localhost:9718/";

        private const long DefaultNumberOfTestDatas = 10;

        static void Main(string[] args)
        {
            if (Environment.GetEnvironmentVariable("START_API_CLIENT") != "TRUE")
            {
                return;
            }
            var serviceProvider = Startup.GetConfiguredServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                ExecuteAPIClientsAsync(scope.ServiceProvider).GetAwaiter().GetResult();
            }
        }

        private static async Task ExecuteAPIClientsAsync(IServiceProvider serviceProvider)
        {
            if (int.TryParse(Environment.GetEnvironmentVariable("MS_START_DELAY"), out var msStartDelay))
            {
                System.Threading.Thread.Sleep(msStartDelay);
            }
            var numberOfTestDatas = DefaultNumberOfTestDatas;
            if (long.TryParse(Environment.GetEnvironmentVariable("N_TEST_DATAS"), out var nDatas))
            {
                numberOfTestDatas = nDatas;
            }
            var envBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS");
            var baseAddress = new Uri(envBaseAddress ?? DefaultBaseAddress);

            var apiClientManager = serviceProvider.GetService<IAPIClientManager>();
            try
            {
                await apiClientManager.ExecuteAPIClients(numberOfTestDatas, baseAddress);
                Console.WriteLine("Integration test with API Client finished with success.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (Environment.GetEnvironmentVariable("WAIT_BEFORE_CLOSE") == "TRUE")
            {
                Console.WriteLine("Hit enter to exit..");
                Console.ReadLine();
            }
        }
    }
}
