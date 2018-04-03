using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Drone.Management.Entities.Models;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClient.BusinessClients
{
    public class DroneStatusBusinessClient : IDroneStatusBusinessClient
    {
        private readonly IBusinessClient BusinessClientField;

        private const string APIValueAddress = "api/drones/statuses/";

        public DroneStatusBusinessClient(IBusinessClient businessClient)
        {
            BusinessClientField = businessClient;
        }
        
        public void SetupHttpClient(Uri baseAddress)
        {
            BusinessClientField.SetupHttpClient(baseAddress);
        }

        public Task<Uri> CreateDroneStatus(IDroneStatus droneStatus)
        {
            var droneStatusDto = Mapper.Map<DroneStatusDto>(droneStatus);
            return BusinessClientField.CreateObject(APIValueAddress, droneStatusDto);
        }

        public async Task<IEnumerable<IIdentity>> GetDroneStatusIds(IIdentity droneId)
        {
            var uri = $"{APIValueAddress}{droneId.Id.ToString()}";
            var idDtos = await BusinessClientField.GetObject<IEnumerable<IdentityDto>>(uri);
            var ids = Mapper.Map<IEnumerable<IIdentity>>(idDtos);
            return ids;
        }

        public async Task<IDroneStatus> GetDroneStatus(IIdentity id)
        {
            var uri = $"{APIValueAddress}status/{id.Id.ToString()}";
            var droneStatusDto = await BusinessClientField.GetObject<DroneStatusDto>(uri);
            var droneStatus = Mapper.Map<IDroneStatus>(droneStatusDto);
            return droneStatus;
        }
    }
}
