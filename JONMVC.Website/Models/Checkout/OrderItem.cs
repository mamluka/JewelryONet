namespace JONMVC.Website.Models.Checkout
{
    public class OrderItem : IOrderItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ID { get; set; }
        public string JewelSize { get; set; }
    }
}