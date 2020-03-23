namespace MatthiWare.YahooFinance.Abstractions.Http
{
    public interface IApiResponse<TData> 
        where TData : class
    {
        /// <summary>
        /// API Call Metadata
        /// </summary>
        IHttpMetadata Metadata { get; }

        /// <summary>
        /// Error message
        /// </summary>
        string Error { get; }

        /// <summary>
        /// Indicates if the response is in error state
        /// </summary>
        bool HasError { get; }

        /// <summary>
        /// Data result
        /// </summary>
        TData Data { get; }
    }
}
