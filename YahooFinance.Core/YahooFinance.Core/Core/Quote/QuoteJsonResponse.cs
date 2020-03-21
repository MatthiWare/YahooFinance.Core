using Newtonsoft.Json;

namespace MatthiWare.YahooFinance.Core.Quote
{
    public class QuoteJsonResponse
    {
        [JsonProperty("quoteResponse")]
        public QuoteResponse QuoteResponse { get; set; }
    }
}
