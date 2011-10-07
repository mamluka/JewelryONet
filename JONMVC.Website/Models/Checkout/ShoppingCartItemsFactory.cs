using System;
using AutoMapper;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.Checkout
{
    public class ShoppingCartItemsFactory
    {
        private readonly IJewelRepository jewelRepository;
        private readonly IDiamondRepository diamondRepository;

        public ShoppingCartItemsFactory(IJewelRepository jewelRepository, IDiamondRepository diamondRepository)
        {
            this.jewelRepository = jewelRepository;
            this.diamondRepository = diamondRepository;
        }

        public JewelCartItem JewelCartItem(int id, string size, JewelMediaType mediaType)
        {
            var jewel = jewelRepository.GetJewelByID(id);
            var price = jewel.Price;

            var cartItem = new JewelCartItem(id, mediaType,(decimal) price);
            if (!String.IsNullOrWhiteSpace(size))
            {
                cartItem.SetSize(size);
            }

            return cartItem;
        }

        public DiamondCartItem DiamondCartItem(int id)
        {
            var diamond = diamondRepository.GetDiamondByID(id);
            var price = diamond.Price;
            return new DiamondCartItem(id,price);
        }

        public CustomJewelCartItem CustomJewelCartItem(int diamondID, int settingID, string size, JewelMediaType mediaType)
        {
            var diamond = diamondRepository.GetDiamondByID(diamondID);
            var jewel = jewelRepository.GetJewelByID(settingID);

            var price = diamond.Price + (decimal) jewel.Price;


            return new CustomJewelCartItem(diamondID, settingID, size, mediaType, price);
        }
    }
}