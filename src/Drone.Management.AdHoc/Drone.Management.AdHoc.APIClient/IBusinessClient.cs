using System;
using System.Net;
using System.Threading.Tasks;

namespace Drone.Management.AdHoc.APIClient
{
    public interface IBusinessClient : IHttpClient
    {
        Task<Uri> CreateObject<T>(string uri, T @object);

        Task<T> GetObject<T>(string uri);

        Task<T> UpdateObject<T>(string uri, T @object);

        Task<HttpStatusCode> DeleteObject(string uri);
    }
}
