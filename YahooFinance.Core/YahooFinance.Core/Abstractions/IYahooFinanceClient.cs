using MatthiWare.YahooFinance.Core.Abstractions.History;
using MatthiWare.YahooFinance.Core.Abstractions.Quote;
using MatthiWare.YahooFinance.Core.Abstractions.Search;

namespace MatthiWare.YahooFinance.Core
{
    public interface IYahooFinanceClient
    {
        ISearchService Search { get; }
        IQuoteService Quote { get; }
        IHistoryService History { get; }
    }
}