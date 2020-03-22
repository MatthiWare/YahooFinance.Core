using MatthiWare.YahooFinance.Abstractions.Http;
using System;
using System.Net;

namespace MatthiWare.YahooFinance.Core.Http
{
    public class ErrorHttpMetadata : IHttpMetadata
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ReasonPhrase => "Client side request failure";

        public TimeSpan ResponseTime => TimeSpan.Zero;
    }
}
