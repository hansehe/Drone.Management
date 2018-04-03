using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Drone.Management.API.Controllers
{
    [Route("api/drones/statuses")]
    public class DroneStatusController : Controller
    {
        private readonly IStatusBusiness StatusBusinessField;

        public DroneStatusController(IStatusBusiness statusBusiness)
        {
            StatusBusinessField = statusBusiness;
        }

        // POST api/drones/statuses
        [HttpPost(Name = "CreateDroneStatus")]
        public async Task<IActionResult> CreateDroneStatus([FromBody] DroneStatusDto droneStatusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var droneStatus = Mapper.Map<IDroneStatus>(droneStatusDto);
            await StatusBusinessField.CreateStatus(droneStatus);

            droneStatusDto = Mapper.Map<DroneStatusDto>(droneStatus);
            var createdAtRouteResult = new CreatedAtRouteResult(new { id = droneStatusDto.Id }, droneStatusDto);
            return createdAtRouteResult;
        }

        // GET api/drones/statuses
        [HttpGet("{droneGuid}", Name = "GetDroneStatusIds")]
        public async Task<IActionResult> GetDroneStatusIds(string droneGuid)
        {
            var idDto = IdentityDto.CreateIdentityDto(droneGuid);
            var id = Mapper.Map<IIdentity>(idDto);
            var droneStatusIds = await StatusBusinessField.GetStatusIds(id);

            var droneStatusIdDtos = Mapper.Map<IEnumerable<IdentityDto>>(droneStatusIds);
            return Ok(droneStatusIdDtos);
        }

        // GET api/drones/statuses/status/{guid}
        [HttpGet("status/{guid}", Name = "GetStatus")]
        public async Task<IActionResult> GetDroneStatus(string guid)
        {
            var idDto = IdentityDto.CreateIdentityDto(guid);
            var id = Mapper.Map<IIdentity>(idDto);
            var droneStatus = await StatusBusinessField.GetStatus(id);

            var droneStatusDto = Mapper.Map<DroneStatusDto>(droneStatus);
            return Ok(droneStatusDto);
        }
    }
}