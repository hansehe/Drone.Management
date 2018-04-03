using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.API.Controllers;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Drone.Management.API.Tests
{
    public class DroneControllerTests
    {
        public DroneControllerTests()
        {
            try
            {
                AutoMapperInit.AutoMapDtoModels();
            }
            catch (Exception e)
            {
            }
        }

        private static DroneDto GetDroneDto()
        {
            var droneDto = new DroneDto();
            return droneDto;
        }

        private static IDrone GetDrone()
        {
            var drone = Entities.Drone.CreateDrone("mocked drone", "mocked owner");
            return drone;
        }

        public IDroneBusiness GetEventBusiness()
        {
            var eventBusiness = new Mock<IDroneBusiness>();
            eventBusiness.Setup(x => x.GetDrone(It.IsAny<IIdentity>())).ReturnsAsync(GetDrone());
            return eventBusiness.Object;
        }

        public ActionContext GetActionContext()
        {
            return new ActionContext();
        }

        public DroneController GetDroneController()
        {
            var eventBusiness = GetEventBusiness();
            var eventController = new DroneController(eventBusiness);
            return eventController;
        }

        [Fact]
        public async Task CreateDrone_Success()
        {
            var eventController = GetDroneController();
            var actionResult = await eventController.CreateDrone(GetDroneDto());
            actionResult.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task GetDroneIds_Success()
        {
            var eventController = GetDroneController();
            var actionResult = await eventController.GetDroneIds();
            var okObjectResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeAssignableTo<IEnumerable<IdentityDto>>();
        }

        [Fact]
        public async Task GetDrone_Success()
        {
            var eventController = GetDroneController();
            var actionResult = await eventController.GetDrone(Guid.NewGuid().ToString());
            var okObjectResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeAssignableTo<DroneDto>();
        }
        
        [Fact]
        public async Task UpdateDrone_Success()
        {
            var eventController = GetDroneController();
            var actionResult = await eventController.UpdateDrone(GetDroneDto());
            actionResult.Should().BeOfType<AcceptedAtRouteResult>();
        }

        [Fact]
        public async Task DeleteDrone_Success()
        {
            var eventController = GetDroneController();
            var actionResult = await eventController.DeleteDrone(Guid.NewGuid().ToString());
            var okObjectResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeAssignableTo<string>();
        }
    }
}
