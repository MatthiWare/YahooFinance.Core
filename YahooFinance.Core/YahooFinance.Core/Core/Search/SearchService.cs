using AutoMapper;
using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Abstractions.Search;
using MatthiWare.YahooFinance.Core.Helpers;
using MatthiWare.YahooFinance.Core.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Search
{
    public class SearchService : ISearchService
    {
        private readonly IApiClient client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public SearchService(IApiClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;

            var mapperConfig = new MapperConfiguration(_ =>
            {
                _.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                _.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                _.CreateMap<QuoteResultResponse, QuoteResult>();
            });

            mapper = mapperConfig.CreateMapper();

            logger.LogDebug("Created SearchService instance");
        }

        public async Task<IApiResponse<IReadOnlyList<QuoteResult>>> SearchAsync(string search, CancellationToken cancellationToken = default)
        {
            const string url = "v1/finance/search";

            var qsb = new QueryStringBuilder();
            qsb.Add("q", search);

            var apiResult = await client.ExecuteJsonAsync<SearchResultResponse>(url, qsb);

            logger.LogDebug("SearchService::SearchAsync completed in {ResponseTime} with status code {StatusCode} reason {ReasonPhrase}", apiResult.Metadata.ResponseTime, apiResult.Metadata.StatusCode, apiResult.Metadata.ReasonPhrase);

            if (apiResult.HasError)
                return ApiResponse.FromError<IReadOnlyList<QuoteResult>>(apiResult.Metadata, apiResult.Error);

            var results = (IReadOnlyList<QuoteResult>)(apiResult.Data.quotes.OrderByDescending(q => q.score).Select(r => mapper.Map<QuoteResult>(r)).ToList());

            logger.LogDebug("SearchService::SearchAsync returns SUCCES - found {count} results", results.Count);

            return ApiResponse.FromSucces(apiResult.Metadata, results);
            
        }
    }
}
