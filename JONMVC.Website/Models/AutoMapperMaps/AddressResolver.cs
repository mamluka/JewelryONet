using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class AddressResolver:ValueResolver<AddressViewModel,Address>
    {
        protected override Address ResolveCore(AddressViewModel source)
        {
            return new Address()
                       {
                           Address1 =  source.Address1,
                           City = source.City,
                           Phone = source.Phone,
                           FirstName = source.FirstName,
                           CountryID = source.CountryID,
                           LastName = source.LastName,
                           StateID = source.StateID,
                           ZipCode = source.ZipCode
                       };
        }
    }
}