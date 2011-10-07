using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class CustomerShippingAddressDBResolver : ValueResolver<usr_CUSTOMERS, Address>
    {
        protected override Address ResolveCore(usr_CUSTOMERS source)
        {
            return new Address()
                       {
                           Address1 = source.street2,
                           City = source.city2,
                           CountryID = source.country2_id,
                           FirstName = source.firstname,
                           LastName = source.lastname,
                           Phone = source.phone2,
                           StateID = source.state2_id,
                           ZipCode = source.zip2,
                           Country = source.sys_COUNTRY1Reference.Value.LANG1_LONGDESCR,
                           State = source.sys_STATE1Reference.Value.LANG1_LONGDESCR
                       };
        }
    }
}