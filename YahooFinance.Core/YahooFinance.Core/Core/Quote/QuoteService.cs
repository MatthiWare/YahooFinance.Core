using AutoMapper;
using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Abstractions.Quote;
using MatthiWare.YahooFinance.Core.Helpers;
using MatthiWare.YahooFinance.Core.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Quote
{
    public class QuoteService : IQuoteService
    {
        private readonly IApiClient client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public QuoteService(IApiClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;

            var mapperConfig = new MapperConfiguration(_ =>
            {
                _.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                _.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            });

            mapper = mapperConfig.CreateMapper();

            logger.LogDebug("Created QuoteService instance");
        }

        public async Task<IApiResponse<IReadOnlyList<SecurityResult>>> LookupAsync(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
            => await LookupAsync(string.Join(",", symbols.Distinct()), cancellationToken);

        public async Task<IApiResponse<IReadOnlyList<SecurityResult>>> LookupAsync(string symbol, CancellationToken cancellationToken = default)
        {
            const string url = "v7/finance/quote";

            var qsb = new QueryStringBuilder();
            qsb.Add("symbols", symbol);

            var apiResult = await client.ExecuteJsonAsync<QuoteJsonResponse>(url, qsb);

            logger.LogDebug("QuoteService::LookupAsync completed in {ResponseTime} with status code {StatusCode} reason {ReasonPhrase}", apiResult.Metadata.ResponseTime, apiResult.Metadata.StatusCode, apiResult.Metadata.ReasonPhrase);

            if (apiResult.HasError)
                return ApiResponse.FromError<IReadOnlyList<SecurityResult>>(apiResult.Metadata, apiResult.Error);

            if (apiResult.Data.QuoteResponse.Error != null)
                return ApiResponse.FromError<IReadOnlyList<SecurityResult>>(apiResult.Metadata, apiResult.Data.QuoteResponse.Error.Description);

            var results = (IReadOnlyList<SecurityResult>)(apiResult.Data.QuoteResponse.Result);

            logger.LogDebug("QuoteService::LookupAsync returns SUCCES - found {count} results", results.Count);

            return ApiResponse.FromSucces(apiResult.Metadata, results);
        }
    }
}
