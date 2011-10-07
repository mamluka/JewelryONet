using System;
using AutoMapper;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class MyAccountHasAddressInformationResolver:ValueResolver<ExtendedCustomer,bool>
    {
        protected override bool ResolveCore(ExtendedCustomer source)
        {
            if (DoWeHaveExtendedCustomerFieldsSet(source))
            {
                return false;
            }
            return true;
        }

        private static bool DoWeHaveExtendedCustomerFieldsSet(ExtendedCustomer source)
        {
            if (source !=null)
            {
                return String.IsNullOrWhiteSpace(source.BillingAddress.City) && string.IsNullOrWhiteSpace(source.BillingAddress.ZipCode);    
            }
            return false;

        }
    }
}