using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.APIClient.BusinessClients
{
    public interface IDroneBusinessClient : IHttpClient
    {
        Task CreateDrone(IDrone drone);

        Task<IEnumerable<IIdentity>> GetDroneIds();

        Task<IDrone> GetDrone(IIdentity id);

        Task UpdateDrone(IDrone drone);

        Task DeleteDrone(IIdentity id);
    }
}
