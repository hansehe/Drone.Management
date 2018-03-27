using Drone.Management.Migrator.Interfaces;

namespace Drone.Management.AdHoc.Migrator
{
    public interface ITestDataMigratorManager : IMigrator, IMigratorBuilder
    {
        long NumberOfDataSets { get; set; }

        bool VerifyMigratedTestData { get; set; }
    }
}
