using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class BillingViewModelBuilder
    {
        private readonly CheckoutDetailsModel checkoutDetailsModel;
        private readonly IAuthentication authentication;
        private readonly ICustomerAccountService accountService;
        private readonly IMappingEngine mapper;

        public BillingViewModelBuilder(CheckoutDetailsModel checkoutDetailsModel, IAuthentication authentication,ICustomerAccountService accountService, IMappingEngine mapper)
        {
            this.checkoutDetailsModel = checkoutDetailsModel;
            this.authentication = authentication;
            this.accountService = accountService;
            this.mapper = mapper;
        }

        public BillingViewModel Build()
        {
            if (authentication.IsSignedIn())
            {
                var signedInCustomer = authentication.CustomerData;
                var extendedCustomer = accountService.GetExtendedCustomerByEmail(signedInCustomer.Email);
                var viewModel =  mapper.Map<ExtendedCustomer, BillingViewModel>(extendedCustomer);
                viewModel.PaymentMethod = checkoutDetailsModel.PaymentMethod;
                return viewModel;
            }
            return mapper.Map<CheckoutDetailsModel, BillingViewModel>(checkoutDetailsModel);
        }
    }
}