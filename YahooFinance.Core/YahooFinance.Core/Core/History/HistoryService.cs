using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Helpers;
using MatthiWare.YahooFinance.Core.Http;
using Microsoft.Extensions.Logging;
using NodaTime;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using MatthiWare.YahooFinance.Abstractions.History;
using MatthiWare.YahooFinance.Core.Enums;

namespace MatthiWare.YahooFinance.Core.History
{
    public class HistoryService : IHistoryService
    {
        private readonly IApiClient client;
        private readonly ILogger logger;

        public HistoryService(IApiClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;

            logger.LogDebug("Created HistoryService instance");
        }

        public async Task<IApiResponse<IReadOnlyList<DividendResult>>> GetDividendsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default)
        {
            const string url = "v7/finance/download/[symbol]";

            if (start >= end)
                return ApiResponse.FromError<IReadOnlyList<DividendResult>>(null, "Start date needs to happen before end date");

            var qsb = new QueryStringBuilder();
            qsb.Add("interval", IntervalToString(interval));
            qsb.Add("period1", start.ToUnixTimeSeconds());
            qsb.Add("period2", end.ToUnixTimeSeconds());
            qsb.Add("events", "div");

            var nvc = new NameValueCollection()
            {
                { "symbol", symbol }
            };

            var apiResult = await client.ExecuteCsvAsync<DividendResult>(url, nvc, qsb);

            logger.LogDebug("HistoryService::GetDividendsAsync completed in {ResponseTime} with status code {StatusCode} reason {ReasonPhrase}", apiResult.Metadata.ResponseTime, apiResult.Metadata.StatusCode, apiResult.Metadata.ReasonPhrase);

            if (apiResult.HasError)
                return ApiResponse.FromError<IReadOnlyList<DividendResult>>(apiResult.Metadata, apiResult.Error);

            var results = (IReadOnlyList<DividendResult>)(apiResult.Data.Where(d => d.Dividends != 0).OrderByDescending(d => d.Date).ToList());

            logger.LogDebug("HistoryService::LookupAsync returns SUCCES - found {count} results", results.Count);

            return ApiResponse.FromSucces(apiResult.Metadata, results);
        }

        public Task<IApiResponse<IReadOnlyList<DividendResult>>> GetPricesAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IApiResponse<IReadOnlyList<DividendResult>>> GetSplitsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        private string IntervalToString(HistoryInterval interval)
        {
            switch (interval)
            {
                case HistoryInterval.Daily:
                    return "1d";
                case HistoryInterval.Weekly:
                    return "1w";
                case HistoryInterval.Monthly:
                    return "1m";
                default:
                    throw new NotImplementedException($"Interval '{interval}' does not exist");
            }
        }
    }
}
