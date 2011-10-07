using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class PaymentMethodFromIntResolver:ValueResolver<int,string>
    {
        protected override string ResolveCore(int source)
        {
            return CustomAttributes.GetDescription((PaymentMethod)source);
        }
    }
}