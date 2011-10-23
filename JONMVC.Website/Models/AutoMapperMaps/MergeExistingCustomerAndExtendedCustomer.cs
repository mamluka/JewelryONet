using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class MergeExistingCustomerAndExtendedCustomer:MergeTwoObjectsForAutoMapper<usr_CUSTOMERS, ExtendedCustomer>
    {
        public override usr_CUSTOMERS First { get; set; }
        public override ExtendedCustomer Second { get; set; }
    }
}