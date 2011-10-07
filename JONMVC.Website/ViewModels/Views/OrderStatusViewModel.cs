using System.Collections.Generic;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class OrderStatusViewModel:PageViewModelBase
    {
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public List<ICartItemViewModel> Items { get; set; }
        public string PaymentMethod { get; set; }
        public string SpecialInstructions { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public string TotalPrice { get; set; }

        public string TrackingNumber { get; set; }

        public string PaymentDate { get; set; }
    }
}