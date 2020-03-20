using MatthiWare.YahooFinance.Core;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YahooFinance.Tests.Extensions;

namespace YahooFinance.Tests.Search
{
    public class SearchTests
    {
        [Theory]
        [InlineData("O", "US7561091049")]
        public async Task SearchReturnsCorrectSymbol(string symbol, string isin)
        {
            var client = new YahooFinanceClient(NullLogger<YahooFinanceClient>.Instance);

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
