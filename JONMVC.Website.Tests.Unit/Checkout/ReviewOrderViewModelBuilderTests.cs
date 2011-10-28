using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class ReviewOrderViewModelBuilderTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>

        [Test]
        public void Build_ShouldMapTheFieldsCorrectly()
        {
            //Arrange
            var builder = CreateDefaultReviewOrderViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert

            viewModel.TotalPrice.Should().Be("$10,000");
            viewModel.CartItems.Should().HaveCount(1);

            viewModel.BillingAddress.Address1.Should().Be("billingaddr1");
            viewModel.BillingAddress.City.Should().Be("billingcity");
            viewModel.BillingAddress.Country.Should().Be("billingcountry");
            viewModel.BillingAddress.FirstName.Should().Be("billingfirstname");
            viewModel.BillingAddress.LastName.Should().Be("billinglastname");
            viewModel.BillingAddress.Phone.Should().Be("billingphone");
            viewModel.BillingAddress.State.Should().Be("billingstate");
            viewModel.BillingAddress.ZipCode.Should().Be("billingzipcode");

            viewModel.ShippingAddress.Address1.Should().Be("shippingaddr1");
            viewModel.ShippingAddress.City.Should().Be("shippingcity");
            viewModel.ShippingAddress.Country.Should().Be("shippingcountry");
            viewModel.ShippingAddress.FirstName.Should().Be("shippingfirstname");
            viewModel.ShippingAddress.LastName.Should().Be("shippinglastname");
            viewModel.ShippingAddress.Phone.Should().Be("shippingphone");
            viewModel.ShippingAddress.State.Should().Be("shippingstate");
            viewModel.ShippingAddress.ZipCode.Should().Be("shippingzipcode");


        }

        [Test]
        public void Build_ShouldMapTheCreditCardCorrectly()
        {
            //Arrange
            var builder = CreateDefaultReviewOrderViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert

            viewModel.CreditCardViewModel.CCV.Should().Be("000");
            viewModel.CreditCardViewModel.CreditCardID.Should().Be(1);
            viewModel.CreditCardViewModel.CreditCardsNumber.Should().Be("12345");
            viewModel.CreditCardViewModel.Month.Should().Be(1);
            viewModel.CreditCardViewModel.Year.Should().Be(2011);
            

        }

        [Test]
        public void Build_ShouldSetTheCartItemsToNotShoeAction()
        {
            //Arrange
            var builder = CreateDefaultReviewOrderViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert

            //viewModel.CartItems.Should().Match(x=> x.)
            


        }

        private ReviewOrderViewModelBuilder CreateDefaultReviewOrderViewModelBuilder()
        {
            var checkoutDetailsModel = DefaultCheckoutDetailsModel();


            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.TotalPrice).Return(10000);
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                                             (decimal) 9999.99)
                                                       });

            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);
            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelryRepository, diamondRepository,
                                                                        mapper);

            var builder = new ReviewOrderViewModelBuilder(checkoutDetailsModel, shoppingCart, cartItemViewModelBuilder, mapper);
            return builder;
        }
    }
}