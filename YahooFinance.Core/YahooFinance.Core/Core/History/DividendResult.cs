using NodaTime;

namespace MatthiWare.YahooFinance.Core.History
{
    public class DividendResult
    {
        public Instant Date { get; set; }
        public decimal Dividends { get; set; }

        public override string ToString()
        {
            return $"{Date} - {Dividends.ToString("C")}";
        }
    }
}
