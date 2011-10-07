using System.Web;

namespace JONMVC.Website.Models.Checkout
{
    public class ShoppingCartWrapper : IShoppingCartWrapper
    {
        private readonly HttpContextBase httpContextBase;

        public ShoppingCartWrapper(HttpContextBase httpContextBase)
        {
            this.httpContextBase = httpContextBase;
        }

        public IShoppingCart Get()
        {
            var session = httpContextBase.Session;
            if (session["cart"] != null)
            {
                return (ShoppingCart) session["cart"];
            }
            return new ShoppingCart();
        }

        public void Presist(IShoppingCart shoppingCart,HttpContextBase httpContextBase)
        {
            httpContextBase.Session["cart"] = shoppingCart;
        }

        public void Clear()
        {
            var session = httpContextBase.Session;
            session["cart"] = null;
        }
    }
}