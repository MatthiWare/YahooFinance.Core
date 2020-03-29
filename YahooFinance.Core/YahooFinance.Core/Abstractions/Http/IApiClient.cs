using MatthiWare.YahooFinance.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Abstractions.Http
{
    public interface IApiClient : IDisposable
    {
        Task<IApiResponse<IEnumerable<ReturnType>>> ExecuteCsvAsync<ReturnType>(
            string urlPattern, NameValueCollection nvc, QueryStringBuilder qsb, CancellationToken cancellationToken)
            where ReturnType : class;

        Task<IApiResponse<ReturnType>> ExecuteJsonAsync<ReturnType>(
            string urlPattern, QueryStringBuilder qsb, CancellationToken cancellationToken)
            where ReturnType : class;
    }
}
