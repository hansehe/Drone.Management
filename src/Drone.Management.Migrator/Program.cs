﻿using System;
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
            if (int.TryParse(Environment.GetEnvironmentVariable("MS_START_DELAY"), out var msStartDelay))
            {
                System.Threading.Thread.Sleep(msStartDelay);
            }
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
                Console.WriteLine("\r\nMigration finished with success.\r\n");
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
