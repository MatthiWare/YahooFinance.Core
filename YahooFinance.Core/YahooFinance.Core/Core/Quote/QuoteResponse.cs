using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatthiWare.YahooFinance.Core.Quote
{
    public class QuoteResponse
    {
        [JsonProperty("result")]
        public List<SecurityResult> Result { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorResponse Error { get; set; }
    }
}
