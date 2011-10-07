using JONMVC.Website.Models.Diamonds;

namespace JONMVC.Website.Models.Checkout
{
    public class DiamondCartItem : ICartItem
    {
        private readonly int id;
        private decimal price;

        public CartItemType Type
        {
            get { return CartItemType.Diamond;}
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
            return "N/A";
        }

        public void SetSize(string size)
        {
            throw new System.NotImplementedException();
        }

        public DiamondCartItem(int id,decimal price)
        {
            this.id = id;
            this.price = price;

        }

    }
}