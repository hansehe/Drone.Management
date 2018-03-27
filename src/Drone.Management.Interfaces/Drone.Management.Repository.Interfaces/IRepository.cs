using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.Interfaces
{
    public interface IRepository
    {
        void SetupRepository(ISettings settings);
    }
}
