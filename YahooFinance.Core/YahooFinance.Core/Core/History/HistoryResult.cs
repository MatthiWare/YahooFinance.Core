using NodaTime;
using CsvHelper.Configuration.Attributes;

namespace MatthiWare.YahooFinance.Core.History
{
    public class HistoryResult
    {
        public Instant  Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        [Name("Adj Close")]
        public decimal AdjustedClose { get; set; }
        public int Volume { get; set; }
    }
}
