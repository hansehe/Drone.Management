using System;
using System.Collections.Generic;
using Dapper;
using Drone.Management.Config;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Commons;
using Drone.Management.Settings;
using Drone.Management.Settings.Interfaces;
using Moq;

namespace Drone.Management.Repository.Tests
{
    public static class RepositoryTestTools
    {
        public static ISqlRepository GetSqlRepository<T>(T @object)
        {
            var sqlRepository = new Mock<ISqlRepository>();
            sqlRepository
                .Setup(x => x.GetObjects<T>(It.IsAny<string>(), It.IsAny<SqlMapper.IDynamicParameters>())).ReturnsAsync(new List<T>());
            sqlRepository
                .Setup(x => x.GetObject<T>(It.IsAny<string>(), It.IsAny<SqlMapper.IDynamicParameters>())).ReturnsAsync(@object);
            return sqlRepository.Object;
        }

        public static IDapperInitializer GetDapperInitializer()
        {
            return new Mock<IDapperInitializer>().Object;
        }

        public static IIdentity GetIdentity()
        {
            var id = new Mock<IIdentity>();
            id.Setup(x => x.Id).Returns(Guid.NewGuid());
            return id.Object;
        }

        public static ISettings GetPostgreSqlSettings()
        {
            var settings = new SettingsHandler();
            settings.DeserializeSettings(SettingsResolver.GetDefaultPostgreSqlSettings());
            return settings;
        }
    }
}
