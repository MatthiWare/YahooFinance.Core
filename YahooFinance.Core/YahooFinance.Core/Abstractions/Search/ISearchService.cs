using MatthiWare.YahooFinance.Core.Search;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Abstractions.Search
{
    /// <summary>
    /// https://query1.finance.yahoo.com/v1/finance/search?q=jfsmfjmfjqmfsqmfsqffs
    /// </summary>
    public interface ISearchService
    {
        Task<IEnumerable<QuoteResult>> SearchAsync(string search, CancellationToken cancellationToken = default);
    }
}
