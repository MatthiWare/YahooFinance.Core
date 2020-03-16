using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace YahooFinance.Core
{
    public class YahooFinanceClient : IYahooFinanceClient
    {
        private readonly ILogger<YahooFinanceClient> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public YahooFinanceClient(ILogger<YahooFinanceClient> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            this.httpClientFactory = httpClientFactory ?? throw new System.ArgumentNullException(nameof(httpClientFactory));
        }
    }
}
