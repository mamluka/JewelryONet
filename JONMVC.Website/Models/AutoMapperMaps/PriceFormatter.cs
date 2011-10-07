using AutoMapper;
using NMoneys;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class PriceFormatter:ValueFormatter<decimal>
    {
        protected override string FormatValueCore(decimal value)
        {
            return new Money(value, Currency.Usd).Format("{1}{0:#,0}");
        }
    }

    public class PriceFormatterForDouble : ValueFormatter<double>
    {
        protected override string FormatValueCore(double value)
        {
            return new Money((decimal) value, Currency.Usd).Format("{1}{0:#,0}");
        }
    }
}