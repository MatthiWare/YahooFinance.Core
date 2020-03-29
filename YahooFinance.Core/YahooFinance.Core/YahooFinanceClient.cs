using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using MatthiWare.YahooFinance.Abstractions.History;
using MatthiWare.YahooFinance.Abstractions.Quote;
using MatthiWare.YahooFinance.Abstractions.Search;
using MatthiWare.YahooFinance.Core.History;
using MatthiWare.YahooFinance.Core.Quote;
using MatthiWare.YahooFinance.Core.Search;
using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Http;
using Microsoft.Extensions.Logging.Abstractions;

namespace MatthiWare.YahooFinance
{
    /// <summary>
    /// Yahoo! Finance API Client
    /// </summary>
    public class YahooFinanceClient : IYahooFinanceClient
    {
        private const string BaseUrl = "https://query1.finance.yahoo.com/";

        private readonly ILogger<YahooFinanceClient> logger;

        private readonly Lazy<ISearchService> searchServiceLazy;
        private readonly Lazy<IQuoteService> quoteServiceLazy;
        private readonly Lazy<IHistoryService> historyServiceLazy;
        private readonly HttpClient client;
        private readonly IApiClient apiClient;

        public YahooFinanceClient()
            : this(NullLogger<YahooFinanceClient>.Instance)
        {
            
        }

        public YahooFinanceClient(ILogger<YahooFinanceClient> logger)
        {
            this.logger = logger ?? NullLogger<YahooFinanceClient>.Instance;

            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36");

            apiClient = new ApiClient(client, logger);

            searchServiceLazy = new Lazy<ISearchService>(() => new SearchService(apiClient, logger));
            quoteServiceLazy = new Lazy<IQuoteService>(() => new QuoteService(apiClient, logger));
            historyServiceLazy = new Lazy<IHistoryService>(() => new HistoryService(apiClient, logger));
        }

        /// <summary>
        /// Search for a ticker symbol or ISIN
        /// </summary>
        public ISearchService Search => searchServiceLazy.Value;

        /// <summary>
        /// Look up quotes
        /// </summary>
        public IQuoteService Quote => quoteServiceLazy.Value;

        /// <summary>
        /// Historical data
        /// </summary>
        public IHistoryService History => historyServiceLazy.Value;

        public void Dispose()
        {
            apiClient.Dispose();
        }
    }
}
