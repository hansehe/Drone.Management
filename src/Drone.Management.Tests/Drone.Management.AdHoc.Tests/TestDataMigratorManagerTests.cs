using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.AdHoc.Migrator;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.AdHoc.Tests
{
    public class TestDataMigratorManagerTests
    {
        public static ISettings GetSettings()
        {
            return new Mock<ISettings>().Object;
        }

        public static ITestDataMigrator GetTestDataMigrator(int queue)
        {
            var migrator = new Mock<ITestDataMigrator>();
            migrator.Setup(x => x.Queue).Returns(queue);
            migrator.Setup(x => x.BuildMigrator(It.IsAny<ISettings>())).ReturnsAsync(new Mock<IMigrator>().Object);
            return migrator.Object;
        }

        public static IEnumerable<ITestDataMigrator> GetTestDataMigrators()
        {
            var migrators = new List<ITestDataMigrator>
            {
                GetTestDataMigrator(0),
                GetTestDataMigrator(1),
                GetTestDataMigrator(2)
            };
            return migrators;
        }

        public static ITestDataMigratorManager GetTestDataMigratorManager()
        {
            var testDataMigratorManager = new TestDataMigratorManager(GetTestDataMigrators(), new Mock<ITestData>().Object);
            return testDataMigratorManager;
        }

        [Fact]
        public async Task Migrate_Success()
        {
            var settings = GetSettings();
            var migrator = GetTestDataMigratorManager();
            await migrator.BuildMigrator(settings);
            await migrator.Migrate();
        }

        [Fact]
        public async Task BuildMigrator_Success()
        {
            var settings = GetSettings();
            var migrator = GetTestDataMigratorManager();
            await migrator.BuildMigrator(settings);
        }
    }
}