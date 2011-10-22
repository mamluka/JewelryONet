using System.Collections.Generic;
using System.Web.Mvc;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class ReviewOrderViewModel
    {
        public List<ICartItemViewModel> CartItems { get; set; }
        public string TotalPrice { get; set; }

        public AddressViewModel BillingAddress { get; set; }
        public AddressViewModel ShippingAddress { get; set; }



        public string Comment { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PaymentMethod { get; set; }

        public CreditCardViewModel CreditCardViewModel { get; set; }

        public int OrderNumber { get; set; }
    }
}