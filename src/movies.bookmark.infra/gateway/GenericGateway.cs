using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using movies.bookmarks.domain.util;
using System.Net;
using movies.bookmarks.infra.util;
using Newtonsoft.Json.Converters;

namespace movies.bookmarks.infra.gateway
{
    public abstract class GenericGateway 
    {
        public IOptions<AppSettings> AppSettings { get; set; }
        public GenericGateway(IOptions<AppSettings> settings)
        {
            this.AppSettings = settings;
        }

        public async Task<T> RequestExternalApiJson<T>(string urlParameters) where T : class
        {
            return await RequestExternalApiJson<T>(AppSettings.Value.MovieApiSettings.ApiBaseUrl, urlParameters, AppSettings.Value.UseProxy);
        }

        public async Task<T> RequestExternalApiJson<T>(string apiBaseUrl, string urlParameters, bool useProxy) where T : class
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            if (useProxy)
            {
                string proxyUri = string.Format("{0}:{1}", AppSettings.Value.ProxyServerSettings.Address, AppSettings.Value.ProxyServerSettings.Port);

                NetworkCredential proxyCreds = new NetworkCredential(
                    AppSettings.Value.ProxyServerSettings.Username,
                    AppSettings.Value.ProxyServerSettings.Password
                );

                WebProxy proxy = new WebProxy(proxyUri)
                {
                    Credentials = proxyCreds,
                };

                httpClientHandler.Proxy = proxy;
                httpClientHandler.UseProxy = true;
                httpClientHandler.PreAuthenticate = true;
                httpClientHandler.UseDefaultCredentials = false;
                //httpClientHandler.Credentials = proxyCreds;
            }
            using (var client = new HttpClient(httpClientHandler))
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);

                    var response = await client.GetAsync(urlParameters);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();

                    //The api returns a datetime format with the chars 'UTC'. 
                    //Couldn't figure out how to correctly convert/parse the value
                    //In this case... workaround the problem
                    stringResult = stringResult.Replace("UTC", "");

                    var jsonDateTimeSettings = new JsonSerializerSettings {  DateParseHandling = DateParseHandling.DateTimeOffset, DateFormatHandling = DateFormatHandling.IsoDateFormat, DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind }; //new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" };

                    return JsonConvert.DeserializeObject<T>(stringResult, jsonDateTimeSettings);
                }
                catch (HttpRequestException httpRequestException)
                {
                    //return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                    throw httpRequestException;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
