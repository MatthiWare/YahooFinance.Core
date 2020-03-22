﻿using MatthiWare.YahooFinance.Abstractions.Http;
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
        Task<IApiResponse<IReadOnlyList<QuoteResult>>> SearchAsync(string search, CancellationToken cancellationToken = default);
    }
}
