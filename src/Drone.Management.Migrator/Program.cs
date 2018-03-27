using System;
using System.Threading.Tasks;
using Drone.Management.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            InputResolver.SetSettingsFromInput(args);
            var serviceProvider = Startup.GetConfiguredServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                ExecuteMigrationAsync(scope.ServiceProvider).GetAwaiter().GetResult();
            }
        }

        public static async Task ExecuteMigrationAsync(IServiceProvider serviceProvider)
        {
            try
            {
                await Startup.ExecuteMigration(serviceProvider);
                Console.WriteLine("Migration finished with success.");
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
