using System;
using System.Collections.Generic;
using Drone.Management.Repository.Builder;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.Repository.Tests
{
    public class RepositoryBuilderTests
    {
        public static IDroneRepository GetDroneRepository(bool canUseFlag)
        {
            var repositoryMock = new Mock<IDroneRepository>();
            repositoryMock.Setup(x => x.CanUseRepository(It.IsAny<ISettings>())).Returns(canUseFlag);
            return repositoryMock.Object;
        }
        
        public static IEnumerable<IDroneRepository> GetDroneRepositories(bool canUseFlag)
        {
            var repositories = new List<IDroneRepository> { GetDroneRepository(false), GetDroneRepository(canUseFlag), GetDroneRepository(false) };
            return repositories;
        }

        public static IRepositoryBuilder GetRepositoryBuilder(bool canUseFlag)
        {
            return new RepositoryBuilder(GetDroneRepositories(canUseFlag));
        }

        public static ISettings GetMockSettings()
        {
            var settingsMock = new Mock<ISettings>();
            return settingsMock.Object;
        }

        [Fact]
        public void BuildDroneRepository_Success()
        {
            var repositoryBuilderBuilder = GetRepositoryBuilder(true);
            var settings = GetMockSettings();
            var repository = repositoryBuilderBuilder.BuildDroneRepository(settings);
        }

        [Fact]
        public void BuildDroneRepository_NoSuccess()
        {
            var repositoryBuilderBuilder = GetRepositoryBuilder(false);
            var settings = GetMockSettings();
            Assert.Throws<Exception>(() => repositoryBuilderBuilder.BuildDroneRepository(settings));
        }
    }
}
