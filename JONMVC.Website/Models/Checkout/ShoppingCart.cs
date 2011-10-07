using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Checkout
{
    public class ShoppingCart:IShoppingCart
    {
        private readonly HttpContextBase httpContext;

        private List<ICartItem> cartItems = new List<ICartItem>();
        private decimal totalPrice;

        public HttpContextBase HttpContext1
        {
            get { return httpContext; }
        }

        public List<ICartItem> Items
        {
            get { return cartItems; }
        }

        public decimal TotalPrice
        {
            get
            {
                RecalculateTotalPrice();
                return totalPrice;
            }
        }

        public int Count
        {
            get {
                return cartItems.Count;
            }
        }


//        public ShoppingCart GetShoppingCart(HttpContextBase httpContext)
//        {
//            var session = httpContext.Session;
//            if (session["cart"] != null)
//            {
//                return (ShoppingCart)session["cart"];
//            }
//            return  new ShoppingCart(new FakeHttpContext());
//        }
//
//        public static void Persist(HttpContextBase httpContext,IShoppingCart shoppingCart)
//        {
//            var session = httpContext.Session;
//            if (shoppingCart != null)
//            {
//                session["cart"] = shoppingCart;
//            }
//           
//        }


        public void AddItem(ICartItem standardItemViewModel)
        {
            cartItems.Add(standardItemViewModel);
            
        }

        

        public void Remove(int cartID)
        {
            cartItems.RemoveAt(cartID);

        }

        public void Update(int cartId, ICartItem updatedCartItem)
        {
            cartItems[cartId] = updatedCartItem;
        }

        private void RecalculateTotalPrice()
        {
            totalPrice = 0;
            foreach (var cartItem in Items)
            {
                totalPrice += cartItem.Price;
            }
        }

       
    }
    
}