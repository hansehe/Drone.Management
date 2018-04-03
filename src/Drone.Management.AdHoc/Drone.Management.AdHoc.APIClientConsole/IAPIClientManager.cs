using System;
using System.Threading.Tasks;

namespace Drone.Management.AdHoc.APIClientConsole
{
    internal interface IAPIClientManager
    {
        Task ExecuteAPIClients(long numberOfTestDatas, Uri baseAddress);
    }
}
