using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using MatthiWare.YahooFinance.Core.Abstractions.History;
using MatthiWare.YahooFinance.Core.Abstractions.Quote;
using MatthiWare.YahooFinance.Core.Abstractions.Search;
using MatthiWare.YahooFinance.Core.History;
using MatthiWare.YahooFinance.Core.Quote;
using MatthiWare.YahooFinance.Core.Search;

namespace MatthiWare.YahooFinance.Core
{
    public class YahooFinanceClient : IYahooFinanceClient
    {
        private const string BaseUrl = "https://query1.finance.yahoo.com/";

        private readonly ILogger<YahooFinanceClient> logger;

        private readonly Lazy<ISearchService> searchServiceLazy;
        private readonly Lazy<IQuoteService> quoteServiceLazy;
        private readonly Lazy<IHistoryService> historyServiceLazy;
        private readonly HttpClient client;

        private YahooFinanceClient()
        {
            searchServiceLazy = new Lazy<ISearchService>(() => new SearchService(client, logger));
            quoteServiceLazy = new Lazy<IQuoteService>(() => new QuoteService());
            historyServiceLazy = new Lazy<IHistoryService>(() => new HistoryService());
        }

        public YahooFinanceClient(ILogger<YahooFinanceClient> logger)
            : this()
        {
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };

            client.DefaultRequestHeaders.Add("User-Agent", "MatthiWare.YahooFinance.Core .NET API");
        }

        public ISearchService Search => searchServiceLazy.Value;

        public IQuoteService Quote => quoteServiceLazy.Value;

        public IHistoryService History => historyServiceLazy.Value;
    }
}
