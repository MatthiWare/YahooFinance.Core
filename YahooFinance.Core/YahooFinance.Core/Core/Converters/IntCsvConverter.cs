using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace MatthiWare.YahooFinance.Core.Converters
{
    /// <inheritdoc/>
    public class IntCsvConverter : DefaultTypeConverter
    {
        /// <inheritdoc/>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!int.TryParse(text, out var convertedDecimal))
            {
                return int.MinValue;
            }

            return convertedDecimal;
        }
    }
}
