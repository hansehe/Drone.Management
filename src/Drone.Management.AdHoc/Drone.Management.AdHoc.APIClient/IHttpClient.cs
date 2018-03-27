using System;

namespace Drone.Management.AdHoc.APIClient
{
    public interface IHttpClient
    {
        void SetupHttpClient(Uri baseAddress);
    }
}
