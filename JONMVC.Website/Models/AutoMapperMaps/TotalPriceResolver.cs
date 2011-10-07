using AutoMapper;
using JONMVC.Website.ViewModels.Builders;
using NMoneys;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class TotalPriceResolver : ValueResolver<MergeDiamondAndJewel,string>
    {
        protected override string ResolveCore(MergeDiamondAndJewel source)
        {
            return new Money(source.Second.Price + source.First.Price, Currency.Usd).Format("{1}{0:#,0}");
        }
    }
}