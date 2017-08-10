using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace movies.bookmarks.domain.aggregates.generic
{
    public class GenericApiAggregate
    {
        public bool Success { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
    }
}
