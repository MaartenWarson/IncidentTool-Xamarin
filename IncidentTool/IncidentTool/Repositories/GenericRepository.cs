using IncidentTool.Interfaces.Repositories;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();
                string jsonResult = string.Empty;

                HttpResponseMessage responseMessage = httpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                throw new HttpRequestException();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task PostAsync<T>(string uri, T data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                StringContent content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                HttpResponseMessage responseMessage = httpClient.PostAsync(uri, content).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
            }
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
