using MatthiWare.YahooFinance.Abstractions.Http;
using System;
using System.Net;

namespace MatthiWare.YahooFinance.Core.Http
{
    public class HttpMetadata : IHttpMetadata
    {
        public HttpMetadata(HttpStatusCode code, string reason, TimeSpan time)
            => (StatusCode, ReasonPhrase, ResponseTime) = (code, reason, time);

        public HttpStatusCode StatusCode { get;  }

        public string ReasonPhrase { get;  }

        public TimeSpan ResponseTime { get; }
    }
}
