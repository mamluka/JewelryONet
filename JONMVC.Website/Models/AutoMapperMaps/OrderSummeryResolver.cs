using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;
using NMoneys;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class OrderSummeryResolver:ValueResolver<List<Order>,List<OrderSummeryViewModel>>
    {
    
   

        protected override List<OrderSummeryViewModel> ResolveCore(List<Order> source)
        {
            return source.Select(order => new OrderSummeryViewModel()
                                              {
                                                  OrderNumber = order.OrderNumber.ToString(), 
                                                  Status = CustomAttributes.GetDescription(order.Status), 
                                                  PurchaseDate = order.CreateDate.ToShortDateString(),
                                                  TotalPrice = new Money(order.TotalPrice, Currency.Usd).Format("{1}{0:#,0}")

                                              }).ToList();
        }
    }
}