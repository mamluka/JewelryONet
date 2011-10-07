using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JONMVC.Website.Models.Checkout
{
    public interface IShoppingCartWrapper
    {
        IShoppingCart Get();
        void Presist(IShoppingCart shoppingCart,HttpContextBase httpContextBase);
        void Clear();

    }
}
