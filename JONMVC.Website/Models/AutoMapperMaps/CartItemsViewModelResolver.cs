using System.Collections.Generic;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Builders;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class CartItemsViewModelResolver : ValueResolver<List<ICartItem>,List<ICartItemViewModel>>
    {
        private readonly CartItemViewModelBuilder cartItemViewModelBuilder;

        public CartItemsViewModelResolver(CartItemViewModelBuilder cartItemViewModelBuilder)
        {
            this.cartItemViewModelBuilder = cartItemViewModelBuilder;
        }


        protected override List<ICartItemViewModel> ResolveCore(List<ICartItem> source)
        {
            return cartItemViewModelBuilder.Build(source);
        }
    }
}