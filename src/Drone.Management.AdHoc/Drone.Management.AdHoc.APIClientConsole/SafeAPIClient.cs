using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.APIClient.BusinessClients;
using Drone.Management.AdHoc.TestData;

namespace Drone.Management.AdHoc.APIClientConsole
{
    internal static class SafeAPIClient
    {
        internal static long NumberOfTestDatas = 10;
        internal static string BaseAddress = "http://localhost:9718/";

        private const int MaxConnectionTries = 0;
        private const int MsSleepBetweenTries = 500;

        internal static async Task ExecuteAPIClientWithExceptionHandling(
            IDroneBusinessClient droneBusinessClient, 
            ITestData testData, 
            int tryLevel = 0, int maxConnectionTries = MaxConnectionTries)
        {
            try
            {
                await ExecuteAPIClient(droneBusinessClient, testData);
            }
            catch (Exception e)
            {
                if (tryLevel >= maxConnectionTries)
                {
                    throw;
                }
                tryLevel++;
                Console.WriteLine(e + $" - Will try to connect to API {maxConnectionTries - tryLevel} more times..");
                System.Threading.Thread.Sleep(MsSleepBetweenTries);
                await ExecuteAPIClientWithExceptionHandling(droneBusinessClient, testData, tryLevel, maxConnectionTries);
            }
        }

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
