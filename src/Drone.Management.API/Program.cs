using Drone.Management.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Drone.Management.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InputResolver.SetSettingsFromInput(args);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
