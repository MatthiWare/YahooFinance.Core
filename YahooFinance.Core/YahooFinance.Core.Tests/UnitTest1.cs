using MatthiWare.YahooFinance.Core;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace YahooFinance.Core.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var client = new YahooFinanceClient(NullLogger<YahooFinanceClient>.Instance);

            var result = await client.Search.SearchAsync("O");

            Assert.NotNull(result);
        }
    }
}
