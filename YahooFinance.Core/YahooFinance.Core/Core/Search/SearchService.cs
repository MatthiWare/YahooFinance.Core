using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Abstractions.Search;
using MatthiWare.YahooFinance.Core.Helpers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Search
{
    public class SearchService : ISearchService
    {
        private readonly IApiClient client;
        private readonly ILogger logger;

        public SearchService(IApiClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<IApiResponse<IEnumerable<QuoteResult>>> SearchAsync(string search, CancellationToken cancellationToken = default)
        {
            const string url = "v1/finance/search";

            var qsb = new QueryStringBuilder();
            qsb.Add("q", search);

            return await client.ExecuteAsync<IEnumerable<QuoteResult>>(url, null, qsb);
        }
    }
}
