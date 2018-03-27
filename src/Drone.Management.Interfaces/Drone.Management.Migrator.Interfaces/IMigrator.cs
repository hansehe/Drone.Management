using System.Threading.Tasks;

namespace Drone.Management.Migrator.Interfaces
{
    public interface IMigrator
    {
        Task Migrate();

        Task Connect();
    }
}
