using System;
using System.Net;

namespace MatthiWare.YahooFinance.Abstractions.Http
{
    public interface IHttpMetadata
    {
        HttpStatusCode StatusCode { get; }
        string ReasonPhrase { get; }
        TimeSpan ResponseTime { get; }
    }
}
