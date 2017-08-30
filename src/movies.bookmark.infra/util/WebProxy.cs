using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace movies.bookmarks.infra.util
{
    public class WebProxy : IWebProxy
    {
        private readonly Uri _uri;

        public WebProxy(string proxyUri)
        {
            _uri = new Uri(proxyUri);
        }
        public Uri GetProxy(Uri destination) => _uri;

        public bool IsBypassed(Uri host) => false;

        public ICredentials Credentials { get; set; }
    }
}
