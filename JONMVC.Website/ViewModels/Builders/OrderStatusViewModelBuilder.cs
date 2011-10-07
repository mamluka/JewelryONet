using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class OrderStatusViewModelBuilder
    {
        private readonly IMappingEngine mapper;
        private readonly IOrderRepository orderRepository;

        public OrderStatusViewModelBuilder(IMappingEngine mapper, IOrderRepository orderRepository)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
        }

        public OrderStatusViewModel Build(int orderNumber)
        {
            var order =  orderRepository.GetOrderByOrderNumber(orderNumber);
            return mapper.Map<Order, OrderStatusViewModel>(order);
        }
    }
}