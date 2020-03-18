using MatthiWare.YahooFinance.Core.Helpers;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Abstractions.Http
{
    public interface IApiClient
    {
        Task<IApiResponse<ReturnType>> ExecuteAsync<ReturnType>(
            string urlPattern, NameValueCollection pathNVC, QueryStringBuilder qsb)
            where ReturnType : class;
    }
}
