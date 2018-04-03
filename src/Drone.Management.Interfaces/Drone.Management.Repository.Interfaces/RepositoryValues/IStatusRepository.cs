using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Repository.Interfaces.RepositoryValues
{
    public interface IStatusRepository : IRepositoryResolver
    {
        Task CreateStatus(IStatus status);

        Task<IEnumerable<IIdentity>> ReadStatusIds(IIdentity droneId);
        
        Task<IStatus> ReadStatus(IIdentity id);
    }
}
