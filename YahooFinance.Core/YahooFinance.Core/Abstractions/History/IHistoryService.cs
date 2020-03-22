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
    /// https://query1.finance.yahoo.com/v7/finance/download/DFN.TO?period1=1426982400&period2=1584835200&interval=1d&events=div
    /// </summary>
    public interface IHistoryService
    {
        Task<IApiResponse<IReadOnlyList<DividendResult>>> GetDividendsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default);
        Task<IApiResponse<IReadOnlyList<DividendResult>>> GetPricesAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default);
        Task<IApiResponse<IReadOnlyList<DividendResult>>> GetSplitsAsync(string symbol, Instant start, Instant end, HistoryInterval interval = HistoryInterval.Daily, CancellationToken cancellationToken = default);
    }
}
