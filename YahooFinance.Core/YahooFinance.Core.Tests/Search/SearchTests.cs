using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using YahooFinance.Tests.Extensions;
using Microsoft.Extensions.Logging;
using MatthiWare.YahooFinance;

namespace YahooFinance.Tests.Search
{
    public class SearchTests
    {
        private readonly ILogger<YahooFinanceClient> logger;

        public SearchTests(ITestOutputHelper testOutput)
        {
            logger = testOutput.BuildLoggerFor<YahooFinanceClient>();
        }

        [Theory]
        [InlineData("AAPL", "US0378331005")]
        [InlineData("AD.AS", "NL0011794037")]
        [InlineData("AGS.BR", "BE0974264930")]
        [InlineData("ADM.L", "GB00B02J6398")]
        [InlineData("ENEL.MI", "IT0003128367")]
        [InlineData("O", "US7561091049")]
        public async Task SearchReturnsCorrectSymbol(string symbol, string isin)
        {
            var client = new YahooFinanceClient(logger);

            var resultSymbol = await client.Search.SearchAsync(symbol);
            var resultIsin = await client.Search.SearchAsync(isin);

            resultSymbol.AssertValidResponse();
            resultIsin.AssertValidResponse();

            var firstResultSymbol = resultSymbol.Data.First(x => x.Symbol == symbol);
            var firstResultIsin = resultIsin.Data.First(x => x.Symbol == symbol);

            Assert.Equal(firstResultSymbol, firstResultIsin);

            Assert.Equal(symbol, firstResultSymbol.Symbol);
            Assert.Equal(symbol, firstResultIsin.Symbol);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        public async Task CheckCancelledResult(int after)
        {
            var client = new YahooFinanceClient(logger);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(after);

            await client.Search.SearchAsync("O", cts.Token).CheckCancelledAsync();
        }
    }
}
