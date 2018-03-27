using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.Entities.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.AdHoc.Tests
{
    public class DroneBusinessClientTests
    {
        public static IIdentity GetIdentity()
        {
            var id = new Mock<IIdentity>();
            id.Setup(x => x.Id).Returns(Guid.NewGuid());
            return id.Object;
        }

        public static IBusinessClient GetBusinessClient()
        {
            var businessClient = new Mock<IBusinessClient>();
            return businessClient.Object;
        }

        public static IDroneBusinessClient GetDroneBusinessClient()
        {
            var droneBusinessClient = new DroneBusinessClient(GetBusinessClient());
            return droneBusinessClient;
        }

        public static IDrone GetDrone()
        {
            var drone = new Mock<IDrone>();
            return drone.Object;
        }

        [Fact]
        public async Task CreateDrone_Success()
        {
            var businessClient = GetDroneBusinessClient();
            await businessClient.CreateDrone(GetDrone());
        }

        [Fact]
        public async Task GetDroneTags_Success()
        {
            var businessClient = GetDroneBusinessClient();
            await businessClient.GetDroneIds();
        }

        [Fact]
        public async Task GetDrone_Success()
        {
            var businessClient = GetDroneBusinessClient();
            await businessClient.GetDrone(GetIdentity());
        }

        [Fact]
        public async Task UpdateDrone_Success()
        {
            var businessClient = GetDroneBusinessClient();
            await businessClient.UpdateDrone(GetDrone());
        }

        [Fact]
        public async Task DeleteDrone_Success()
        {
            var businessClient = GetDroneBusinessClient();
            await businessClient.DeleteDrone(GetIdentity());
        }
    }
}
