using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.Interfaces
{
    public interface IRepositoryBuilder
    {
        IDroneRepository BuildDroneRepository(ISettings settings);
    }
}
