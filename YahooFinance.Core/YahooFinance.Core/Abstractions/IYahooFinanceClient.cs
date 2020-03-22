using MatthiWare.YahooFinance.Abstractions.History;
using MatthiWare.YahooFinance.Abstractions.Quote;
using MatthiWare.YahooFinance.Abstractions.Search;
using System;

namespace MatthiWare.YahooFinance
{
    public interface IYahooFinanceClient : IDisposable
    {
        ISearchService Search { get; }
        IQuoteService Quote { get; }
        IHistoryService History { get; }
    }
}