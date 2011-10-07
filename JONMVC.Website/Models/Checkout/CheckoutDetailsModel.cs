using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Checkout
{
    public class CheckoutDetailsModel
    {
        public AddressViewModel BillingAddress { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public CreditCardViewModel CreditCardViewModel { get; set; }
        public String Comment { get; set; }
        public bool SameAddress { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


    }
}