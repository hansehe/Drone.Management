using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.AdHoc.Migrator.TestDataMigrators
{
    public class DroneTestDataMigrator : ITestDataMigrator
    {
        public int Queue { get; } = 0;

        private readonly IRepositoryBuilder RepositoryBuilderField;

        private IDroneRepository DroneRepositoryField { get; set; }

        private ITestData TestDataField;

        public DroneTestDataMigrator(IRepositoryBuilder repositoryBuilder)
        {
            RepositoryBuilderField = repositoryBuilder;
        }

        public void SetTestData(ITestData testData)
        {
            TestDataField = testData;
        }

        public async Task VerifyMigratedTestData()
        {
            Console.WriteLine("\r\nExecuting DRONE test data verification\r\n");
            var droneIds = await DroneRepositoryField.ReadDroneIds();
            Console.WriteLine($"Found {droneIds.Count()} drone ids");
            foreach (var dataSet in TestDataField.DataSets)
            {
                var drone = dataSet.Item1;
                var stopWatch = Stopwatch.StartNew();
                var cpDrone = await DroneRepositoryField.ReadDrone(drone);
                stopWatch.Stop();
                if (cpDrone.Id.CompareTo(drone.Id) != 0)
                {
                    throw new Exception("Drone read from DB does not match created drone!");
                }
                Console.WriteLine($"Read drone {cpDrone.Tag} in {stopWatch.ElapsedMilliseconds} ms");
                drone.Tag = $"Updated {drone.Tag}";
                await DroneRepositoryField.UpdateDrone(drone);
                await DroneRepositoryField.DeleteDrone(drone);
                await DroneRepositoryField.CreateDrone(drone);
            }
        }

        public async Task Migrate()
        {
            Console.WriteLine($"Creating {TestDataField.DataSets.Count} drones");
            foreach (var dataSet in TestDataField.DataSets)
            {
                var drone = dataSet.Item1;
                await DroneRepositoryField.CreateDrone(drone);
            }
        }

        public async Task Connect()
        {
        }

        public async Task<IMigrator> BuildMigrator(ISettings settings)
        {
            DroneRepositoryField = RepositoryBuilderField.BuildDroneRepository(settings);
            return this;
        }
    }
}
