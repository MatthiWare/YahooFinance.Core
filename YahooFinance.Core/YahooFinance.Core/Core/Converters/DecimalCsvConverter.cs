using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;

namespace MatthiWare.YahooFinance.Core.Converters
{
    /// <inheritdoc/>
    public class DecimalCsvConverter : DefaultTypeConverter
    {
        /// <inheritdoc/>
        public override object ConvertFromString(
            string text,
            IReaderRow row,
            MemberMapData memberMapData
        )
        {
            return decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var convertedDecimal) 
                ? convertedDecimal 
                : decimal.MinValue;
        }
    }
}