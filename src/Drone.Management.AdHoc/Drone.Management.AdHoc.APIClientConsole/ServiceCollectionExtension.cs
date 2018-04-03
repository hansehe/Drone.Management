﻿using Drone.Management.AdHoc.APIClientConsole.APIClients;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.AdHoc.APIClientConsole
{
    public static class ServiceCollectionExtension
    {
        public static void AddAdHocAPIClientConsoleServices(IServiceCollection services)
        {
            services.AddScoped<IAPIClientManager, APIClientManager>();
            services.AddScoped<IAPIClient, DroneAPIClient>();
            services.AddScoped<IAPIClient, DroneStatusAPIClient>();
        }
    }
}