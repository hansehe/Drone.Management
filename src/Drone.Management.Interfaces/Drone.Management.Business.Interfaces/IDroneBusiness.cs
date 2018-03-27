using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Business.Interfaces
{
    public interface IDroneBusiness
    {
        Task CreateDrone(IDrone drone);

        Task<IEnumerable<IIdentity>> GetDroneIds();
        
        Task<IDrone> GetDrone(IIdentity id);

        Task UpdateDrone(IDrone drone);

        Task DeleteDrone(IIdentity id);
    }
}
