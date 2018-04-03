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
    public class DroneStatusControllerTests
    {
        public DroneStatusControllerTests()
        {
            try
            {
                AutoMapperInit.AutoMapDtoModels();
            }
            catch (Exception e)
            {
            }
        }

        private static DroneStatusDto GetDroneStatusDto()
        {
            var droneStatusDto = new DroneStatusDto();
            return droneStatusDto;
        }

        private static IDroneStatus GetDroneStatus()
        {
            var droneStatus = DroneStatus.CreateDroneStatus("mocked status", true, Guid.NewGuid());
            return droneStatus;
        }

        public IStatusBusiness GetStatusBusiness()
        {
            var statusBusiness = new Mock<IStatusBusiness>();
            statusBusiness.Setup(x => x.GetStatus(It.IsAny<IIdentity>())).ReturnsAsync(GetDroneStatus());
            return statusBusiness.Object;
        }

        public ActionContext GetActionContext()
        {
            return new ActionContext();
        }

        public DroneStatusController GetStatusController()
        {
            var statusBusiness = GetStatusBusiness();
            var droneStatusController = new DroneStatusController(statusBusiness);
            return droneStatusController;
        }

        [Fact]
        public async Task CreateDroneStatus_Success()
        {
            var eventController = GetStatusController();
            var actionResult = await eventController.CreateDroneStatus(GetDroneStatusDto());
            actionResult.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task GetDroneStatusIds_Success()
        {
            var eventController = GetStatusController();
            var actionResult = await eventController.GetDroneStatusIds(Guid.NewGuid().ToString());
            var okObjectResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeAssignableTo<IEnumerable<IdentityDto>>();
        }

        [Fact]
        public async Task GetDroneStatus_Success()
        {
            var eventController = GetStatusController();
            var actionResult = await eventController.GetDroneStatus(Guid.NewGuid().ToString());
            var okObjectResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeAssignableTo<DroneStatusDto>();
        }
    }
}
