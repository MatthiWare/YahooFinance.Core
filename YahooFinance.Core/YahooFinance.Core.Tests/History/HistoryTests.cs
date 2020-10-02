using MatthiWare.YahooFinance;
using MatthiWare.YahooFinance.Abstractions.Http;
using Microsoft.Extensions.Logging;
using NodaTime;
using System.Threading;
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
        [InlineData("ACAMU")]
        public async Task GetDividendsWorksCorrectly(string symbol)
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetDividendsAsync(symbol, clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());

            result.AssertValidResponse();
        }

        [Theory]
        [InlineData("O")]
        [InlineData("SPG")]
        [InlineData("ACAMU")]
        public async Task GetSplitsWorksCorrectly(string symbol)
        {
            var client = new YahooFinanceClient(logger);

            var result = await client.History.GetSplitsAsync(symbol, Instant.MinValue, clock.GetCurrentInstant());

            result.AssertValidResponse();
        }

        [Theory]
        [InlineData("O")]
        [InlineData("ACAMU")]
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

        [Theory]
        [InlineData("div", 0)]
        [InlineData("div", 25)]
        [InlineData("history", 0)]
        [InlineData("history", 25)]
        [InlineData("split", 0)]
        [InlineData("split", 25)]
        public async Task CheckCancelledResult(string historyType, int after)
        {
            var client = new YahooFinanceClient(logger);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(after);

            const string symbol = "MSFT";

            switch (historyType)
            {
                case "div":
                    await client.History.GetDividendsAsync(symbol, Instant.MinValue, clock.GetCurrentInstant(), 
                        cancellationToken: cts.Token).CheckCancelledAsync();
                    break;
                case "history":
                    await client.History.GetPricesAsync(symbol, Instant.MinValue, clock.GetCurrentInstant(), 
                        cancellationToken: cts.Token).CheckCancelledAsync();
                    break;
                case "split":
                    await client.History.GetSplitsAsync(symbol, Instant.MinValue, clock.GetCurrentInstant(), 
                        cancellationToken: cts.Token).CheckCancelledAsync();
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
