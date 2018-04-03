using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Business
{
    public class StatusBusiness : IStatusBusiness
    {
        private readonly IStatusRepository StatusRepositoryField;

        public StatusBusiness(IRepositoryBuilder repositoryBuilder, ISettings settings)
        {
            StatusRepositoryField = repositoryBuilder.BuildStatusRepository(settings);
        }
        
        public Task CreateStatus(IStatus status)
        {
            return StatusRepositoryField.CreateStatus(status);
        }

        public Task<IEnumerable<IIdentity>> GetStatusIds(IIdentity droneId)
        {
            return StatusRepositoryField.ReadStatusIds(droneId);
        }

        public Task<IStatus> GetStatus(IIdentity id)
        {
            return StatusRepositoryField.ReadStatus(id);
        }
    }
}
