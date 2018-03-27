using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.Interfaces
{
    public interface IRepositoryResolver : IRepository
    {
        bool CanUseRepository(ISettings settings);
    }
}
