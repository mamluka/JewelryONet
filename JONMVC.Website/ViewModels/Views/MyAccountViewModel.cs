using System.Collections.Generic;

namespace JONMVC.Website.ViewModels.Views
{
    public class MyAccountViewModel:PageViewModelBase
    {
        public List<OrderSummeryViewModel> Orders { get; set; }

        public AddressViewModel BillingAddress { get; set; }
        public AddressViewModel ShippingAddress { get; set; }

        public bool HasAddressInformation { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public string MemeberSince { get; set; }
    }
}