using System;

namespace JONMVC.Website.Models.Checkout
{
    public class ExtendedCustomer:Customer
    {
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public DateTime MemeberSince { get; set; }
    }
}