using System.Net.Http;

namespace Drone.Management.AdHoc.APIClient
{
    public interface IHttpClientWrapper : IHttpClient
    {
        HttpClient HttpClientField { get; }
    }
}