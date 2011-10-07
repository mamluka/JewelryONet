using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class CustomerBillingAddressDBResolver:ValueResolver<usr_CUSTOMERS,Address>
    {
        protected override Address ResolveCore(usr_CUSTOMERS source)
        {
            return new Address()
                       {
                           Address1 = source.street1,
                           City = source.city1,
                           CountryID = source.country1_id,
                           FirstName = source.firstname,
                           LastName = source.lastname,
                           Phone = source.phone1,
                           StateID = source.state1_id,
                           ZipCode = source.zip1,
                           Country = source.sys_COUNTRYReference.Value.LANG1_LONGDESCR,
                           State = source.sys_STATEReference.Value.LANG1_LONGDESCR
                           
                       };
        }
    }
}