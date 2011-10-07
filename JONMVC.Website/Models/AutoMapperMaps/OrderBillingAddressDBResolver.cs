using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class OrderBillingAddressDBResolver:ValueResolver<acc_ORDERS,Address>
    {
        protected override Address ResolveCore(acc_ORDERS source)
        {
            return new Address()
                       {
                           Address1 = source.adrs_billing_street,
                           City = source.adrs_billing_city,
                           FirstName = source.adrs_billing_firstname,
                           LastName = source.adrs_billing_lastname,
                           CountryID = source.adrs_billing_country_id,
                           StateID = source.adrs_billing_state_id,
                           ZipCode = source.adrs_billing_zip,
                           Phone = source.adrs_billing_phone,
                           Country = source.sys_COUNTRYReference.Value.LANG1_LONGDESCR,
                           State = source.sys_STATEReference.Value.LANG1_LONGDESCR,
                       };
        }
    }
}