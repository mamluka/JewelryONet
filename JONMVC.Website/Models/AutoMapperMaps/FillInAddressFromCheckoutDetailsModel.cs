using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class FillInAddressFromCheckoutDetailsModel:ValueResolver<CheckoutDetailsModel,AddressViewModel>
    {
        protected override AddressViewModel ResolveCore(CheckoutDetailsModel source)
        {
            return new AddressViewModel()
                       {
                           FirstName = source.FirstName,
                           LastName = source.LastName,
                           Phone = source.Phone
                       };
        }
    }
}