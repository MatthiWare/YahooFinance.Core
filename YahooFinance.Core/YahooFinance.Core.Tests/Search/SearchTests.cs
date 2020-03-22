using System.Linq;
using System.Threading.Tasks;
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
        [InlineData("O", "US7561091049")]
        [InlineData("ABI.BR", "BE0974293251")]
        public async Task SearchReturnsCorrectSymbol(string symbol, string isin)
        {
            var client = new YahooFinanceClient(logger);

            var resultSymbol = await client.Search.SearchAsync(symbol);
            var resultIsin = await client.Search.SearchAsync(isin);

            resultSymbol.AssertValidResponse();
            resultIsin.AssertValidResponse();

            var firstResultSymbol = resultSymbol.Data.First();
            var firstResultIsin = resultIsin.Data.First();

            Assert.Equal(firstResultSymbol, firstResultIsin);

            Assert.Equal(symbol, firstResultSymbol.Symbol);
            Assert.Equal(symbol, firstResultIsin.Symbol);

        }
    }
}
