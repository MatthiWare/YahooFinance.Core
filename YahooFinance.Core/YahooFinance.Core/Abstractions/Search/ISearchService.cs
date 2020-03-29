using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Search;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Abstractions.Search
{
    /// <summary>
    /// https://query1.finance.yahoo.com/v1/finance/search?q=O
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Search for a symbol, returns a list of matching results.
        /// First result is the best matching result.
        /// </summary>
        /// <param name="search">search phrase</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IApiResponse<IReadOnlyList<QuoteResult>>> SearchAsync(string search, CancellationToken cancellationToken = default);
    }
}
