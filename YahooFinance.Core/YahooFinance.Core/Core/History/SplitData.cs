namespace MatthiWare.YahooFinance.Core.History
{
    public class SplitData
    {
        public int Before { get;  }
        public int After { get; }

        public SplitData(int before, int after)
        {
            Before = before;
            After = after;
        }
    }
}
