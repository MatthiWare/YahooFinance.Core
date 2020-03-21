using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Quote;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Abstractions.Quote
{
    /// <summary>
    /// https://query1.finance.yahoo.com/v7/finance/quote?symbols=O,MSFT
    /// </summary>
    public interface IQuoteService
    {
        Task<IApiResponse<IReadOnlyList<SecurityResult>>> LookupAsync(IEnumerable<string> symbols, CancellationToken cancellationToken = default);
        Task<IApiResponse<IReadOnlyList<SecurityResult>>> LookupAsync(string symbol, CancellationToken cancellationToken = default);
    }
}
