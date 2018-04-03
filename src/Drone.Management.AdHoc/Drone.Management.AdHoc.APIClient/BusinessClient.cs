using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Drone.Management.AdHoc.APIClient
{
    public class BusinessClient : IBusinessClient
    {
        private readonly IHttpClientWrapper HttpClientWrapperField;

        public BusinessClient(IHttpClientWrapper httpClientWrapper)
        {
            HttpClientWrapperField = httpClientWrapper;
        }

        public void SetupHttpClient(Uri baseAddress)
        {
            HttpClientWrapperField.SetupHttpClient(baseAddress);
        }

        public async Task<Uri> CreateObject<T>(string uri, T @object)
        {
            var response = await HttpClientWrapperField.HttpClientField.PostAsJsonAsync(uri, @object);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public async Task<T> GetObject<T>(string uri)
        {
            var @object = default(T);
            var response = await HttpClientWrapperField.HttpClientField.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                @object = await response.Content.ReadAsAsync<T>();
            }
            return @object;
        }

        public async Task<Uri> UpdateObject<T>(string uri, T @object)
        {
            var response = await HttpClientWrapperField.HttpClientField.PutAsJsonAsync(uri, @object);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public async Task<HttpStatusCode> DeleteObject(string uri)
        {
            var response = await HttpClientWrapperField.HttpClientField.DeleteAsync(uri);
            return response.StatusCode;
        }
    }
}
