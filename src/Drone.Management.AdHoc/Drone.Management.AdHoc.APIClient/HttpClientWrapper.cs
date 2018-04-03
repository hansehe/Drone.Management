using System;
using System.Net.Http;

namespace Drone.Management.AdHoc.APIClient
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpClient HttpClientField { get; }

        public HttpClientWrapper()
        {
            HttpClientField = new HttpClient();
        }

        public void SetupHttpClient(Uri baseAddress)
        {
            if (HttpClientField.BaseAddress == null)
            {
                HttpClientField.BaseAddress = baseAddress;
            }
        }
    }
}
