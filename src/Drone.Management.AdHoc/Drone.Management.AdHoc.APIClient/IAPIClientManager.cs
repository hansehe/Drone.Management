using System;
using System.Threading.Tasks;

namespace Drone.Management.AdHoc.APIClient
{
    public interface IAPIClientManager
    {
        Task ExecuteAPIClients(long numberOfTestDatas, Uri baseAddress);
    }
}
