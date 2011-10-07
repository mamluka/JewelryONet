using System.Linq;
using AutoMapper;

namespace JONMVC.Website.Models.Checkout
{
    public class OrderBuilder
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IAuthentication authentication;
        private readonly IMappingEngine mapper;

        public OrderBuilder(IShoppingCart shoppingCart, IAuthentication authentication, IMappingEngine mapper)
        {
            this.shoppingCart = shoppingCart;
            this.authentication = authentication;
            this.mapper = mapper;
        }

        public Order Build(CheckoutDetailsModel details)
        {
            var order = mapper.Map<CheckoutDetailsModel, Order>(details);
            order.TotalPrice = shoppingCart.TotalPrice;
            order.Items = shoppingCart.Items;
            if (authentication.IsSignedIn())
            {
                var customer = authentication.CustomerData;
                order.FirstName = customer.Firstname;
                order.LastName = customer.Lastname;
                order.Email = customer.Email;
                order.Phone = order.BillingAddress.Phone;
            }
            return order;


        }
    }
}