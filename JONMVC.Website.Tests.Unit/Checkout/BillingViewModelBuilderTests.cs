using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class BillingViewModelBuilderTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Build_ShouldMapTheFieldsCorrectly()
        {
            //Arrange
            var checkoutDetailsModel = fixture.CreateAnonymous<CheckoutDetailsModel>();
            var authentication = MockRepository.GenerateStub<IAuthentication>();

            var customerAccountService = CreateCustomerAccountServiceWithExtendedCustomer();

            var builder = new BillingViewModelBuilder(checkoutDetailsModel, authentication, customerAccountService, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Email.Should().Be(checkoutDetailsModel.Email);
            viewModel.FirstName.Should().Be(checkoutDetailsModel.FirstName);
            viewModel.LastName.Should().Be(checkoutDetailsModel.LastName);
            viewModel.PaymentMethod.Should().Be(checkoutDetailsModel.PaymentMethod);
            viewModel.Phone.Should().Be(checkoutDetailsModel.Phone);

            viewModel.BillingAddress.FirstName.Should().Be(checkoutDetailsModel.FirstName);
            viewModel.BillingAddress.LastName.Should().Be(checkoutDetailsModel.LastName);
            viewModel.BillingAddress.Phone.Should().Be(checkoutDetailsModel.Phone);

            viewModel.ShippingAddress.FirstName.Should().Be(checkoutDetailsModel.FirstName);
            viewModel.ShippingAddress.LastName.Should().Be(checkoutDetailsModel.LastName);
            viewModel.ShippingAddress.Phone.Should().Be(checkoutDetailsModel.Phone);


        }

        [Test]
        public void Build_ShouldCheckIfTheCustomerIsSignedIn()
        {
            //Arrange
            
            var checkoutDetailsModel = fixture.CreateAnonymous<CheckoutDetailsModel>();

            var authentication = MockRepository.GenerateStub<IAuthentication>();
            authentication.Expect(x => x.IsSignedIn()).Return(false);

            var customerAccountService = CreateCustomerAccountServiceWithExtendedCustomer();
            var builder = new BillingViewModelBuilder(checkoutDetailsModel, authentication, customerAccountService, mapper);
            //Act
            builder.Build();
            //Assert
            authentication.VerifyAllExpectations();

        }

        [Test]
        public void Build_ShouldMapTheExtendedCustomerIfTheCustomerIsSignedIn()
        {
            //Arrange

            var checkoutDetailsModel = fixture.CreateAnonymous<CheckoutDetailsModel>();
            var customerData = fixture.CreateAnonymous<Customer>();
            var extendedCustomer = fixture.CreateAnonymous<ExtendedCustomer>();

            var authentication = CreateAuthenticationWithCustomerData(customerData);

            var customerAccountService =  CreateCustomerAccountServiceWithExtendedCustomer(extendedCustomer);

            var builder = new BillingViewModelBuilder(checkoutDetailsModel, authentication, customerAccountService, mapper);
            //Act
            var viewModel  = builder.Build();
            //Assert
            viewModel.ShippingAddress.Address1.Should().Be(extendedCustomer.ShippingAddress.Address1);
            viewModel.ShippingAddress.City.Should().Be(extendedCustomer.ShippingAddress.City);
            viewModel.ShippingAddress.CountryID.Should().Be(extendedCustomer.ShippingAddress.CountryID);
            viewModel.ShippingAddress.FirstName.Should().Be(extendedCustomer.ShippingAddress.FirstName);
            viewModel.ShippingAddress.LastName.Should().Be(extendedCustomer.ShippingAddress.LastName);
            viewModel.ShippingAddress.Phone.Should().Be(extendedCustomer.ShippingAddress.Phone);
            viewModel.ShippingAddress.ZipCode.Should().Be(extendedCustomer.ShippingAddress.ZipCode);

            viewModel.BillingAddress.Address1.Should().Be(extendedCustomer.BillingAddress.Address1);
            viewModel.BillingAddress.City.Should().Be(extendedCustomer.BillingAddress.City);
            viewModel.BillingAddress.CountryID.Should().Be(extendedCustomer.BillingAddress.CountryID);
            viewModel.BillingAddress.FirstName.Should().Be(extendedCustomer.BillingAddress.FirstName);
            viewModel.BillingAddress.LastName.Should().Be(extendedCustomer.BillingAddress.LastName);
            viewModel.BillingAddress.Phone.Should().Be(extendedCustomer.BillingAddress.Phone);
            viewModel.BillingAddress.ZipCode.Should().Be(extendedCustomer.BillingAddress.ZipCode);
        

        }

        [Test]
        public void Build_ShouldMapThePaymentMethodAsWasSelectedInTheCheckoutModel()
        {
            //Arrange

            var checkoutDetailsModel = fixture.Build<CheckoutDetailsModel>().With(x=> x.PaymentMethod,PaymentMethod.PayPal).CreateAnonymous();
            var customerData = fixture.CreateAnonymous<Customer>();
            var extendedCustomer = fixture.CreateAnonymous<ExtendedCustomer>();

            var authentication = CreateAuthenticationWithCustomerData(customerData);

            var customerAccountService = CreateCustomerAccountServiceWithExtendedCustomer(extendedCustomer);

            var builder = new BillingViewModelBuilder(checkoutDetailsModel, authentication, customerAccountService, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.PaymentMethod.Should().Be(checkoutDetailsModel.PaymentMethod);


        }

        private ICustomerAccountService CreateCustomerAccountServiceWithExtendedCustomer(ExtendedCustomer extendedCustomer)
        {
            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            customerAccountService.Stub(x => x.GetExtendedCustomerByEmail(Arg<string>.Is.Anything)).
                Return(extendedCustomer);

            return customerAccountService;
        }

        private ICustomerAccountService CreateCustomerAccountServiceWithExtendedCustomer()
        {
            var extendedCustomer = fixture.CreateAnonymous<ExtendedCustomer>();
            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            customerAccountService.Stub(x => x.GetExtendedCustomerByEmail(Arg<string>.Is.Anything)).
                Return(extendedCustomer);

            return customerAccountService;
        }

    
        private static IAuthentication CreateAuthenticationWithCustomerData(Customer customerData)
        {
            var authentication = MockRepository.GenerateStub<IAuthentication>();
            authentication.Stub(x => x.IsSignedIn()).Return(true);
            authentication.Stub(x => x.CustomerData).Return(customerData);
            return authentication;
        }
    }
}