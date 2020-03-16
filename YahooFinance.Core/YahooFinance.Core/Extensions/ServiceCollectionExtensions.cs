using Microsoft.Extensions.DependencyInjection;

namespace YahooFinance.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddYahooFinance(this IServiceCollection services)
        {
            services.AddSingleton<IYahooFinanceClient, YahooFinanceClient>();
        }
    }
}
