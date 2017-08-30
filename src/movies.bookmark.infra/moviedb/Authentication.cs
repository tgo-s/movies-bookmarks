using movies.bookmarks.domain.aggregates.authentication;
using Newtonsoft.Json;
using System.Threading.Tasks;
using movies.bookmarks.infra.gateway;
using movies.bookmarks.domain.util;
using Microsoft.Extensions.Options;

namespace movies.bookmarks.infra.moviedb
{
    public class Authentication : GenericGateway
    {
        public Authentication(IOptions<AppSettings> settings) : base(settings) { }


        /// <summary>
        /// Request a TheMovieDB token for use on the app
        /// </summary>
        /// <returns></returns>
        public async Task<Token> RequestTokenAsync()
        {
            string urlParameter = $"{AppSettings.Value.MovieApiSettings.AuthAddresses.RequestTokenUrl}?api_key={AppSettings.Value.MovieApiSettings.ApiKey}";

            var response = await base.RequestExternalApiJson<Token>(urlParameter);

            return response;
        }
        /// <summary>
        /// Create the URL responsable for ask user's permission.
        /// </summary>
        /// <param name="token">A token recieved from TheMovieDB API</param>
        /// <param name="redirectTo">URL to redirect after user's approval</param>
        /// <returns></returns>
        public string RequestUserPermissionUrl(string token, string redirectTo)
        {
            string url = string.Empty;
            url = $"{AppSettings.Value.MovieApiSettings.AuthAddresses.UserAuthorizationBaseUrl}{AppSettings.Value.MovieApiSettings.AuthAddresses.RequestUserAuthorizationUrl}{token}?redirect_to={redirectTo}";

            return url;
        }

        public async Task<ApiSession> CreateSession()
        {
            string urlParameter = $"{AppSettings.Value.MovieApiSettings.AuthAddresses.CreateSessionUrl}?api_key={AppSettings.Value.MovieApiSettings.ApiKey}";

            var response = await base.RequestExternalApiJson<ApiSession>(urlParameter);

            return response;
        }
        public async Task<ApiGuestSession> CreateGuestSession()
        {
            string urlParameter = $"{AppSettings.Value.MovieApiSettings.AuthAddresses.CreateGuestSessionUrl}?api_key={AppSettings.Value.MovieApiSettings.ApiKey}";

            var response = await base.RequestExternalApiJson<ApiGuestSession>(urlParameter);

            return response;
        }
    }
}
