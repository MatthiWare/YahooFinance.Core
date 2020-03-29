using MatthiWare.YahooFinance.Abstractions.History;
using MatthiWare.YahooFinance.Abstractions.Quote;
using MatthiWare.YahooFinance.Abstractions.Search;
using System;

namespace MatthiWare.YahooFinance
{
    /// <summary>
    /// Yahoo! Finance API Client
    /// </summary>
    public interface IYahooFinanceClient : IDisposable
    {
        /// <summary>
        /// Search for a ticker symbol or ISIN
        /// </summary>
        ISearchService Search { get; }

        /// <summary>
        /// Look up quotes
        /// </summary>
        IQuoteService Quote { get; }

        /// <summary>
        /// Historical data
        /// </summary>
        IHistoryService History { get; }
    }
}