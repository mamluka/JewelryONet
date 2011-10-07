using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class AddressViewModelResolver:ValueResolver<Address,AddressViewModel>
    {
        protected override AddressViewModel ResolveCore(Address source)
        {
            if (source != null)
            {
                return new AddressViewModel()
                       {
                           Address1 = source.Address1,
                           City = source.City,
                           Country = source.Country,
                           CountryID = source.CountryID,
                           FirstName = source.FirstName,
                           LastName = source.LastName,
                           Phone = source.Phone,
                           State = source.State,
                           StateID  = source.StateID,
                           ZipCode  = source.ZipCode,
                       };
            }
            return null;

        }
    }
}