using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Business.Interfaces
{
    public interface IStatusBusiness
    {
        Task CreateStatus(IStatus status);

        Task<IEnumerable<IIdentity>> GetStatusIds(IIdentity droneId);
        
        Task<IStatus> GetStatus(IIdentity id);
    }
}
