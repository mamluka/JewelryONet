using System;
using System.Collections.Generic;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Views;
using NMoneys;

namespace JONMVC.Website.ViewModels.Builders
{
    public class ShoppingCartViewModelBuilder
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IJewelRepository jewelRepository;
        private readonly CartItemViewModelBuilder cartItemViewModelBuilder;
        private readonly IAuthentication authentication;
        private readonly IMappingEngine mapper;

        public ShoppingCartViewModelBuilder(IShoppingCart shoppingCart, IJewelRepository jewelRepository, CartItemViewModelBuilder cartItemViewModelBuilder, IAuthentication authentication, IMappingEngine mapper)
        {
            this.shoppingCart = shoppingCart;
            this.jewelRepository = jewelRepository;
            this.cartItemViewModelBuilder = cartItemViewModelBuilder;
            this.authentication = authentication;
            this.mapper = mapper;
        }

        public ShoppingCartViewModel Build()
        {

            var viewModel = new ShoppingCartViewModel();

            var cartItemsViewModelList = cartItemViewModelBuilder.Build(shoppingCart.Items);

            viewModel.CartItems = cartItemsViewModelList;
            viewModel.TotalPrice = new Money(shoppingCart.TotalPrice, Currency.Usd).Format("{1}{0:#,0}");

            if (authentication.IsSignedIn())
            {
                viewModel.IsSignedIn = authentication.IsSignedIn();
                viewModel.Email = authentication.CustomerData.Email;
            }

            return viewModel;
        }

       
    }
}