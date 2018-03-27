using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.Migrator;
using Drone.Management.AdHoc.Migrator.TestDataMigrators;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.AdHoc.Tests
{
    public class TestDataMigratorTests
    {
        public static ITestData GetTestData()
        {
            var nDatas = 10;
            var testDatas = new List<Tuple<IDrone>>();
            for (var i = 0; i < nDatas; i++)
            {
                var data = new Tuple<IDrone>(new Mock<IDrone>().Object);
                testDatas.Append(data);
            }
            var testData = new Mock<ITestData>();
            testData.Setup(x => x.DataSets).Returns(testDatas);
            return testData.Object;
        }

        public static ISettings GetSettings()
        {
            return new Mock<ISettings>().Object;
        }

        public static IRepositoryBuilder GetRepositoryBuilder()
        {
            var repositoryBuilderMock = new Mock<IRepositoryBuilder>();
            repositoryBuilderMock.Setup(x => x.BuildDroneRepository(It.IsAny<ISettings>())).Returns(new Mock<IDroneRepository>().Object);
            return repositoryBuilderMock.Object;
        }

        public static ITestDataMigrator GetDroneTestDataMigrator()
        {
            return new DroneTestDataMigrator(GetRepositoryBuilder());
        }

        public static IEnumerable<object[]> GetTestDataMigrators()
        {
            return new List<object[]>
            {
                new object[] { GetDroneTestDataMigrator() }
            };
        }

        [Theory]
        [MemberData(nameof(GetTestDataMigrators))]
        public async Task Migrate_Success(ITestDataMigrator migrator)
        {
            var settings = GetSettings();
            await migrator.BuildMigrator(settings);
            migrator.SetTestData(GetTestData());
            await migrator.Migrate();
        }

        [Theory]
        [MemberData(nameof(GetTestDataMigrators))]
        public async Task BuildMigrator_Success(ITestDataMigrator migrator)
        {
            var settings = GetSettings();
            await migrator.BuildMigrator(settings);
        }
    }
}
