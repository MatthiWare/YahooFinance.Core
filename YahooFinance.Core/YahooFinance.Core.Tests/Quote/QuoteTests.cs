using MatthiWare.YahooFinance;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
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
        [InlineData(new string[] { "O", "MSFT", "ABI.BR", "ADM.L", "AGS.BR", "ENEL.MI", "AD.AS" }, 7)]
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

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        public async Task CheckCancelledResult(int after)
        {
            var client = new YahooFinanceClient(logger);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(after);

            await client.Quote.LookupAsync("O", cts.Token).CheckCancelledAsync();
        }
    }
}
