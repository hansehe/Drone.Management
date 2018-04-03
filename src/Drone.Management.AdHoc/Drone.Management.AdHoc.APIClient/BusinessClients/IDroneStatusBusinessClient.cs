using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClient.BusinessClients
{
    public interface IDroneStatusBusinessClient : IHttpClient
    {
        Task<Uri> CreateDroneStatus(IDroneStatus droneStatus);

        Task<IEnumerable<IIdentity>> GetDroneStatusIds(IIdentity droneId);

        Task<IDroneStatus> GetDroneStatus(IIdentity id);
    }
}
