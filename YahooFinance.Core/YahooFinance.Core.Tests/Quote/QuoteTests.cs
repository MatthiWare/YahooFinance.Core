using MatthiWare.YahooFinance.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using YahooFinance.Tests.Extensions;

namespace YahooFinance.Tests.Quote
{
    public class QuoteTests
    {
        private readonly ILogger<YahooFinanceClient> logger;

        public QuoteTests(ITestOutputHelper testOutput)
        {
            logger = testOutput.BuildLoggerFor<YahooFinanceClient>();
        }

        [Theory]
        [InlineData(new string[] { "O", "MSFT", "ABI.BR" }, 3)]
        [InlineData(new string[] { "O", "O", "O" }, 1)]
        public async Task LookupWorks(string[] symbols, int results)
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.Quote.LookupAsync(symbols);

            result.AssertValidResponse();

            Assert.Equal(results, result.Data.Count);
        }

        [Fact]
        public async Task IncorrectLookupShowsErrorResponse()
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.Quote.LookupAsync(Array.Empty<string>());

            Assert.True(result.HasError);
            Assert.NotNull(result.Error);
        }
    }
}
