using System;
using System.Collections.Generic;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Checkout
{
    public class Order
    {
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public int PaymentID { get; set; }
        public String Comment { get; set; }

        public CreditCard CreditCard { get; set; }
        public int OrderNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ICartItem> Items { get; set; }

        public string TrackingNumber { get; set; }

        public string PaymentDate { get; set; }

        public DateTime CreateDate { get; set; }

        public OrderStatus Status { get; set; }
    }
}