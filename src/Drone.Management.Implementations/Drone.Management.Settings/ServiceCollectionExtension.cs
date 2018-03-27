using Drone.Management.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Settings
{
    public static class ServiceCollectionExtension
    {
        public static void AddSettingsServices(IServiceCollection services)
        {
            services.AddScoped<ISettings, SettingsHandler>();
        }
    }
}
