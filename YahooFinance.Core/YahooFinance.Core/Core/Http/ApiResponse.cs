using MatthiWare.YahooFinance.Abstractions.Http;

namespace MatthiWare.YahooFinance.Core.Http
{
    public static class ApiResponse
    {
        public static IApiResponse<T> FromError<T>(IHttpMetadata metadata, string err)
            where T : class
        {
            return new ApiResponse<T>()
            {
                Metadata = metadata,
                Error = err
            };
        }

        public static IApiResponse<T> FromSucces<T>(IHttpMetadata metadata, T data)
            where T : class
        {
            return new ApiResponse<T>()
            {
                Metadata = metadata,
                Data = data
            };
        }
    }

    public class ApiResponse<TData> : IApiResponse<TData>
        where TData : class
    {
        public IHttpMetadata Metadata { get; set; }

        public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);

        public TData Data { get; set; }
    }
}
