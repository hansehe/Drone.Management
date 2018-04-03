using System;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClientConsole.APIClients
{
    internal class DroneStatusAPIClient : IAPIClient
    {
        private readonly IDroneStatusBusinessClient DroneStatusBusinessClientField;

        public int Queue { get; } = 1;

        public DroneStatusAPIClient(IDroneStatusBusinessClient droneStatusBusinessClient)
        {
            DroneStatusBusinessClientField = droneStatusBusinessClient;
        }

        public Task ExecuteAPIClient(ITestData testData, Uri baseAddress)
        {
            return ExecuteDroneAPIClient(DroneStatusBusinessClientField, testData, baseAddress);
        }

        internal static async Task ExecuteDroneAPIClient(IDroneStatusBusinessClient droneStatusBusinessClient, ITestData testData, Uri baseAddress)
        {
            Console.WriteLine($"\r\nConnecting to DRONE API server on {baseAddress.AbsoluteUri} with port {baseAddress.Port}\r\n");
            droneStatusBusinessClient.SetupHttpClient(baseAddress);
            foreach (var dataSet in testData.DataSets)
            {
                var drone = dataSet.Item1;
                var droneStatus = dataSet.Item2;
                await ExecuteGetStatusDroneIds(droneStatusBusinessClient, drone);
                await ExecuteCreateDroneStatus(droneStatusBusinessClient, droneStatus);
                await ExecuteGetDroneStatus(droneStatusBusinessClient, droneStatus);
                Console.WriteLine($"Created and read drone status with tag: {droneStatus.Status}");
            }
        }

        private static async Task ExecuteGetStatusDroneIds(IDroneStatusBusinessClient droneStatusBusinessClient, IIdentity droneId)
        {
            var droneIds = await droneStatusBusinessClient.GetDroneStatusIds(droneId);
            Console.WriteLine($"Found {droneIds.Count()} drone status ids");
        }

        private static async Task ExecuteCreateDroneStatus(IDroneStatusBusinessClient droneStatusBusinessClient, IDroneStatus droneStatus)
        {
            var uri = await droneStatusBusinessClient.CreateDroneStatus(droneStatus);
            if ($"?id={droneStatus.Id.ToString()}" != uri.Query)
            {
                throw new Exception("Created drone status id returned from API does not match original drone status id!");
            }
        }

        private static async Task ExecuteGetDroneStatus(IDroneStatusBusinessClient droneStatusBusinessClient, IDroneStatus droneStatus)
        {
            var cpDrone = await droneStatusBusinessClient.GetDroneStatus(droneStatus);
            if (cpDrone.Id.CompareTo(droneStatus.Id) != 0)
            {
                throw new Exception("Drone status read from API does not match created drone status!");
            }
        }
    }
}
