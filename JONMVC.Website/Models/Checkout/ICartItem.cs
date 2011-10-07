using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JONMVC.Website.Models.Checkout
{
    public interface ICartItem
    {
        CartItemType Type { get; }
        decimal Price { get; }
        int ID { get; }
        string GetSize();
        void SetSize(string size);

    }
}
