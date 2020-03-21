using Newtonsoft.Json;
using System;

namespace MatthiWare.YahooFinance.Core.Quote
{
    public partial class SecurityResult
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("triggerable")]
        public bool Triggerable { get; set; }

        [JsonProperty("quoteSourceName", NullValueHandling = NullValueHandling.Ignore)]
        public string QuoteSourceName { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName", NullValueHandling = NullValueHandling.Ignore)]
        public string LongName { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName { get; set; }

        [JsonProperty("gmtOffSetMilliseconds")]
        public long GmtOffSetMilliseconds { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("esgPopulated")]
        public bool EsgPopulated { get; set; }

        [JsonProperty("fullExchangeName")]
        public string FullExchangeName { get; set; }

        [JsonProperty("financialCurrency", NullValueHandling = NullValueHandling.Ignore)]
        public string FinancialCurrency { get; set; }

        [JsonProperty("regularMarketOpen")]
        public double RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public long AverageDailyVolume3Month { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public long AverageDailyVolume10Day { get; set; }

        [JsonProperty("dividendDate", NullValueHandling = NullValueHandling.Ignore)]
        public long? DividendDate { get; set; }

        [JsonProperty("firstTradeDateMilliseconds")]
        public long FirstTradeDateMilliseconds { get; set; }

        [JsonProperty("postMarketChangePercent", NullValueHandling = NullValueHandling.Ignore)]
        public double? PostMarketChangePercent { get; set; }

        [JsonProperty("postMarketTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? PostMarketTime { get; set; }

        [JsonProperty("bookValue", NullValueHandling = NullValueHandling.Ignore)]
        public double? BookValue { get; set; }

        [JsonProperty("fiftyDayAverage")]
        public double FiftyDayAverage { get; set; }

        [JsonProperty("fiftyDayAverageChange")]
        public double FiftyDayAverageChange { get; set; }

        [JsonProperty("fiftyDayAverageChangePercent")]
        public double FiftyDayAverageChangePercent { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public double TwoHundredDayAverage { get; set; }

        [JsonProperty("twoHundredDayAverageChange")]
        public double TwoHundredDayAverageChange { get; set; }

        [JsonProperty("twoHundredDayAverageChangePercent")]
        public double TwoHundredDayAverageChangePercent { get; set; }

        [JsonProperty("marketCap", NullValueHandling = NullValueHandling.Ignore)]
        public long? MarketCap { get; set; }

        [JsonProperty("forwardPE", NullValueHandling = NullValueHandling.Ignore)]
        public double? ForwardPe { get; set; }

        [JsonProperty("priceToBook", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceToBook { get; set; }

        [JsonProperty("sourceInterval")]
        public long SourceInterval { get; set; }

        [JsonProperty("exchangeDataDelayedBy")]
        public long ExchangeDataDelayedBy { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("priceHint")]
        public long PriceHint { get; set; }

        [JsonProperty("postMarketPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? PostMarketPrice { get; set; }

        [JsonProperty("postMarketChange", NullValueHandling = NullValueHandling.Ignore)]
        public double? PostMarketChange { get; set; }

        [JsonProperty("regularMarketChange")]
        public double RegularMarketChange { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public double RegularMarketChangePercent { get; set; }

        [JsonProperty("regularMarketTime")]
        public long RegularMarketTime { get; set; }

        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public double RegularMarketDayHigh { get; set; }

        [JsonProperty("regularMarketDayRange")]
        public string RegularMarketDayRange { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public double RegularMarketDayLow { get; set; }

        [JsonProperty("regularMarketVolume")]
        public long RegularMarketVolume { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public double RegularMarketPreviousClose { get; set; }

        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("bidSize")]
        public long BidSize { get; set; }

        [JsonProperty("askSize")]
        public long AskSize { get; set; }

        [JsonProperty("fiftyTwoWeekLowChange")]
        public double FiftyTwoWeekLowChange { get; set; }

        [JsonProperty("fiftyTwoWeekLowChangePercent")]
        public double FiftyTwoWeekLowChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekRange")]
        public string FiftyTwoWeekRange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChange")]
        public double FiftyTwoWeekHighChange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChangePercent")]
        public double FiftyTwoWeekHighChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }

        [JsonProperty("earningsTimestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long? EarningsTimestamp { get; set; }

        [JsonProperty("earningsTimestampStart", NullValueHandling = NullValueHandling.Ignore)]
        public long? EarningsTimestampStart { get; set; }

        [JsonProperty("earningsTimestampEnd", NullValueHandling = NullValueHandling.Ignore)]
        public long? EarningsTimestampEnd { get; set; }

        [JsonProperty("trailingAnnualDividendRate", NullValueHandling = NullValueHandling.Ignore)]
        public double? TrailingAnnualDividendRate { get; set; }

        [JsonProperty("trailingPE", NullValueHandling = NullValueHandling.Ignore)]
        public double? TrailingPe { get; set; }

        [JsonProperty("trailingAnnualDividendYield", NullValueHandling = NullValueHandling.Ignore)]
        public double? TrailingAnnualDividendYield { get; set; }

        [JsonProperty("marketState")]
        public string MarketState { get; set; }

        [JsonProperty("epsTrailingTwelveMonths", NullValueHandling = NullValueHandling.Ignore)]
        public double? EpsTrailingTwelveMonths { get; set; }

        [JsonProperty("epsForward", NullValueHandling = NullValueHandling.Ignore)]
        public double? EpsForward { get; set; }

        [JsonProperty("sharesOutstanding", NullValueHandling = NullValueHandling.Ignore)]
        public long? SharesOutstanding { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("headSymbolAsString", NullValueHandling = NullValueHandling.Ignore)]
        public string HeadSymbolAsString { get; set; }

        [JsonProperty("contractSymbol", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ContractSymbol { get; set; }

        [JsonProperty("underlyingSymbol", NullValueHandling = NullValueHandling.Ignore)]
        public string UnderlyingSymbol { get; set; }

        [JsonProperty("underlyingExchangeSymbol", NullValueHandling = NullValueHandling.Ignore)]
        public string UnderlyingExchangeSymbol { get; set; }

        [JsonProperty("openInterest", NullValueHandling = NullValueHandling.Ignore)]
        public long? OpenInterest { get; set; }

        [JsonProperty("expireDate", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpireDate { get; set; }

        [JsonProperty("expireIsoDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ExpireIsoDate { get; set; }
    }
}
