using MatthiWare.YahooFinance;
using MatthiWare.YahooFinance.Abstractions.Http;
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

        [Theory]
        [InlineData("O")]
        [InlineData("SPG")]
        public async Task GetSplitsWorksCorrectly(string symbol)
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetSplitsAsync(symbol, Instant.MinValue, clock.GetCurrentInstant());

            result.AssertValidResponse();
        }

        [Theory]
        [InlineData("O")]
        public async Task GetPricesWorksCorrectly(string symbol)
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetPricesAsync(symbol, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());

            result.AssertValidResponse();
        }

        [Theory]
        [InlineData("div")]
        [InlineData("history")]
        [InlineData("split")]
        public async Task IncorrectLookupShowsErrorResponseWhenStartDateHappendBeforeEndDate(string historyType)
        {
            var client = new YahooFinanceClient(logger);

            switch (historyType)
            {
                case "div":
                    AssertHasError(await client.History.GetDividendsAsync(string.Empty, clock.GetCurrentInstant(), clock.GetCurrentInstant().Minus(Duration.FromDays(365))));
                    break;
                case "history":
                    AssertHasError(await client.History.GetPricesAsync(string.Empty, clock.GetCurrentInstant(), clock.GetCurrentInstant().Minus(Duration.FromDays(365))));
                    break;
                case "split":
                    AssertHasError(await client.History.GetSplitsAsync(string.Empty, clock.GetCurrentInstant(), clock.GetCurrentInstant().Minus(Duration.FromDays(365))));
                    break;
                default:
                    throw new Xunit.Sdk.XunitException("Invalid history type");
            }
        }

        [Theory]
        [InlineData("div")]
        [InlineData("history")]
        [InlineData("split")]
        public async Task IncorrectLookupShowsErrorResponseWhenIncorrectSymbol(string historyType)
        {
            var client = new YahooFinanceClient(logger);

            switch (historyType)
            {
                case "div":
                    AssertHasError(await client.History.GetDividendsAsync(string.Empty, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant()));
                    break;
                case "history":
                    AssertHasError(await client.History.GetPricesAsync(string.Empty, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant()));
                    break;
                case "split":
                    AssertHasError(await client.History.GetSplitsAsync(string.Empty, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant()));
                    break;
                default:
                    throw new Xunit.Sdk.XunitException("Invalid history type");
            }
        }

        private void AssertHasError<T>(IApiResponse<T> result)
            where T : class
        {
            Assert.True(result.HasError);
            Assert.NotNull(result.Error);
        }
    }
}
