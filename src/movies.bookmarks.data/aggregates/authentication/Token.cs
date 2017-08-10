using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using movies.bookmarks.domain.aggregates.generic;

namespace movies.bookmarks.domain.aggregates.authentication
{
    public class Token : GenericApiAggregate
    {
        [JsonProperty("request_token")]
        public string RequestToken { get; set; }
    }
}
