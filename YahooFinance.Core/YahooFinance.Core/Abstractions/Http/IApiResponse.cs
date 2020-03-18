namespace MatthiWare.YahooFinance.Abstractions.Http
{
    public interface IApiResponse<TData> 
        where TData : class
    {
        string Error { get; }
        bool HasError { get; }
        TData Data { get; }
    }
}
