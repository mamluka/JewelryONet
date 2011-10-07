using System.Collections.Generic;

namespace JONMVC.Website.Models.Checkout
{
    public interface IOrderRepository
    {
        int Save(Order orderdto);
        Order GetOrderByOrderNumber(int orderNumber);
        List<Order> GetOrdersByCustomerEmail(string email);
    }
}