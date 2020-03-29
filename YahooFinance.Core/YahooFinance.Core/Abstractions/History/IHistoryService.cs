using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Enums;
using MatthiWare.YahooFinance.Core.History;
using NodaTime;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Abstractions.History
{
    /// <summary>
    /// History API
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        /// Get a list of dividends from a given period
        /// </summary>
        /// <param name="symbol">Ticker symbol e.g. DFN.TO</param>
        /// <param name="start">Start instant</param>
        /// <param name="end">End period</param>
        /// <param name="interval">Lookup interval</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IApiResponse<IReadOnlyList<DividendResult>>> GetDividendsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of prices from a given period
        /// </summary>
        /// <param name="symbol">Ticker symbol e.g. DFN.TO</param>
        /// <param name="start">Start instant</param>
        /// <param name="end">End period</param>
        /// <param name="interval">Lookup interval</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IApiResponse<IReadOnlyList<HistoryResult>>> GetPricesAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of splits from a given period
        /// </summary>
        /// <param name="symbol">Ticker symbol e.g. DFN.TO</param>
        /// <param name="start">Start instant</param>
        /// <param name="end">End period</param>
        /// <param name="interval">Lookup interval</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IApiResponse<IReadOnlyList<SplitResult>>> GetSplitsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default);
    }
}
