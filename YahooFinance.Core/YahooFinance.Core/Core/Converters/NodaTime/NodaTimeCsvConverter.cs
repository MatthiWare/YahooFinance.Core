using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using NodaTime.Text;

namespace MatthiWare.YahooFinance.Core.Converters.NodaTime
{
    public class NodaTimeCsvConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == null)
            {
                return base.ConvertFromString(null, row, memberMapData);
            }

            return InstantPattern.CreateWithInvariantCulture("yyyy-MM-dd").Parse(text).Value;
        }
    }
}
