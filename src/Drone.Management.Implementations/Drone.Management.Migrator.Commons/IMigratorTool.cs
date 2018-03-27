using System.Reflection;
using System.Threading.Tasks;

namespace Drone.Management.Migrator.Commons
{
    public interface IMigratorTool
    {
        bool CanMigrate(AvailableMigratorTools migratorTool);

        Task Migrate(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly);

        Task Connect(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly);
    }
}
