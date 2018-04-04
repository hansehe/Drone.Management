using System;
using Drone.Management.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InputResolver.SetSettingsFromInput(args);
            var serviceProvider = Startup.GetConfiguredServiceProvider();
            if (int.TryParse(Environment.GetEnvironmentVariable("MS_START_DELAY"), out var msStartDelay))
            {
                System.Threading.Thread.Sleep(msStartDelay);
            }
            using (var scope = serviceProvider.CreateScope())
            {
                TestDataMigrator.ExecuteTestDataMigrationAsync(scope.ServiceProvider).GetAwaiter().GetResult();
                System.Console.WriteLine("\r\nTest data migrated with success.\r\n");
            }
            using (var scope = serviceProvider.CreateScope())
            {
                APIClientExecutor.ExecuteAPIClientsAsync(scope.ServiceProvider).GetAwaiter().GetResult();
                System.Console.WriteLine("\r\nIntegration test with API Client finished with success.\r\n");
            }
            if (Environment.GetEnvironmentVariable("WAIT_BEFORE_CLOSE") == "TRUE")
            {
                System.Console.WriteLine("Hit enter to exit..");
                System.Console.ReadLine();
            }
        }
    }
}
