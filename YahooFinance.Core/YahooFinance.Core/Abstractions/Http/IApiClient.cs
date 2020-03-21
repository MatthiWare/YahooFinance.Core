using MatthiWare.YahooFinance.Core.Helpers;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Abstractions.Http
{
    public interface IApiClient : IDisposable
    {
        Task<IApiResponse<ReturnType>> ExecuteAsync<ReturnType>(
            string urlPattern, QueryStringBuilder qsb)
            where ReturnType : class;
    }
}
