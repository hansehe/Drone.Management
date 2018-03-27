using System;
using Drone.Management.AdHoc.APIClient;
using Xunit;

namespace Drone.Management.AdHoc.Tests
{
    public class HttpClientWrapperTests
    {
        public static IHttpClientWrapper GetHttClientWrapper()
        {
            var httpclientWrapper = new HttpClientWrapper();
            return httpclientWrapper;
        }

        [Fact]
        public void GetHttpClient_Success()
        {
            var httpClientWrapper = GetHttClientWrapper();
            var httpClient = httpClientWrapper.HttpClientField;
        }

        [Fact]
        public void SetupHttpClient_Success()
        {
            var httpClientWrapper = GetHttClientWrapper();
            httpClientWrapper.SetupHttpClient(new Uri("http://baseAddress"));
        }
    }
}
