using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Quote;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Abstractions.Quote
{
    /// <summary>
    /// https://query1.finance.yahoo.com/v7/finance/quote?symbols=O,MSFT
    /// </summary>
    public interface IQuoteService
    {
        /// <summary>
        /// Look up quotes of multiple symbols
        /// </summary>
        /// <param name="symbols">List of symbols</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IApiResponse<IReadOnlyList<SecurityResult>>> LookupAsync(IEnumerable<string> symbols, CancellationToken cancellationToken = default);

        /// <summary>
        /// Look up quotes for a symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IApiResponse<IReadOnlyList<SecurityResult>>> LookupAsync(string symbol, CancellationToken cancellationToken = default);
    }
}
