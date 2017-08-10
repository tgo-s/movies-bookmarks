using movies.bookmarks.domain.aggregates.generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace movies.bookmarks.domain.aggregates.authentication
{
    public class ApiGuestSession : GenericApiAggregate
    {
        [JsonProperty("guest_session_id")]
        public string GuestSessionId { get; set; }
    }
}
