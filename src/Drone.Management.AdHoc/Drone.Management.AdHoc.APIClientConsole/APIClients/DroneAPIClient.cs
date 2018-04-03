using System;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClientConsole.APIClients
{
    internal class DroneAPIClient : IAPIClient
    {
        private readonly IDroneBusinessClient DroneBusinessClientField;

        public int Queue { get; } = 0;

        public DroneAPIClient(IDroneBusinessClient droneBusinessClient)
        {
            DroneBusinessClientField = droneBusinessClient;
        }

        public Task ExecuteAPIClient(ITestData testData, Uri baseAddress)
        {
            return ExecuteDroneAPIClient(DroneBusinessClientField, testData, baseAddress);
        }

        internal static async Task ExecuteDroneAPIClient(IDroneBusinessClient droneBusinessClient, ITestData testData, Uri baseAddress)
        {
            Console.WriteLine($"\r\nConnecting to DRONE API server on {baseAddress.AbsoluteUri} with port {baseAddress.Port}\r\n");
            droneBusinessClient.SetupHttpClient(baseAddress);
            await ExecuteGetDroneIds(droneBusinessClient);
            foreach (var dataSet in testData.DataSets)
            {
                var drone = dataSet.Item1;
                await ExecuteCreateDrone(droneBusinessClient, drone);
                await ExecuteGetDrone(droneBusinessClient, drone);
                await ExecuteUpdateDrone(droneBusinessClient, drone);
                await ExecuteDeleteDrone(droneBusinessClient, drone);
                Console.WriteLine($"Created, read, updated and deleted drone with tag: {drone.Tag}");
            }
        }

        private static async Task ExecuteGetDroneIds(IDroneBusinessClient droneBusinessClient)
        {
            var droneIds = await droneBusinessClient.GetDroneIds();
            Console.WriteLine($"Found {droneIds.Count()} drone ids");
        }

        private static async Task ExecuteCreateDrone(IDroneBusinessClient droneBusinessClient, IDrone drone)
        {
            var uri = await droneBusinessClient.CreateDrone(drone);
            if ($"?id={drone.Id.ToString()}" != uri.Query)
            {
                throw new Exception("Created drone id returned from API does not match original drone id!");
            }
        }

        private static async Task ExecuteGetDrone(IDroneBusinessClient droneBusinessClient, IDrone drone)
        {
            var cpDrone = await droneBusinessClient.GetDrone(drone);
            if (cpDrone.Id.CompareTo(drone.Id) != 0)
            {
                throw new Exception("Drone read from API does not match created drone!");
            }
        }

        private static async Task ExecuteUpdateDrone(IDroneBusinessClient droneBusinessClient, IDrone drone)
        {
            drone.Tag = $"Updated {drone.Tag}";
            var uri = await droneBusinessClient.UpdateDrone(drone);
            if ($"?id={drone.Id.ToString()}" != uri.Query)
            {
                throw new Exception("Updated event id returned from API does not match original event id!");
            }
        }

        private static Task ExecuteDeleteDrone(IDroneBusinessClient droneBusinessClient, IDrone drone)
        {
            return droneBusinessClient.DeleteDrone(drone);
        }
    }
}
