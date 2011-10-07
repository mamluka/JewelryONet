using System;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.Checkout
{
    public class CustomJewelCartItem : ICartItem
    {
        private readonly int diamondID;
        private readonly int settingID;
        private string size;
        private readonly JewelMediaType mediaType;
        private readonly decimal price;
        private readonly int id;
        private readonly CartItemType type;


        public CartItemType Type
        {
            get { return  type; }
        }

        public decimal Price
        {
            get { return price; }
        }

        public int ID
        {
            get { return id; }
        }

        public string GetSize()
        {
            return size;
        }

        public int DiamondID
        {
            get { return diamondID; }
        }

        public int SettingID
        {
            get { return settingID; }
        }

        public string Size
        {
            get { return size; }
        }

        public JewelMediaType MediaType
        {
            get { return mediaType; }
        }


        public CustomJewelCartItem(int diamondID, int settingID, string size, JewelMediaType mediaType, decimal price)
        {
            this.diamondID = diamondID;
            this.settingID = settingID;
            this.size = size;
            this.mediaType = mediaType;
            this.price = price;
            this.id = diamondID*3 + settingID*7;
            this.type = CartItemType.CustomJewel;
        }

        public void SetSize(string size)
        {
            this.size = size;
        }
    }
}