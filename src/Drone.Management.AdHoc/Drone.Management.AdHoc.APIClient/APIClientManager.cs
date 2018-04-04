using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drone.Management.AdHoc.TestData;

namespace Drone.Management.AdHoc.APIClient
{
    public class APIClientManager : IAPIClientManager
    {
        private readonly IEnumerable<IAPIClient> SortedApiClientsField;

        private readonly ITestData TestDataField;

        public APIClientManager(IEnumerable<IAPIClient> apiClients, ITestData testData)
        {
            SortedApiClientsField = apiClients.OrderBy(x => x.Queue).ToList();
            TestDataField = testData;
        }

        public async Task ExecuteAPIClients(long numberOfTestDatas, Uri baseAddress)
        {
            TestDataField.GenerateDataSets(numberOfTestDatas);
            foreach (var apiClient in SortedApiClientsField)
            {
                await apiClient.ExecuteAPIClient(TestDataField, baseAddress);
            }
        }
    }
}
