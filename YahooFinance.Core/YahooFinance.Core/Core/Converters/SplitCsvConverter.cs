using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using MatthiWare.YahooFinance.Core.History;

namespace MatthiWare.YahooFinance.Core.Converters
{
    public class SplitCsvConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == null || !text.Contains(":"))
            {
                return base.ConvertFromString(null, row, memberMapData);
            }

            var tokens = text.Split(':');

            var before = int.Parse(tokens[0]);
            var after = int.Parse(tokens[1]);

            return new SplitData(before, after);
        }
    }
}
