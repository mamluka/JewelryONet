using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class OrderBuilderTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Build_ShouldMapTheAddressesCorrectly()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            //Act
            var order = builder.Build(details);
            //Assert

            order.BillingAddress.Address1.Should().Be("billingaddr1");
            order.BillingAddress.City.Should().Be("billingcity");
            order.BillingAddress.CountryID.Should().Be(10);
            order.BillingAddress.FirstName.Should().Be("billingfirstname");
            order.BillingAddress.LastName.Should().Be("billinglastname");
            order.BillingAddress.Phone.Should().Be("billingphone");
            order.BillingAddress.StateID.Should().Be(20);
            order.BillingAddress.ZipCode.Should().Be("billingzipcode");

            order.ShippingAddress.Address1.Should().Be("shippingaddr1");
            order.ShippingAddress.City.Should().Be("shippingcity");
            order.ShippingAddress.CountryID.Should().Be(10);
            order.ShippingAddress.FirstName.Should().Be("shippingfirstname");
            order.ShippingAddress.LastName.Should().Be("shippinglastname");
            order.ShippingAddress.Phone.Should().Be("shippingphone");
            order.ShippingAddress.StateID.Should().Be(20);
            order.ShippingAddress.ZipCode.Should().Be("shippingzipcode");

        }

        [Test]
        public void Build_ShouldMapTheOrderTotalPrice()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            //Act
            var order = builder.Build(details);
            //Assert

            order.TotalPrice.Should().Be(6000);

        }

        [Test]
        public void Build_ShouldMapTheOrderComment()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            //Act
            var order = builder.Build(details);
            //Assert

            order.Comment.Should().Be("comment");

        }


        [Test]
        public void Build_ShouldMapTheUserDetailsThatAreNotReplatedToBilling()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            //Act
            var order = builder.Build(details);
            //Assert

            order.FirstName.Should().Be("firstname");
            order.Email.Should().Be("email");
            order.LastName.Should().Be("lastname");
            order.Phone.Should().Be("phone");
            

        }

        [Test]
        public void Build_ShouldMapPaymentIDCorrectlyWhenCraditCard()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            //Act
            var order = builder.Build(details);
            //Assert

            order.PaymentID.Should().Be(1);

        }

        [Test]
        public void Build_ShouldMapPaymentIDCorrectlyWhenWireTransfer()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            details.PaymentMethod=PaymentMethod.WireTransfer;
            //Act
            var order = builder.Build(details);
            //Assert

            order.PaymentID.Should().Be(2);

        }

        [Test]
        public void Build_ShouldMapPaymentIDCorrectlyWhenPayPal()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            details.PaymentMethod = PaymentMethod.PayPal;
            //Act
            var order = builder.Build(details);
            //Assert

            order.PaymentID.Should().Be(3);

        }

        [Test]
        public void Build_ShouldMapOrderItemsCorrectly()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            
            //Act
            var order = builder.Build(details);
            //Assert

            order.Items.Should().HaveCount(3);

        }

        [Test]
        public void Build_ShouldMapCreditCardCorrectly()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();

            //Act
            var order = builder.Build(details);
            //Assert

            order.CreditCard.CCV.Should().Be("000");
            order.CreditCard.CreditCardID.Should().Be(1);
            order.CreditCard.CreditCardsNumber.Should().Be("12345");
            order.CreditCard.Month.Should().Be(1);
            order.CreditCard.Year.Should().Be(2011);



        }

        [Test]
        public void Build_ShouldMapCreditCardEvenIfCreditCardOnTheDetailsIsNull()
        {
            //Arrange
            var builder = CreateDefaultOrderBuilder();

            var details = DefaultCheckoutDetailsModel();
            details.CreditCardViewModel = null;
            //Act
            var order = builder.Build(details);
            //Assert

            order.CreditCard.Should().BeNull();

        }

        [Test]
        public void Build_ShouldCheckIfUserIsAuthenticated()
        {
            //Arrange
            var authentication = MockRepository.GenerateMock<IAuthentication>();
            authentication.Expect(x => x.IsSignedIn()).Return(false);

            var builder = CreateDefaultOrderBuilderWithCustomeAuthentication(authentication);

            var details = DefaultCheckoutDetailsModel();

            //Act
            builder.Build(details);
            //Assert

            authentication.VerifyAllExpectations();

        }

        [Test]
        public void Build_ShouldLoadTheCustomerDataToTheOrderWhenUserIsSignedIn()
        {
            //Arrange
            var customerData = fixture.CreateAnonymous<Customer>();

            var authentication = MockRepository.GenerateMock<IAuthentication>();
            authentication.Stub(x => x.IsSignedIn()).Return(true);
            authentication.Stub(x => x.CustomerData).Return(customerData);

            var builder = CreateDefaultOrderBuilderWithCustomeAuthentication(authentication);

            var details = DefaultCheckoutDetailsModel();

            //Act
            var order = builder.Build(details);
            //Assert
            order.Email.Should().Be(customerData.Email);
            order.FirstName.Should().Be(customerData.FirstName);
            order.LastName.Should().Be(customerData.LastName);
            order.Phone.Should().Be(details.BillingAddress.Phone);

        }


        private OrderBuilder CreateDefaultOrderBuilder()
        {
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();

            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold, 
                                                               1000),
                                                           FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID, 
                                                               2000),
                                                           FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold, 
                                                               3000)
                                                       });

            shoppingCart.Stub(x => x.TotalPrice).Return(6000);

            var authentication = MockRepository.GenerateStub<IAuthentication>();

            var builder = new OrderBuilder(shoppingCart, authentication, mapper);
            return builder;
        }

        private OrderBuilder CreateDefaultOrderBuilderWithCustomeAuthentication(IAuthentication authentication)
        {
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();

            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>());

            var builder = new OrderBuilder(shoppingCart,authentication, mapper);
            return builder;
        }
    }
}