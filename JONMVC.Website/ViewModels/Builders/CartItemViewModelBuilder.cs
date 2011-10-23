using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class CartItemViewModelBuilder
    {
        private readonly IJewelRepository jewelRepository;
        private readonly IDiamondRepository diamondRepository;
        private readonly IMappingEngine mapper;

        public CartItemViewModelBuilder(IJewelRepository jewelRepository, IDiamondRepository diamondRepository, IMappingEngine mapper)
        {
            this.jewelRepository = jewelRepository;
            this.diamondRepository = diamondRepository;
            this.mapper = mapper;
        }


        public List<ICartItemViewModel> Build(List<ICartItem> cartItems)
        {
            
            var cartItemsViewModelList = new List<ICartItemViewModel>();
            for (int index = 0; index < cartItems.Count; index++)
            {
                var cartItem = cartItems[index];
                switch (cartItem.Type)
                {
                    case CartItemType.Jewelry:
                        {
                            var currentCartItemViewModel = JewelCartItemViewModelBuilderMethod(cartItem, index);
                            cartItemsViewModelList.Add(currentCartItemViewModel);
                            break;
                        }
                       
                    case CartItemType.Diamond:
                        {
                            var currentCartItemViewModel = DiamondCartItemViewModelBuilderMethod(cartItem, index);
                            cartItemsViewModelList.Add(currentCartItemViewModel);
                            break;
                        }
                    case CartItemType.CustomJewel:
                        {
                            var currentCartItemViewModel = CustomJewelCartItemViewModelBuilderMethod(cartItem, index);
                            cartItemsViewModelList.Add(currentCartItemViewModel);
                            break;
                        }
                        
                    default:
                        throw new Exception(
                            "When asked to generate view models for the cart items the type was not supported");
                }
            }

            return cartItemsViewModelList;
        }

        private ICartItemViewModel CustomJewelCartItemViewModelBuilderMethod(ICartItem cartItem, int index)
        {
            try
            {
                var customJewel = cartItem as CustomJewelCartItem;
                jewelRepository.FilterMediaByMetal(customJewel.MediaType);
                var diamond = diamondRepository.GetDiamondByID(customJewel.DiamondID);
                var jewel = jewelRepository.GetJewelByID(customJewel.SettingID);

                var twoObjects = new MergeDiamondAndJewel()
                                     {
                                         First = diamond,
                                         Second = jewel
                                     };


                var viewModel = mapper.Map<MergeDiamondAndJewel, CustomJewelCartItemViewModel>(twoObjects);
                viewModel.CartID = index;
                viewModel.Size = customJewel.GetSize();
                return viewModel;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message +
                                    "\n When asked to build a view model for jewelcartitem we had a problem explained above");
            }
        }

        private ICartItemViewModel DiamondCartItemViewModelBuilderMethod(ICartItem cartItem, int index)
        {
            try
            {
                var diamond = diamondRepository.GetDiamondByID(cartItem.ID);
                var viewModel = mapper.Map<Diamond, DiamondCartItemViewModel>(diamond);
                viewModel.CartID = index;
                return viewModel;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message +
                                    "\n When asked to build a view model for jewelcartitem we had a problem explained above");
            }
        }

        private JewelCartItemViewModel JewelCartItemViewModelBuilderMethod(ICartItem cartItem, int index)
        {
            try
            {
                var jewelCartItem = cartItem as JewelCartItem;
                jewelRepository.FilterMediaByMetal(jewelCartItem.MediaType);

                var jewel = jewelRepository.GetJewelByID(cartItem.ID);
                var viewModel = mapper.Map<Jewel, JewelCartItemViewModel>(jewel);
                viewModel.JewelSize = jewelCartItem.Size;
                viewModel.CartID = index;
                return viewModel;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message +
                                    "\n When asked to build a view model for jewelcartitem we had a problem explained above");
            }


        }
    }
}