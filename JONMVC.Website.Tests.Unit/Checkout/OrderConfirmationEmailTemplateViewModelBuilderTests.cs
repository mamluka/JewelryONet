using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Fakes;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class OrderConfirmationEmailTemplateViewModelBuilderTests:CheckoutTestsBaseClass
    {

        [Test]
        public void Build_ShouldSetThePersonalInformationFieldsCorrectly()
        {
            //Arrange
            var orderNumber = fixture.CreateAnonymous("OrderNumber");
            var model = fixture.CreateAnonymous<CheckoutDetailsModel>();
            var builder = CreateDefaultOrderConfirmationEmailTemplateViewModelBuilder(orderNumber, model);
            //Act
            var emailTemplate = builder.Build();
            //Assert
            emailTemplate.Email.Should().Be(model.Email);
            emailTemplate.OrderNumber = orderNumber;
            emailTemplate.Name.Should().Be(model.FirstName + " " + model.LastName);
        }

      

        [Test]
        public void Build_ShouldSetTheItemsCorrectlyToHaveTheRightCount()
        {
            //Arrange
            var orderNumber = fixture.CreateAnonymous("OrderNumber");
            var model = fixture.CreateAnonymous<CheckoutDetailsModel>();
            var builder = CreateDefaultOrderConfirmationEmailTemplateViewModelBuilder(orderNumber, model);

            //Act
            var emailTemplate = builder.Build();
            //Assert
            emailTemplate.Items.Should().HaveCount(3);
        }

        [Test] 
        public void Build_ShouldSetThePaymentMethodCorrectly()
        {
            //Arrange
            var orderNumber = fixture.CreateAnonymous("OrderNumber");
            var model = fixture.Build<CheckoutDetailsModel>().With(x=> x.PaymentMethod,PaymentMethod.PayPal).CreateAnonymous();
            var builder = CreateDefaultOrderConfirmationEmailTemplateViewModelBuilder(orderNumber, model);

            //Act
            var emailTemplate = builder.Build();
            //Assert
            emailTemplate.PaymentMethod.Should().Be(PaymentMethod.PayPal);
        }

        [Test]
        public void Build_ShouldSetTheCreditCartCorrectly()
        {
            //Arrange
            var orderNumber = fixture.CreateAnonymous("OrderNumber");
            var model = fixture.Build<CheckoutDetailsModel>().CreateAnonymous();
            var builder = CreateDefaultOrderConfirmationEmailTemplateViewModelBuilder(orderNumber, model);

            //Act
            var emailTemplate = builder.Build();
            //Assert
            var length = model.CreditCardViewModel.CreditCardsNumber.Length;
            var zeroBaseIndex = length - 4;
            emailTemplate.CCLast4Digits.Should().Be(model.CreditCardViewModel.CreditCardsNumber.Substring(zeroBaseIndex, 4));
            emailTemplate.CCType.Should().Be(model.CreditCardViewModel.CreditCart);
        }

        [Test]
        public void Build_ShouldSetTheCreditCardToBeEmptyStringWhenTheMethodIsNotCreditCard()
        {
            //Arrange
            var orderNumber = fixture.CreateAnonymous("OrderNumber");
            var model = fixture.Build<CheckoutDetailsModel>().With(x=> x.PaymentMethod,PaymentMethod.PayPal).CreateAnonymous();
            var builder = CreateDefaultOrderConfirmationEmailTemplateViewModelBuilder(orderNumber, model);

            //Act
            var emailTemplate = builder.Build();
            //Assert

            emailTemplate.CCLast4Digits.Should().BeNull();
            emailTemplate.CCType.Should().BeNull();
        }



        private OrderConfirmationEmailTemplateViewModelBuilder CreateDefaultOrderConfirmationEmailTemplateViewModelBuilder(
            string orderNumber, CheckoutDetailsModel model)
        {
            var shoppingCart = FakeFactory.ShoppingCartWith3Items();


            var jewelRepostory = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);
            var cartItemBuilder = new CartItemViewModelBuilder(jewelRepostory, diamondRepository, mapper);


            var builder = new OrderConfirmationEmailTemplateViewModelBuilder(orderNumber, model, shoppingCart, cartItemBuilder);
            return builder;
        }


    }
}