using System;
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
            try
            {
                await DroneBusinessField.CreateDrone(drone);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

            return Ok(droneDto);
        }

        // GET api/drones
        [HttpGet(Name = "GetDroneIds")]
        public async Task<IActionResult> GetDroneIds()
        {
            IEnumerable<IIdentity> droneIds;
            try
            {
                droneIds = await DroneBusinessField.GetDroneIds();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

            var droneIdDtos = Mapper.Map<IEnumerable<IdentityDto>>(droneIds);
            return Ok(droneIdDtos);
        }

        // GET api/drones/{guid}
        [HttpGet("{guid}", Name = "GetDrone")]
        public async Task<IActionResult> GetDrone(string guid)
        {
            IDrone drone;
            try
            {
                var idDto = IdentityDto.CreateIdentityDto(guid);
                var id = Mapper.Map<IIdentity>(idDto);
                drone = await DroneBusinessField.GetDrone(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

            var droneDto = Mapper.Map<DroneDto>(drone);
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
            try
            {
                await DroneBusinessField.UpdateDrone(drone);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

            return Ok(droneDto);
        }

        // DELETE api/drones/{guid}
        [HttpDelete("{guid}", Name = "DeleteDrone")]
        public async Task<IActionResult> DeleteDrone(string guid)
        {
            try
            {
                var idDto = IdentityDto.CreateIdentityDto(guid);
                var id = Mapper.Map<IIdentity>(idDto);
                await DroneBusinessField.DeleteDrone(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

            return Ok(guid);
        }
    }
}