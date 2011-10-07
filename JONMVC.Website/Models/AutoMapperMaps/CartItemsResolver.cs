using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class CartItemsResolver:ValueResolver<acc_ORDERS,List<ICartItem>>
    {
        private readonly IJewelRepository jewelRepository;
        private readonly IDiamondRepository diamondRepository;

        public CartItemsResolver(IJewelRepository jewelRepository, IDiamondRepository diamondRepository)
        {
            this.jewelRepository = jewelRepository;
            this.diamondRepository = diamondRepository;
            
        }

        protected override List<ICartItem> ResolveCore(acc_ORDERS source)
        {
            try
            {
                var cartItems = new List<ICartItem>();

                var jewelItems = source.acc_JEWELRY_ORDER_ITEMS.ToList();
                foreach (var jewelItem in jewelItems)
                {
                    var dbJewel = jewelRepository.GetJewelByID(jewelItem.Item_id);
                    var jewelCartItem = new JewelCartItem(jewelItem.Item_id, (JewelMediaType)jewelItem.metal, (decimal) dbJewel.Price);

                    jewelCartItem.SetSize(jewelItem.jewelsize);
                    
                    cartItems.Add(jewelCartItem);
                }

                var diamondItems = source.acc_DIAMOND_ORDER_ITEMS.ToList();
                foreach (var diamondItem in diamondItems)
                {
                    var dbDiamond = diamondRepository.GetDiamondByID(diamondItem.Item_id);
                    var diamondCartItem = new DiamondCartItem(diamondItem.Item_id, dbDiamond.Price);
                    cartItems.Add(diamondCartItem);
                }

                var customJewelItems = source.acc_CUSTOMJEWEL_ORDER_ITEMS.ToList();

                foreach (var customJewelItem in customJewelItems)
                {
                    var dbsetting = jewelRepository.GetJewelByID(customJewelItem.Setting_id);
                    var dbdiamond = diamondRepository.GetDiamondByID(customJewelItem.Diamond_id);

                    var customJewelCartItem = new CustomJewelCartItem(customJewelItem.Diamond_id,
                                                                      customJewelItem.Setting_id, customJewelItem.size, JewelMediaType.WhiteGold,
                                                                      dbdiamond.Price + (decimal) dbsetting.Price);


                    cartItems.Add(customJewelCartItem);
                }

                return cartItems;


            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to convert from a db order object to a Order POCO:\r\n ordernumber=" + source.OrderNumber + "\r\n" + " an error occured:" + ex.Message );
            }
            
        }
    }
}