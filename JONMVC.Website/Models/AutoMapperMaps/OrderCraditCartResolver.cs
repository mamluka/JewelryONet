using System;
using System.Linq;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class OrderCraditCartResolver:ValueResolver<acc_ORDERS,CreditCard>
    {
        protected override CreditCard ResolveCore(acc_ORDERS source)
        {
            return new CreditCard()
                       {
                           CCV = source.acc_CASHFLOW.SingleOrDefault().cc_cvv,
                           CreditCardID = source.acc_CASHFLOW.SingleOrDefault().cc_type_id,
                           CreditCardsNumber = source.acc_CASHFLOW.SingleOrDefault().cc_number,
                           Month = Convert.ToInt32(source.acc_CASHFLOW.SingleOrDefault().cc_exp_month),
                           Year = Convert.ToInt32(source.acc_CASHFLOW.SingleOrDefault().cc_exp_year),
                       };
        }
    }
}