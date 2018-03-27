using System.Threading.Tasks;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Migrator.Interfaces
{
    public interface IMigratorResolver : IMigrator
    {
        bool CanMigrate(ISettings settings);

        Task SetupMigration(ISettings settings);
    }
}
