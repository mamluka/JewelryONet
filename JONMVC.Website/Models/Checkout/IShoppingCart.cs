using System.Collections.Generic;

namespace JONMVC.Website.Models.Checkout
{
    public interface IShoppingCart
    {
        void AddItem(ICartItem standardItemViewModel);
        List<ICartItem> Items { get; }
        decimal TotalPrice { get; }
        void Remove(int cartId);
        void Update(int cartid,ICartItem updatedCartItem);
        int Count { get; }
    }
}