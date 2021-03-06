﻿namespace MatthiWare.YahooFinance.Core.Search
{
    public class QuoteResultResponse
    {
        public string exchange { get; set; }
        public string shortname { get; set; }
        public string quoteType { get; set; }
        public string symbol { get; set; }
        public string index { get; set; }
        public double score { get; set; }
        public string typeDisp { get; set; }
        public string longname { get; set; }
        public bool isYahooFinance { get; set; }
    }
}
