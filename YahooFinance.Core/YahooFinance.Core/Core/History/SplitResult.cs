using CsvHelper.Configuration.Attributes;
using MatthiWare.YahooFinance.Abstractions.History;
using NodaTime;

namespace MatthiWare.YahooFinance.Core.History
{
    public class SplitResult : IHistoryItem
    {
        [Name("Stock Splits")]
        public SplitData Split { get; set; }
        public Instant Date { get; set; }
    }
}
