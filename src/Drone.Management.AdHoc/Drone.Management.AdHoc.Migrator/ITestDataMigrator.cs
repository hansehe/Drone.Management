using System.Threading.Tasks;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Migrator.Interfaces;

namespace Drone.Management.AdHoc.Migrator
{
    public interface ITestDataMigrator : IMigrator, IMigratorBuilder
    {
        int Queue { get; }

        void SetTestData(ITestData testData);

        Task VerifyMigratedTestData();

        Task DeleteMigratedTestData();
    }
}
