using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.AdHoc.Migrator.TestDataMigrators
{
    public class DroneStatusTestDataMigrator : ITestDataMigrator
    {
        public int Queue { get; } = 1;

        private readonly IRepositoryBuilder RepositoryBuilderField;

        private IStatusRepository StatusRepositoryField { get; set; }

        private ITestData TestDataField;

        public DroneStatusTestDataMigrator(IRepositoryBuilder repositoryBuilder)
        {
            RepositoryBuilderField = repositoryBuilder;
        }

        public void SetTestData(ITestData testData)
        {
            TestDataField = testData;
        }

        public async Task VerifyMigratedTestData()
        {
            Console.WriteLine("\r\nExecuting DRONE STATUS test data verification\r\n");
            foreach (var dataSet in TestDataField.DataSets)
            {
                var drone = dataSet.Item1;
                var droneStatus = dataSet.Item2;
                await ReadDroneStatusIds(drone);
                await VerifyDroneStatus(droneStatus);
            }
        }

        public async Task DeleteMigratedTestData()
        {
        }

        public async Task Migrate()
        {
            Console.WriteLine($"Creating {TestDataField.DataSets.Count} drone statuses");
            foreach (var dataSet in TestDataField.DataSets)
            {
                var droneStatus = dataSet.Item2;
                await StatusRepositoryField.CreateStatus(droneStatus);
            }
        }

        public async Task Connect()
        {
        }

        public async Task<IMigrator> BuildMigrator(ISettings settings)
        {
            StatusRepositoryField = RepositoryBuilderField.BuildStatusRepository(settings);
            return this;
        }

        public async Task ReadDroneStatusIds(IIdentity droneId)
        {
            var droneStatusIds = await StatusRepositoryField.ReadStatusIds(droneId);
            Console.WriteLine($"Found {droneStatusIds.Count()} drone status ids");
        }

        public async Task VerifyDroneStatus(IDroneStatus droneStatus)
        {
            var stopWatch = Stopwatch.StartNew();
            var cpDroneStatus = await StatusRepositoryField.ReadStatus(droneStatus);
            stopWatch.Stop();
            if (cpDroneStatus.Id.CompareTo(droneStatus.Id) != 0)
            {
                throw new Exception("Drone read from DB does not match created drone!");
            }
            Console.WriteLine($"Verified drone status {cpDroneStatus.Status} in {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}
