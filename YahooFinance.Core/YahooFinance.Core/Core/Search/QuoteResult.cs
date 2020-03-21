using System;

namespace MatthiWare.YahooFinance.Core.Search
{
    public class QuoteResult : IEquatable<QuoteResult>
    {
        public string Exchange { get; set; }
        public string Shortname { get; set; }
        public string QuoteType { get; set; }
        public string Symbol { get; set; }
        public string TypeDisp { get; set; }
        public string LongName { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as QuoteResult);
        }

        public bool Equals(QuoteResult other)
        {
            if (other == null) return false;

            if (ReferenceEquals(this, other)) return true;
            if (Exchange != other.Exchange) return false;
            if (QuoteType != other.QuoteType) return false;
            if (Symbol != other.Symbol) return false;
            if (TypeDisp != other.TypeDisp) return false;
            if (LongName != other.LongName) return false;
            if (Shortname != other.Shortname) return false;

            return true;
        }
    }
}
