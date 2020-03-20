using MatthiWare.YahooFinance.Abstractions.Http;
using System.Collections;
using Xunit;

namespace YahooFinance.Tests.Extensions
{
    public static class ApiResponseExtensions
    {
        public static void AssertValidResponse<T>(this IApiResponse<T> response) where T : class
        {
            Assert.NotNull(response);
            Assert.False(response.HasError, $"Error in response: {response.Error}");

            Assert.NotNull(response.Data);

            if (response.Data is IEnumerable)
                Assert.NotEmpty(response.Data as IEnumerable);
        }
    }
}
