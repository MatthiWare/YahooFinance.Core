using MatthiWare.YahooFinance.Core.Abstractions.Search;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Search
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient client;
        private readonly ILogger logger;

        public SearchService(HttpClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public Task<IEnumerable<QuoteResult>> SearchAsync(string search, CancellationToken cancellationToken = default)
        {
            
        }
    }
}
