using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.TestData;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.AdHoc.Migrator
{
    public class TestDataMigratorManager : ITestDataMigratorManager
    {
        public long NumberOfDataSets { get; set; } = 10;

        public bool VerifyMigratedTestData { get; set; } = true;

        private readonly IEnumerable<ITestDataMigrator> SortedTestDataMigratorsField;

        private readonly ITestData TestDataField;

        public TestDataMigratorManager(IEnumerable<ITestDataMigrator> testDataMigrators, ITestData testData)
        {
            SortedTestDataMigratorsField = testDataMigrators.OrderBy(x => x.Queue).ToList();
            TestDataField = testData;
        }

        public async Task<IMigrator> BuildMigrator(ISettings settings)
        {
            foreach (var testDataMigrator in SortedTestDataMigratorsField)
            {
                await testDataMigrator.BuildMigrator(settings);
            }
            return this;
        }

        public async Task Migrate()
        {
            TestDataField.GenerateDataSets(NumberOfDataSets);
            foreach (var testDataMigrator in SortedTestDataMigratorsField)
            {
                testDataMigrator.SetTestData(TestDataField);
                await testDataMigrator.Migrate();
            }
            if (VerifyMigratedTestData)
            {
                foreach (var testDataMigrator in SortedTestDataMigratorsField)
                {
                    await testDataMigrator.VerifyMigratedTestData();
                }
            }
        }

        public async Task Connect()
        {
        }
    }
}
