using AutoMapper;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class MyAccountViewModelBuilder
    {
        private readonly string userEmail;
        private readonly ICustomerAccountService customerAccountService;
        private readonly IOrderRepository orderRepository;
        private readonly IMappingEngine mapper;

        public MyAccountViewModelBuilder(string userEmail, ICustomerAccountService customerAccountService, IOrderRepository orderRepository, IMappingEngine mapper)
        {
            this.userEmail = userEmail;
            this.customerAccountService = customerAccountService;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public MyAccountViewModel Build()
        {
            var viewModel = new MyAccountViewModel();
            var orders = orderRepository.GetOrdersByCustomerEmail(userEmail);
            var customer = customerAccountService.GetExtendedCustomerByEmail(userEmail);

            var mergeOrdersAndCustomer = new MergeOrdersAndCustomer();
            mergeOrdersAndCustomer.First = orders;
            mergeOrdersAndCustomer.Second = customer;

            viewModel = mapper.Map<MergeOrdersAndCustomer, MyAccountViewModel>(mergeOrdersAndCustomer);

            return viewModel;
        }
    }
}