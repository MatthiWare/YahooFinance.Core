using Newtonsoft.Json;

namespace MatthiWare.YahooFinance.Core.Quote
{
    public class ErrorResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
