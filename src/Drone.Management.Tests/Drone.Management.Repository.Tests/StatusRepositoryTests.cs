using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Repository.PostgreSql;
using Drone.Management.Settings.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Drone.Management.Repository.Tests
{
    public abstract class StatusRepositoryTests
    {
        public static IStatus GetStatus()
        {
            var statusMock = new Mock<IStatus>();
            statusMock.Setup(x => x.Id).Returns(RepositoryTestTools.GetIdentity().Id);
            statusMock.Setup(x => x.Created).Returns(DateTime.Now);
            statusMock.Setup(x => x.Status).Returns("mocked status");
            statusMock.Setup(x => x.Operational).Returns(true);
            return statusMock.Object;
        }

        public static IStatusRepository GetPostgresStatusRepository()
        {
            var repository = new PostgreSqlStatusRepository(RepositoryTestTools.GetSqlRepository(new Mock<IDrone>().Object), RepositoryTestTools.GetDapperInitializer());
            return repository;
        }

        public static IEnumerable<object[]> StatusRepositoryTestData()
        {
            return new List<object[]>
            {
                new object[] { GetPostgresStatusRepository(), RepositoryTestTools.GetPostgreSqlSettings() }
            };
        }

        [Theory]
        [MemberData(nameof(StatusRepositoryTestData))]
        public void CanUseRepository_True(IStatusRepository repository, ISettings settings)
        {
            repository.CanUseRepository(settings).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(StatusRepositoryTestData))]
        public void SetupRepository_NoFail(IStatusRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
        }

        [Theory]
        [MemberData(nameof(StatusRepositoryTestData))]
        public async Task CreateStatus_Success(IStatusRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.CreateStatus(GetStatus());
        }

        [Theory]
        [MemberData(nameof(StatusRepositoryTestData))]
        public async Task ReadStatusIds_Success(IStatusRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.ReadStatusIds(RepositoryTestTools.GetIdentity());
        }

        [Theory]
        [MemberData(nameof(StatusRepositoryTestData))]
        public async Task ReadStatus_Success(IStatusRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.ReadStatus(RepositoryTestTools.GetIdentity());
        }
    }
}