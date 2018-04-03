using System;
using System.Threading.Tasks;
using Drone.Management.AdHoc.TestData;

namespace Drone.Management.AdHoc.APIClientConsole
{
    internal interface IAPIClient
    {
        int Queue { get; }

        Task ExecuteAPIClient(ITestData testData, Uri baseAddress);
    }
}
