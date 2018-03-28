using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.AdHoc.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.APIClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.GetEnvironmentVariable("START_API_CLIENT") != "TRUE")
            {
                return;
            }
            var serviceProvider = Startup.GetConfiguredServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                ExecuteAPIClientAsync(scope.ServiceProvider).GetAwaiter().GetResult();
            }
        }

        public static async Task ExecuteAPIClientAsync(IServiceProvider serviceProvider)
        {
            if (int.TryParse(Environment.GetEnvironmentVariable("MS_START_DELAY"), out var msStartDelay))
            {
                System.Threading.Thread.Sleep(msStartDelay);
            }
            var droneBusinessClient = serviceProvider.GetService<IDroneBusinessClient>();
            var testData = serviceProvider.GetService<ITestData>();
            try
            {
                await APIClient.ExecuteAPIClient(droneBusinessClient, testData);
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
