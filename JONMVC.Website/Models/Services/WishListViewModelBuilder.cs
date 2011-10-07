using System;
using System.Collections.Generic;
using AutoMapper;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Services
{
    public class WishListViewModelBuilder : IViewModelBuilder
    {
        private readonly IWishListPersistence wishListPersistence;
        private readonly IJewelRepository jewelRepository;
        private readonly IMappingEngine mapper;

        public WishListViewModelBuilder(IWishListPersistence wishListPersistence, IJewelRepository jewelRepository, IMappingEngine mapper)
        {
            this.wishListPersistence = wishListPersistence;
            this.jewelRepository = jewelRepository;
            this.mapper = mapper;
        }

        public WishListViewModel Build()
        {
            try
            {
                var itemsViewModelList = new List<WishListItemViewModel>();

                var itemdIDs = wishListPersistence.GetItemsOnWishList();

                foreach (var id in itemdIDs)
                {
                    var jewel = jewelRepository.GetJewelByID(id);
                    if (jewel == null)
                    {
                        continue;
                    }
                    var itemViewModel = mapper.Map<Jewel, WishListItemViewModel>(jewel);
                    itemsViewModelList.Add(itemViewModel);
                }

                var viewModel = new WishListViewModel { Items = itemsViewModelList };

                return viewModel;
            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to build the model for the wishlist page an error occured\r\n" + ex.Message);
            }
           
        }
    }
}