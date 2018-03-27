using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Repository.Interfaces.RepositoryValues
{
    public interface IDroneRepository : IRepositoryResolver
    {
        Task CreateDrone(IDrone drone);

        Task<IEnumerable<IIdentity>> ReadDroneIds();
        
        Task<IDrone> ReadDrone(IIdentity id);

        Task UpdateDrone(IDrone drone);

        Task DeleteDrone(IIdentity id);
    }
}
