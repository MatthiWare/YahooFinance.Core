using System;
using System.Threading.Tasks;
using Xunit;

namespace YahooFinance.Tests.Extensions
{
    public static class TaskExtensions
    {
        public static async Task CheckCancelledAsync(this Task task)
        {
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => task);

            Assert.True(task.IsCanceled);
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
        }
    }
}
