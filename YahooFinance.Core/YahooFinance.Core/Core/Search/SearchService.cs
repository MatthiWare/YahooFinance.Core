using AutoMapper;
using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Abstractions.Search;
using MatthiWare.YahooFinance.Core.Helpers;
using MatthiWare.YahooFinance.Core.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IApiResponse<IReadOnlyList<QuoteResult>>> SearchAsync(string search, CancellationToken cancellationToken = default)
        {
            const string url = "v1/finance/search";

            var qsb = new QueryStringBuilder();
            qsb.Add("q", search);

            var apiResult = await client.ExecuteAsync<SearchResultResponse>(url, null, qsb);

            if (apiResult.HasError) 
                return new ApiResponse<IReadOnlyList<QuoteResult>>() { Error = apiResult.Error };

            var cfg = new MapperConfiguration(_ => 
            {
                _.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                _.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                _.CreateMap<QuoteResultResponse, QuoteResult>();
            });

            var mapper = cfg.CreateMapper();

            var results = apiResult.Data.quotes.OrderByDescending(q => q.score).Select(r => mapper.Map<QuoteResult>(r)).ToList();

            return new ApiResponse<IReadOnlyList<QuoteResult>> { Data = results };
            
        }
    }
}
