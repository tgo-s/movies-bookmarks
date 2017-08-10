using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using movies.bookmarks.domain.util;

namespace movies.bookmarks.infra.gateway
{
    public abstract class GenericGateway 
    {
        public IOptions<AppSettings> settings;
        public GenericGateway(IOptions<AppSettings> settings)
        {
            this.settings = settings;
        }
        public async Task<T> RequestExternalApiJson<T>(string apiBaseUrl, string urlParameters) where T : class
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    var response = await client.GetAsync(urlParameters);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(stringResult);
                }
                catch (HttpRequestException httpRequestException)
                {
                    //return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                    throw httpRequestException;
                }
            }
        }
    }
}
