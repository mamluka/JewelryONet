using System.Collections.Generic;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class MergeOrdersAndCustomer:MergeTwoObjectsForAutoMapper<List<Order>,ExtendedCustomer>
    {
        public override List<Order> First { get; set; }
        public override ExtendedCustomer Second { get; set; }
    }
}