using MatthiWare.YahooFinance;
using Microsoft.Extensions.Logging;
using NodaTime;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using YahooFinance.Tests.Extensions;

namespace YahooFinance.Tests.History
{
    public class HistoryTests
    {
        private readonly ILogger<YahooFinanceClient> logger;
        private readonly IClock clock;

        public HistoryTests(ITestOutputHelper testOutput)
        {
            logger = testOutput.BuildLoggerFor<YahooFinanceClient>();
            clock = SystemClock.Instance;
        }

        [Theory]
        [InlineData("O")]
        public async Task GetDividendsWorksCorrectly(string symbol)
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetDividendsAsync(symbol, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());

            result.AssertValidResponse();
        }

        [Fact]
        public async Task IncorrectLookupShowsErrorResponseWhenStartDateHappendBeforeEndDate()
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetDividendsAsync(string.Empty, clock.GetCurrentInstant(), clock.GetCurrentInstant().Minus(Duration.FromDays(365)));

            Assert.True(result.HasError);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task IncorrectLookupShowsErrorResponseWhenIncorrectSymbol()
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetDividendsAsync(string.Empty, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());

            Assert.True(result.HasError);
            Assert.NotNull(result.Error);
        }
    }
}
