using AutoMapper;
using JONMVC.Website.Models.Diamonds;
using NMoneys;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DiamondPageTitleResolver:ValueResolver<Diamond,string>
    {
        protected override string ResolveCore(Diamond source)
        {
            return source.Description + " - " + new Money(source.Price, Currency.Usd).Format("{1}{0:#,0}");
        }
    }
}