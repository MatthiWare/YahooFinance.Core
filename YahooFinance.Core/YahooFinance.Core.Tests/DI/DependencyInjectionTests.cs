using MatthiWare.YahooFinance.Core;
using MatthiWare.YahooFinance.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace YahooFinance.Tests.DI
{
    public class DependencyInjectionTests
    {
        private readonly ITestOutputHelper testOutput;

        public DependencyInjectionTests(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }

        [Fact]
        public void TestDISetup()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder => 
            {
                builder.ClearProviders();
                builder.AddXunit(testOutput);
                builder.SetMinimumLevel(LogLevel.Trace);
            });

            services.AddYahooFinance();

            var provider = services.BuildServiceProvider();

            var client = provider.GetService<IYahooFinanceClient>();

            Assert.NotNull(client);
            Assert.NotNull(client.Search);
            Assert.NotNull(client.Quote);
            Assert.NotNull(client.History);

            provider.Dispose();
        }
    }
}
