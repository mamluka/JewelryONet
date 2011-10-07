using System;
using AutoMapper;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class PaymentMethodResolver:ValueResolver<PaymentMethod,int>
    {
        protected override int ResolveCore(PaymentMethod source)
        {
            switch (source)
            {
                case PaymentMethod.CraditCard:
                    return 1;
                
                case PaymentMethod.WireTransfer:
                    return 2;

                case PaymentMethod.PayPal:
                    return 3;
              
                default:
                    throw new Exception("When asked to convert a payment method from view to database the type was not supported and was " + source.ToString());
            }
        }
    }
}