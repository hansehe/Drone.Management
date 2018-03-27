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
    public abstract class DroneRepositoryTests
    {
        public static IDrone GetDrone()
        {
            var droneMock = new Mock<IDrone>();
            droneMock.Setup(x => x.Id).Returns(RepositoryTestTools.GetIdentity().Id);
            droneMock.Setup(x => x.Created).Returns(DateTime.Now);
            droneMock.Setup(x => x.Updated).Returns(DateTime.Now);
            droneMock.Setup(x => x.Tag).Returns("mocked tag");
            return droneMock.Object;
        }

        public static IDroneRepository GetPostgresDroneRepository()
        {
            var repository = new PostgreSqlDroneRepository(RepositoryTestTools.GetSqlRepository(new Mock<IDrone>().Object), RepositoryTestTools.GetDapperInitializer());
            return repository;
        }

        public static IEnumerable<object[]> DroneRepositoryTestData()
        {
            return new List<object[]>
            {
                new object[] { GetPostgresDroneRepository(), RepositoryTestTools.GetPostgreSqlSettings() }
            };
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public void CanUseRepository_True(IDroneRepository repository, ISettings settings)
        {
            repository.CanUseRepository(settings).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public void SetupRepository_NoFail(IDroneRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public async Task CreateDrone_Success(IDroneRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.CreateDrone(GetDrone());
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public async Task ReadDroneIds_Success(IDroneRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.ReadDroneIds();
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public async Task ReadDrone_Success(IDroneRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.ReadDrone(RepositoryTestTools.GetIdentity());
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public async Task UpdateDrone_Success(IDroneRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.UpdateDrone(GetDrone());
        }

        [Theory]
        [MemberData(nameof(DroneRepositoryTestData))]
        public async Task DeleteDrone_Success(IDroneRepository repository, ISettings settings)
        {
            repository.SetupRepository(settings);
            await repository.DeleteDrone(RepositoryTestTools.GetIdentity());
        }
    }
}