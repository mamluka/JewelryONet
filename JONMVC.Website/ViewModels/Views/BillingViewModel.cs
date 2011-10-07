using System;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class BillingViewModel
    {
        public AddressViewModel BillingAddress { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public CreditCardViewModel CreditCardViewModel { get; set; }

        public String Comment { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}