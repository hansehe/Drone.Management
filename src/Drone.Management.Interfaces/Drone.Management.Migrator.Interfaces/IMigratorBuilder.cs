using System.Threading.Tasks;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Migrator.Interfaces
{
    public interface IMigratorBuilder
    {
        Task<IMigrator> BuildMigrator(ISettings settings);
    }
}
