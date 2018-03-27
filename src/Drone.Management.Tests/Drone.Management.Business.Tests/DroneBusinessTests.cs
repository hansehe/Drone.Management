using System;
using System.Threading.Tasks;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.Business.Tests
{
    public class DroneBusinessTests
    {
        public static IIdentity GetIdentity()
        {
            var id = new Mock<IIdentity>();
            id.Setup(x => x.Id).Returns(Guid.NewGuid());
            return id.Object;
        }

        public static IDrone GetDrone()
        {
            var droneMock = new Mock<IDrone>();
            droneMock.Setup(x => x.Id).Returns(GetIdentity().Id);
            droneMock.Setup(x => x.Created).Returns(DateTime.Now);
            droneMock.Setup(x => x.Updated).Returns(DateTime.Now);
            droneMock.Setup(x => x.Tag).Returns("mocked tag");
            return droneMock.Object;
        }

        public static IRepositoryBuilder GetRepositoryBuilder()
        {
            var repositoryBuilderMock = new Mock<IRepositoryBuilder>();
            repositoryBuilderMock.Setup(x => x.BuildDroneRepository(It.IsAny<ISettings>()))
                .Returns(new Mock<IDroneRepository>().Object);
            return repositoryBuilderMock.Object;
        }

        public static IDroneBusiness GetDroneBusiness()
        {
            var business = new DroneBusiness(GetRepositoryBuilder(), new Mock<ISettings>().Object);
            return business;
        }
        
        [Fact]
        public async Task CreateDrone_Success()
        {
            var business = GetDroneBusiness();
            await business.CreateDrone(GetDrone());
        }

        [Fact]
        public async Task GetDrone_Success()
        {
            var business = GetDroneBusiness();
            await business.GetDrone(GetIdentity());
        }

        [Fact]
        public async Task GetDroneIds_Success()
        {
            var business = GetDroneBusiness();
            await business.GetDroneIds();
        }

        [Fact]
        public async Task UpdateDrone_Success()
        {
            var business = GetDroneBusiness();
            await business.UpdateDrone(GetDrone());
        }

        [Fact]
        public async Task DeleteDrone_Success()
        {
            var business = GetDroneBusiness();
            await business.DeleteDrone(GetIdentity());
        }
    }
}
