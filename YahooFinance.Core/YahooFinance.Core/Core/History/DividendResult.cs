using MatthiWare.YahooFinance.Abstractions.History;
using NodaTime;

namespace MatthiWare.YahooFinance.Core.History
{
    public class DividendResult : IHistoryItem
    {
        public Instant Date { get; set; }
        public decimal Dividends { get; set; }

        public override string ToString()
        {
            return $"{Date} - {Dividends.ToString("C")}";
        }
    }
}
