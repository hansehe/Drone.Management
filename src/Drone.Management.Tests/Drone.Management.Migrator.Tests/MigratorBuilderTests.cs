using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Migrator.Builder;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.Migrator.Tests
{
    public class MigratorBuilderTests
    {
        public static IMigratorResolver GetMockMigratorResolver(bool canMigrateFlag)
        {
            var migratorMock = new Mock<IMigratorResolver>();
            migratorMock.Setup(x => x.CanMigrate(It.IsAny<ISettings>())).Returns(canMigrateFlag);
            return migratorMock.Object;
        }

        public static IEnumerable<IMigratorResolver> GetMigratorResolvers(bool canMigrateFlag)
        {
            var migrators = new List<IMigratorResolver> { GetMockMigratorResolver(false), GetMockMigratorResolver(canMigrateFlag), GetMockMigratorResolver(false) };
            return migrators;
        }

        public static IMigratorBuilder GetMigratorBuilder(bool canMigrateFlag)
        {
            return new MigratorBuilder(GetMigratorResolvers(canMigrateFlag));
        }

        public static ISettings GetMockSettings()
        {
            var settingsMock = new Mock<ISettings>();
            return settingsMock.Object;
        }

        [Fact]
        public void BuildMigrator_Success()
        {
            var migratorBuilder = GetMigratorBuilder(true);
            var settings = GetMockSettings();
            var migrator = migratorBuilder.BuildMigrator(settings);
        }

        [Fact]
        public async Task BuildMigrator_NoSuccess()
        {
            var migratorBuilder = GetMigratorBuilder(false);
            var settings = GetMockSettings();
            await Assert.ThrowsAsync<Exception>(() => migratorBuilder.BuildMigrator(settings));
        }
    }
}
