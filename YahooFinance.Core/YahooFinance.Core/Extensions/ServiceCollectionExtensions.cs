using Microsoft.Extensions.DependencyInjection;

namespace MatthiWare.YahooFinance.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IYahooFinanceClient"/> to the services.
        /// </summary>
        /// <param name="services"></param>
        public static void AddYahooFinance(this IServiceCollection services)
        {
            services.AddSingleton<IYahooFinanceClient, YahooFinanceClient>();
        }
    }
}
