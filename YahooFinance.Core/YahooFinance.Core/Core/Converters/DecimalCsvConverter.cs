using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace MatthiWare.YahooFinance.Core.Converters
{
    /// <inheritdoc/>
    public class DecimalCsvConverter : DefaultTypeConverter
    {
        /// <inheritdoc/>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!decimal.TryParse(text, out var convertedDecimal))
            {
                return decimal.MinValue;
            }

            return convertedDecimal;
        }
    }
}
