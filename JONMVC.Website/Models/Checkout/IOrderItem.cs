namespace JONMVC.Website.Models.Checkout
{
    public interface IOrderItem
    {
        string Description { get; set; }
        int Quantity { get; set; }
        int ID { get; set; }
        string JewelSize { get; set; }
    }
}