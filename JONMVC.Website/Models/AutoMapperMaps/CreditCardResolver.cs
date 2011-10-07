using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class CreditCardResolver:ValueResolver<CreditCardViewModel,CreditCard>
    {
        protected override CreditCard ResolveCore(CreditCardViewModel source)
        {

            return source == null
                       ? null
                       : new CreditCard()
                             {
                                 CCV = source.CCV,
                                 CreditCardID = source.CreditCardID,
                                 CreditCardsNumber = source.CreditCardsNumber,
                                 Month = source.Month,
                                 Year = source.Year
                             };
        }
    }
}