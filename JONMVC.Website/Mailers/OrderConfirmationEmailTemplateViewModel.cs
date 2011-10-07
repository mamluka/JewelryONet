using System.Collections.Generic;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.Mailers
{
    public class OrderConfirmationEmailTemplateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string OrderNumber { get; set; }
        public string TotalPrice { get; set; }
        public List<ICartItemViewModel> Items { get; set; }
        public string CCType { get; set; }
        public string CCLast4Digits { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}