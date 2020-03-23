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
using System.Runtime.CompilerServices;

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
            if (start >= end)
                return ApiResponse.FromError<IReadOnlyList<DividendResult>>(null, "Start date needs to happen before end date");

            var apiResult = await GetData<DividendResult>(symbol, start, end, interval, cancellationToken);

            logger.LogDebug("{Svc}::GetDividendsAsync completed in {ResponseTime} with status code {StatusCode} reason {ReasonPhrase}", nameof(HistoryService), apiResult.Metadata.ResponseTime, apiResult.Metadata.StatusCode, apiResult.Metadata.ReasonPhrase);

            if (apiResult.HasError)
                return ApiResponse.FromError<IReadOnlyList<DividendResult>>(apiResult.Metadata, apiResult.Error);

            var results = (IReadOnlyList<DividendResult>)(apiResult.Data
                .Where(d => d.Dividends != 0)
                .OrderByDescending(d => d.Date)
                .ToList());

            logger.LogDebug("HistoryService::GetDividendsAsync returns SUCCES - found {count} results", results.Count);

            return ApiResponse.FromSucces(apiResult.Metadata, results);
        }

        public async Task<IApiResponse<IReadOnlyList<HistoryResult>>> GetPricesAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default)
        {
            if (start >= end)
                return ApiResponse.FromError<IReadOnlyList<HistoryResult>>(null, "Start date needs to happen before end date");

            var apiResult = await GetData<HistoryResult>(symbol, start, end, interval, cancellationToken);

            logger.LogDebug("{Svc}::GetPricesAsync completed in {ResponseTime} with status code {StatusCode} reason {ReasonPhrase}", nameof(HistoryService), apiResult.Metadata.ResponseTime, apiResult.Metadata.StatusCode, apiResult.Metadata.ReasonPhrase);

            if (apiResult.HasError)
                return ApiResponse.FromError<IReadOnlyList<HistoryResult>>(apiResult.Metadata, apiResult.Error);

            var results = (IReadOnlyList<HistoryResult>)(apiResult.Data
                .Where(d => d.AdjustedClose != 0 && d.Close != 0 && d.Open != 0)
                .OrderByDescending(d => d.Date)
                .ToList());

            logger.LogDebug("HistoryService::GetPricesAsync returns SUCCES - found {count} results", results.Count);

            return ApiResponse.FromSucces(apiResult.Metadata, results);
        }

        public async Task<IApiResponse<IReadOnlyList<SplitResult>>> GetSplitsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default)
        {
            if (start >= end)
                return ApiResponse.FromError<IReadOnlyList<SplitResult>>(null, "Start date needs to happen before end date");

            var apiResult = await GetData<SplitResult>(symbol, start, end, interval, cancellationToken);

            logger.LogDebug("{Svc}::GetSplitsAsync completed in {ResponseTime} with status code {StatusCode} reason {ReasonPhrase}", nameof(HistoryService), apiResult.Metadata.ResponseTime, apiResult.Metadata.StatusCode, apiResult.Metadata.ReasonPhrase);

            if (apiResult.HasError)
                return ApiResponse.FromError<IReadOnlyList<SplitResult>>(apiResult.Metadata, apiResult.Error);

            var results = (IReadOnlyList<SplitResult>)(apiResult.Data
                .Where(d => d.Split.Before != 0 && d.Split.After != 0)
                .OrderByDescending(d => d.Date)
                .ToList());

            logger.LogDebug("HistoryService::GetSplitsAsync returns SUCCES - found {count} results", results.Count);

            return ApiResponse.FromSucces(apiResult.Metadata, results);
        }

        private async Task<IApiResponse<IEnumerable<T>>> GetData<T>(string symbol, Instant start, Instant end, HistoryInterval interval, CancellationToken cancellationToken)
            where T : class, IHistoryItem
        {
            const string url = "v7/finance/download/[symbol]";

            var qsb = new QueryStringBuilder();
            qsb.Add("interval", IntervalToString(interval));
            qsb.Add("period1", start.ToUnixTimeSeconds());
            qsb.Add("period2", end.ToUnixTimeSeconds());
            qsb.Add("events", GetEventTypeFromHistoryItem<T>());

            var nvc = new NameValueCollection()
            {
                { "symbol", symbol }
            };

            return await client.ExecuteCsvAsync<T>(url, nvc, qsb);
        }

        private string GetEventTypeFromHistoryItem<T>()
            where T : IHistoryItem
        {
            var type = typeof(T);
            if (type == typeof(HistoryResult))
                return "history";
            else if (type == typeof(DividendResult))
                return "div";
            else if (type == typeof(SplitResult))
                return "split";
            else
                throw new NotImplementedException("No event type for given history item");
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
