using System;
using System.Collections.Generic;
using System.Text;

namespace movies.bookmarks.domain.util
{
    public class AppSettings
    {
        public MovieApiSettings MovieApiSettings { get; set; }
        public LoggingSettings Logging { get; set; }
    }

    public class LoggingSettings
    {
        public bool IncludeScopes { get; set; }
        public LogLevelSettings LogLevel { get; set; }
    }

    public class LogLevelSettings
    {
        public string Deffault { get; set; }
    }

    public class MovieApiSettings
    {
        public string ApiKey { get; set; }
        public string ApiBaseUrl { get; set; } 
        public MovieApiAuthAddresses AuthAddresses { get; set; }
        public MovieApiMoviesAddresses MovieAddresses { get; set; }


    }

    public class MovieApiAuthAddresses
    {
        public string RequestTokenUrl { get; set; }
        public string UserAuthorizationBaseUrl { get; set; }
        public string RequestUserAuthorizationUrl { get; set; }
        public string CreateSessionUrl { get; set; }
        public string CreateGuestSessionUrl { get; set; }
    }

    public class MovieApiMoviesAddresses
    {
        public string DiscoverMoviesUrl { get; set; }
    }
}
