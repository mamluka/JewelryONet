using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class OrderShippingAddressDBResolver : ValueResolver<acc_ORDERS, Address>
    {
        protected override Address ResolveCore(acc_ORDERS source)
        {
            return new Address()
                       {
                           Address1 = source.adrs_delivery_street,
                           City = source.adrs_delivery_city,
                           FirstName = source.adrs_delivery_firstname,
                           LastName = source.adrs_delivery_lastname,
                           CountryID = source.adrs_delivery_country_id,
                           StateID = source.adrs_delivery_state_id,
                           ZipCode = source.adrs_delivery_zip,
                           Phone = source.adrs_delivery_phone,
                           Country = source.sys_COUNTRY1Reference.Value.LANG1_LONGDESCR,
                           State = source.sys_STATE1Reference.Value.LANG1_LONGDESCR,
                       };
        }
    }
}