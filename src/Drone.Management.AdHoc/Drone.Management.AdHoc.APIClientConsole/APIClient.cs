using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.AdHoc.TestData;

namespace Drone.Management.AdHoc.APIClientConsole
{
    internal static class APIClient
    {
        internal static long NumberOfTestDatas = 10;
        internal static string BaseAddress = "http://localhost:9718/";
        
        internal static async Task ExecuteAPIClient(
            IDroneBusinessClient droneBusinessClient, 
            ITestData testData)
        {
            if (long.TryParse(Environment.GetEnvironmentVariable("N_TEST_DATAS"), out var nDatas))
            {
                NumberOfTestDatas = nDatas;
            }
            var baseAddress = new Uri(Environment.GetEnvironmentVariable("API_BASE_ADDRESS") ?? BaseAddress);
            testData.GenerateDataSets(NumberOfTestDatas);
            await DroneAPIClient.ExecuteDroneAPIClient(droneBusinessClient, testData, baseAddress);
        }
    }
}
