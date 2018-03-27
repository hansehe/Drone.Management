using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Config;
using Drone.Management.Migrator.Commons;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Migrator.PostgreSql;
using Drone.Management.Settings;
using Drone.Management.Settings.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Drone.Management.Migrator.Tests
{
    public class MigratorTests
    {
        public static ISettings GetValidPostgreSettings()
        {
            var settings = new SettingsHandler();
            settings.DeserializeSettings(SettingsResolver.GetDefaultPostgreSqlSettings());
            return settings;
        }

        public static IMigratorToolResolver GetMigratorToolResolver()
        {
            var migratorToolResolver = new Mock<IMigratorToolResolver>();
            migratorToolResolver.Setup(x => x.ResolveMigratorTool(It.IsAny<AvailableMigratorTools>()))
                .Returns(new Mock<IMigratorTool>().Object);
            return migratorToolResolver.Object;
        }

        public static IMigratorResolver GetPostgreSqlMigratorResolver()
        {
            return new PostgreSqlMigrator(GetMigratorToolResolver());
        }

        public static IEnumerable<object[]> SuccessMigratorTestData()
        {
            return new List<object[]>
            {
                new object[] { GetPostgreSqlMigratorResolver(), GetValidPostgreSettings() }
            };
        }

        [Theory]
        [MemberData(nameof(SuccessMigratorTestData))]
        public async Task Migrate_Success(IMigratorResolver migrator, ISettings settings)
        {
            await migrator.SetupMigration(settings);
            await migrator.Migrate();
        }

        [Theory]
        [MemberData(nameof(SuccessMigratorTestData))]
        public void CanMigrate_True(IMigratorResolver migrator, ISettings settings)
        {
            migrator.CanMigrate(settings).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(SuccessMigratorTestData))]
        public async Task SetupMigration_Success(IMigratorResolver migrator, ISettings settings)
        {
            await migrator.SetupMigration(settings);
        }

        [Theory]
        [MemberData(nameof(SuccessMigratorTestData))]
        public async Task Connect_Success(IMigratorResolver migrator, ISettings settings)
        {
            await migrator.SetupMigration(settings);
            await migrator.Connect();
        }
    }
}
