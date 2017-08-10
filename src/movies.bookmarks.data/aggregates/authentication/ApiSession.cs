using movies.bookmarks.domain.aggregates.generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace movies.bookmarks.domain.aggregates.authentication
{
    public class ApiSession : GenericApiAggregate
    {

        [JsonProperty("session_id")]
        public int SessionId { get; set; }
        
    }
}
