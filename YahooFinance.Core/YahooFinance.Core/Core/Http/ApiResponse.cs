using MatthiWare.YahooFinance.Abstractions.Http;

namespace MatthiWare.YahooFinance.Core.Http
{
    public class ApiResponse<TData> : IApiResponse<TData>
        where TData : class
    {
        public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);

        public TData Data { get; set; }
    }
}
