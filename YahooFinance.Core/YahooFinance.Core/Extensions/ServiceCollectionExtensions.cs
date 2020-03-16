using Microsoft.Extensions.DependencyInjection;

namespace MatthiWare.YahooFinance.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddYahooFinance(this IServiceCollection services)
        {
            services.AddSingleton<IYahooFinanceClient, YahooFinanceClient>();
        }
    }
}
