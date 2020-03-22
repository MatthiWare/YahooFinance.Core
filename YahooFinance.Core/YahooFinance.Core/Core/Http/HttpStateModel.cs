using MatthiWare.YahooFinance.Abstractions.Http;
using System;

namespace MatthiWare.YahooFinance.Core.Http
{
    public class HttpStateModel
    {
        public IHttpMetadata Meta { get; }
        public string Response { get; }
        public bool Error { get; }

        public HttpStateModel(IHttpMetadata meta, string response, bool error)
        {
            Meta = meta ?? throw new ArgumentNullException(nameof(meta));
            Response = response ?? throw new ArgumentNullException(nameof(response));
            Error = error;
        }
    }
}
