using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.MyAccount
{
    [TestFixture]
    public class MyAccountViewModelBuilderTests:MyAccountTestsBase
    {

        [Test]
        public void Build_ShouldReadTheOrdersOfTheLoggedInUser()
        {
            //Arrange
            var userEmail = fixture.CreateAnonymous("email");

            var orderRepository = MockRepository.GenerateMock<IOrderRepository>();
            orderRepository.Expect(x => x.GetOrdersByCustomerEmail(Arg<string>.Is.Equal(userEmail))).Repeat.Once();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            

            var builder = new MyAccountViewModelBuilder(userEmail,customerAccountService,orderRepository,mapper);
            //Act
            builder.Build();
            //Assert
            orderRepository.VerifyAllExpectations();
        }

        [Test]
        public void Build_ShouldReadTheCustomerExtraDetails()
        {
            //Arrange
            var customer = fixture.CreateAnonymous<ExtendedCustomer>();

            var userEmail = fixture.CreateAnonymous("email");

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();
            orderRepository.Stub(x => x.GetOrdersByCustomerEmail(Arg<string>.Is.Anything));
             
            var customerAccountService = MockRepository.GenerateMock<ICustomerAccountService>();
            customerAccountService.Expect(x => x.GetExtendedCustomerByEmail(Arg<string>.Is.Equal(userEmail)))
                .Repeat.Once().Return(customer);


            var builder = new MyAccountViewModelBuilder(userEmail, customerAccountService, orderRepository, mapper);
            //Act
            builder.Build();
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void Build_ShouldMapTheOrderSummery()
        {
            //Arrange
            var userEmail = fixture.CreateAnonymous("email");

            var order = fixture.CreateAnonymous<Order>();

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();
            orderRepository.Stub(x => x.GetOrdersByCustomerEmail(Arg<string>.Is.Anything))
                .Return(new List<Order> {order});

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
           



            var builder = new MyAccountViewModelBuilder(userEmail, customerAccountService, orderRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            var orderSummery = viewModel.Orders[0];
            orderSummery.OrderNumber.Should().Be(order.OrderNumber.ToString());
            orderSummery.PurchaseDate.Should().Be(order.CreateDate.ToShortDateString());
            orderSummery.TotalPrice.Should().Be(Tests.AsMoney(order.TotalPrice));
            orderSummery.Status.Should().Be(CustomAttributes.GetDescription(order.Status));

        }

        [Test]
        public void Build_ShouldMapTheCustomerDetails()
        {
            //Arrange
            var customer = fixture.CreateAnonymous<ExtendedCustomer>();

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();
            orderRepository.Stub(x => x.GetOrdersByCustomerEmail(Arg<string>.Is.Anything));

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            customerAccountService.Stub(x => x.GetExtendedCustomerByEmail(Arg<string>.Is.Anything)).Return(customer);


            var builder = new MyAccountViewModelBuilder(customer.Email, customerAccountService, orderRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.BillingAddress.Address1.Should().Be(customer.BillingAddress.Address1);
            viewModel.BillingAddress.City.Should().Be(customer.BillingAddress.City);
            viewModel.BillingAddress.Country.Should().Be(customer.BillingAddress.Country);
            viewModel.BillingAddress.FirstName.Should().Be(customer.BillingAddress.FirstName);
            viewModel.BillingAddress.State.Should().Be(customer.BillingAddress.State);
            viewModel.BillingAddress.LastName.Should().Be(customer.BillingAddress.LastName);
            viewModel.BillingAddress.ZipCode.Should().Be(customer.BillingAddress.ZipCode);
            viewModel.BillingAddress.Phone.Should().Be(customer.BillingAddress.Phone);

            viewModel.ShippingAddress.Address1.Should().Be(customer.ShippingAddress.Address1);
            viewModel.ShippingAddress.City.Should().Be(customer.ShippingAddress.City);
            viewModel.ShippingAddress.Country.Should().Be(customer.ShippingAddress.Country);
            viewModel.ShippingAddress.FirstName.Should().Be(customer.ShippingAddress.FirstName);
            viewModel.ShippingAddress.State.Should().Be(customer.ShippingAddress.State);
            viewModel.ShippingAddress.LastName.Should().Be(customer.ShippingAddress.LastName);
            viewModel.ShippingAddress.ZipCode.Should().Be(customer.ShippingAddress.ZipCode);
            viewModel.ShippingAddress.Phone.Should().Be(customer.ShippingAddress.Phone);

            viewModel.Email.Should().Be(customer.Email);
            viewModel.FirstName.Should().Be(customer.FirstName);
            viewModel.LastName.Should().Be(customer.LastName);
            viewModel.Country.Should().Be(customer.Country);
            viewModel.State.Should().Be(customer.State);
            viewModel.Phone.Should().Be(customer.Phone);
            viewModel.MemeberSince.Should().Be(customer.MemeberSince.ToShortDateString());

        }

    }
}