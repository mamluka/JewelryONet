using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;
using NMoneys;

namespace JONMVC.Website.ViewModels.Builders
{
    public class ReviewOrderViewModelBuilder
    {
        private readonly CheckoutDetailsModel checkoutDetailsModel;
        private readonly IShoppingCart shoppingCart;
        private readonly CartItemViewModelBuilder cartItemViewModelBuilder;
        private readonly IMappingEngine mapper;

        public ReviewOrderViewModelBuilder(CheckoutDetailsModel checkoutDetailsModel, IShoppingCart shoppingCart, CartItemViewModelBuilder cartItemViewModelBuilder , IMappingEngine mapper)
        {
            this.checkoutDetailsModel = checkoutDetailsModel;
            this.shoppingCart = shoppingCart;
            this.cartItemViewModelBuilder = cartItemViewModelBuilder;
            this.mapper = mapper;
        }

        public ReviewOrderViewModel Build()
        {
            var viewModel = mapper.Map<CheckoutDetailsModel, ReviewOrderViewModel>(checkoutDetailsModel);
            viewModel.CartItems = cartItemViewModelBuilder.Build(shoppingCart.Items);
            viewModel.TotalPrice =  new Money(shoppingCart.TotalPrice, Currency.Usd).Format("{1}{0:#,0}"); 
            
            return viewModel;
        }
    }
}