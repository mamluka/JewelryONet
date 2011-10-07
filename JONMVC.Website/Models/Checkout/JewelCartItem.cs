using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.Checkout
{
    public class JewelCartItem:ICartItem
    {
        private CartItemType type;
        private decimal price;
        private int jewelID;
        private readonly JewelMediaType mediaType;
        private string size;

        public int ID
        {
            get
            {
                return jewelID;
            }
        }

        public string GetSize()
        {
            return size;
        }

        public CartItemType Type
        {
            get { return type; }
        }

        public decimal Price
        {
            get { return price; }
        }

        public string Size
        {
            get { return size; }
        }

        public JewelMediaType MediaType
        {
            get { return mediaType; }
        }

        public JewelCartItem(int id, JewelMediaType mediaType, decimal price)
        {
            type = CartItemType.Jewelry;
            jewelID = id;
            this.mediaType = mediaType;
            this.price = price;


        }

       


        public void SetSize(string jewelsize)
        {
            size = jewelsize;
        }
    }
}