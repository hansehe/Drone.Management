using System;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClientConsole
{
    internal static class DroneAPIClient
    {
        internal static async Task ExecuteDroneAPIClient(IDroneBusinessClient droneBusinessClient, ITestData testData, Uri baseAddress)
        {
            Console.WriteLine($"Connecting to {baseAddress} drone API server");
            droneBusinessClient.SetupHttpClient(baseAddress);
            await ExecuteGetDroneIds(droneBusinessClient);
            foreach (var dataSet in testData.DataSets)
            {
                var drone = dataSet.Item1;
                await ExecuteCreateAndGetDrone(droneBusinessClient, drone);
            }
        }

        private static async Task ExecuteGetDroneIds(IDroneBusinessClient droneBusinessClient)
        {
            var droneIds = await droneBusinessClient.GetDroneIds();
            Console.WriteLine($"Found {droneIds.Count()} drone ids");
        }

        private static async Task ExecuteCreateAndGetDrone(IDroneBusinessClient droneBusinessClient, IDrone drone)
        {
            await droneBusinessClient.CreateDrone(drone);
            var cpDrone = await droneBusinessClient.GetDrone(drone);
            if (cpDrone.Id.CompareTo(drone.Id) != 0)
            {
                throw new Exception("Drone read from API does not match created drone!");
            }
            drone.Tag = $"Updated {drone.Tag}";
            await droneBusinessClient.UpdateDrone(drone);
            await droneBusinessClient.DeleteDrone(drone);
            Console.WriteLine($"Created, read, updated and deleted drone with tag: {drone.Tag}");
        }
    }
}
