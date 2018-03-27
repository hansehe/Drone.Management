using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Business
{
    public class DroneBusiness : IDroneBusiness
    {
        private readonly IDroneRepository DroneRepositoryField;

        public DroneBusiness(IRepositoryBuilder repositoryBuilder, ISettings settings)
        {
            DroneRepositoryField = repositoryBuilder.BuildDroneRepository(settings);
        }

        public Task CreateDrone(IDrone drone)
        {
            return DroneRepositoryField.CreateDrone(drone);
        }

        public Task<IEnumerable<IIdentity>> GetDroneIds()
        {
            return DroneRepositoryField.ReadDroneIds();
        }

        public Task<IDrone> GetDrone(IIdentity id)
        {
            return DroneRepositoryField.ReadDrone(id);
        }

        public Task UpdateDrone(IDrone drone)
        {
            return DroneRepositoryField.UpdateDrone(drone);
        }

        public Task DeleteDrone(IIdentity id)
        {
            return DroneRepositoryField.DeleteDrone(id);
        }
    }
}
