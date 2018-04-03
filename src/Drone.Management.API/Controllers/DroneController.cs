using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Drone.Management.API.Controllers
{
    [Route("api/drones")]
    public class DroneController : Controller
    {
        private readonly IDroneBusiness DroneBusinessField;

        public DroneController(IDroneBusiness droneBusiness)
        {
            DroneBusinessField = droneBusiness;
        }

        // POST api/drones
        [HttpPost(Name = "CreateDrone")]
        public async Task<IActionResult> CreateDrone([FromBody] DroneDto droneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drone = Mapper.Map<IDrone>(droneDto);
            await DroneBusinessField.CreateDrone(drone);

            droneDto = Mapper.Map<DroneDto>(drone);
            var createdAtRouteResult = new CreatedAtRouteResult(new { id = droneDto.Id }, droneDto);
            return createdAtRouteResult;
        }

        // GET api/drones
        [HttpGet(Name = "GetDroneIds")]
        public async Task<IActionResult> GetDroneIds()
        {
            var droneIds = await DroneBusinessField.GetDroneIds();

            var droneIdDtos = Mapper.Map<IEnumerable<IdentityDto>>(droneIds);
            return Ok(droneIdDtos);
        }

        // GET api/drones/{guid}
        [HttpGet("{guid}", Name = "GetDrone")]
        public async Task<IActionResult> GetDrone(string guid)
        {
            var idDto = IdentityDto.CreateIdentityDto(guid);
            var id = Mapper.Map<IIdentity>(idDto);
            var drone = await DroneBusinessField.GetDrone(id);

            var droneDto = Mapper.Map<DroneDto>(drone);
            return Ok(droneDto);
        }

        // GET api/drones
        [HttpGet(Name = "GetDrones")]
        public async Task<IActionResult> GetDrones([FromBody] IEnumerable<IdentityDto> droneIdDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var droneIds = Mapper.Map<IEnumerable<IIdentity>>(droneIdDtos);
            var drones = await DroneBusinessField.GetDrones(droneIds);

            var droneDto = Mapper.Map<IEnumerable<DroneDto>>(drones);
            return Ok(droneDto);
        }

        // PUT api/drones
        [HttpPut(Name = "UpdateDrone")]
        public async Task<IActionResult> UpdateDrone([FromBody] DroneDto droneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drone = Mapper.Map<IDrone>(droneDto);
            await DroneBusinessField.UpdateDrone(drone);

            droneDto = Mapper.Map<DroneDto>(drone);
            var acceptedAtRouteResult = new AcceptedAtRouteResult(new { id = droneDto.Id }, droneDto);
            return acceptedAtRouteResult;
        }

        // DELETE api/drones/{guid}
        [HttpDelete("{guid}", Name = "DeleteDrone")]
        public async Task<IActionResult> DeleteDrone(string guid)
        {
            var idDto = IdentityDto.CreateIdentityDto(guid);
            var id = Mapper.Map<IIdentity>(idDto);
            await DroneBusinessField.DeleteDrone(id);
            
            return Ok(idDto);
        }
    }
}