using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Drone.Management.Entities.Models;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClient.BusinessClients
{
    public class DroneBusinessClient : IDroneBusinessClient
    {
        private readonly IBusinessClient BusinessClientField;

        private const string APIValueAddress = "api/drones/";

        public DroneBusinessClient(IBusinessClient businessClient)
        {
            BusinessClientField = businessClient;
        }
        
        public void SetupHttpClient(Uri baseAddress)
        {
            BusinessClientField.SetupHttpClient(baseAddress);
        }

        public Task<Uri> CreateDrone(IDrone drone)
        {
            var droneDto = Mapper.Map<DroneDto>(drone);
            return BusinessClientField.CreateObject(APIValueAddress, droneDto);
        }

        public async Task<IEnumerable<IIdentity>> GetDroneIds()
        {
            var idDtos = await BusinessClientField.GetObject<IEnumerable<IdentityDto>>(APIValueAddress);
            var ids = Mapper.Map<IEnumerable<IIdentity>>(idDtos);
            return ids;
        }

        public async Task<IDrone> GetDrone(IIdentity id)
        {
            var uri = $"{APIValueAddress}{id.Id.ToString()}";
            var droneDto = await BusinessClientField.GetObject<DroneDto>(uri);
            var drone = Mapper.Map<IDrone>(droneDto);
            return drone;
        }

        public Task<Uri> UpdateDrone(IDrone drone)
        {
            var droneDto = Mapper.Map<DroneDto>(drone);
            return BusinessClientField.UpdateObject(APIValueAddress, droneDto);
        }

        public Task DeleteDrone(IIdentity id)
        {
            var uri = $"{APIValueAddress}{id.Id.ToString()}";
            return BusinessClientField.DeleteObject(uri);
        }
    }
}
