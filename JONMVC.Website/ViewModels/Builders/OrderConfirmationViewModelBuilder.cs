using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class OrderConfirmationViewModelBuilder
    {
        private readonly int orderID;
        private readonly CheckoutDetailsModel checkoutDetails;

        public OrderConfirmationViewModelBuilder(int orderID, CheckoutDetailsModel checkoutDetails)
        {
            this.orderID = orderID;
            this.checkoutDetails = checkoutDetails;
        }

        public OrderConfirmationViewModel Build()
        {
            var viewModel = new OrderConfirmationViewModel();
            viewModel.OrderNumber = orderID.ToString();
            viewModel.Email = checkoutDetails.Email;

            return viewModel;

        }
    }
}