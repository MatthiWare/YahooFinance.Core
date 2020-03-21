using MatthiWare.YahooFinance.Core.Abstractions.History;
using MatthiWare.YahooFinance.Core.Abstractions.Quote;
using MatthiWare.YahooFinance.Core.Abstractions.Search;
using System;

namespace MatthiWare.YahooFinance.Core
{
    public interface IYahooFinanceClient : IDisposable
    {
        ISearchService Search { get; }
        IQuoteService Quote { get; }
        IHistoryService History { get; }
    }
}