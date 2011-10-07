using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Fakes;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class ShoppingCartViewModelBuilderTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
   

        [Test]
        public void Build_ShouldReturnTheRightTotalPrice()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>());

            shoppingCart.Stub(x => x.TotalPrice).Return(20000);
            var builder = CreateDefaultShoppingCartViewModelBuilderWithShoppingCartAsParameter(shoppingCart);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.TotalPrice.Should().Be("$20,000");

        }

        [Test]
        public void Build_ShouldReturnCartItemsWith1ItemInside()
        {
            //Arrange
            var builder = CreateDefaultShoppingCartViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.CartItems.Should().HaveCount(1);
        }

        [Test]
        public void Build_ShouldTryToDetermiteIfTheCustomerIsSignedIn()
        {
            //Arrange
            var authentication = MockRepository.GenerateMock<IAuthentication>();
            authentication.Expect(x => x.IsSignedIn()).Repeat.Once().Return(false);


            var builder = CreateDefaultShoppingCartViewModelBuilderWithCustomAuthentication(authentication);
            //Act
            var viewModel = builder.Build();
            //Assert
            authentication.VerifyAllExpectations();
        }   

        [Test]
        public void Build_ShouldReturnUserSignedInAsFalseIfTheUserIsNotSignedIn()
        {
            //Arrange
      
            var customerData = fixture.CreateAnonymous<Customer>();

            var authentication = CreateAuthenticationWithCustomerDataAndSignedInSetTo(false, customerData);


            var builder = CreateDefaultShoppingCartViewModelBuilderWithCustomAuthentication(authentication);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.IsSignedIn.Should().BeFalse();
        }

        [Test]
        public void Build_ShouldReturnUserSignedInAsTruefTheUserIsIndeedSignedIn()
        {
            //Arrange
        
            var customerData = fixture.CreateAnonymous<Customer>();

            var authentication = CreateAuthenticationWithCustomerDataAndSignedInSetTo(true, customerData);


            var builder = CreateDefaultShoppingCartViewModelBuilderWithCustomAuthentication(authentication);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.IsSignedIn.Should().BeTrue();
        }

        [Test]
        public void Build_ShouldReturnEmailAddressAsInTheSignedInCustomerData()
        {
            //Arrange
            var customerData = fixture.CreateAnonymous<Customer>();

            var authentication = CreateAuthenticationWithCustomerDataAndSignedInSetTo(true,customerData);


            var builder = CreateDefaultShoppingCartViewModelBuilderWithCustomAuthentication(authentication);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Email.Should().Be(customerData.Email);
        }

        

        [Test]
        public void Build_ShouldCallTheFilterMetalOnTheRepositoryWithTheCorrectMediaType()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>() { FakeJewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.YellowGold,Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT) });
            shoppingCart.Stub(x => x.TotalPrice).Return(10000);

            var fakeJewel = new FakeJewelRepository(new FakeSettingManager()).GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            var jewelryRepository = MockRepository.GenerateMock<IJewelRepository>();
            jewelryRepository.Stub(x => x.GetJewelByID(Arg<int>.Is.Anything)).Return(fakeJewel);
            jewelryRepository.Expect(x => x.FilterMediaByMetal(Arg<JewelMediaType>.Is.Equal(JewelMediaType.YellowGold)))
                .Repeat.Once();

            var diamondRepository = new FakeDiamondRepository(mapper);

            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelryRepository, diamondRepository, mapper);

            var authentication = MockRepository.GenerateStub<IAuthentication>();
            var builder = new ShoppingCartViewModelBuilder(shoppingCart, jewelryRepository, cartItemViewModelBuilder, authentication, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            jewelryRepository.VerifyAllExpectations();
            

        }

        private ShoppingCartViewModelBuilder CreateDefaultShoppingCartViewModelBuilder()
        {
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                                             Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);

            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelryRepository, diamondRepository,
                                                                        mapper);

            var authentication = MockRepository.GenerateStub<IAuthentication>();

            var builder = new ShoppingCartViewModelBuilder(shoppingCart, jewelryRepository, cartItemViewModelBuilder, authentication, mapper);
            return builder;
        }

        private ShoppingCartViewModelBuilder CreateDefaultShoppingCartViewModelBuilderWithShoppingCartAsParameter(IShoppingCart shoppingCart)
        {
            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);

            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelryRepository, diamondRepository,
                                                                        mapper);

            var authentication = MockRepository.GenerateStub<IAuthentication>();

            var builder = new ShoppingCartViewModelBuilder(shoppingCart, jewelryRepository, cartItemViewModelBuilder, authentication, mapper);
            return builder;
        }

        private ShoppingCartViewModelBuilder CreateDefaultShoppingCartViewModelBuilderWithCustomAuthentication(IAuthentication authentication)
        {
            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);
            var shoppingCart = FakeFactory.ShoppingCartWith3Items();
            

            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelryRepository, diamondRepository,
                                                                        mapper);

            

            var builder = new ShoppingCartViewModelBuilder(shoppingCart, jewelryRepository, cartItemViewModelBuilder,authentication, mapper);
            return builder;
        }

        private static IAuthentication CreateAuthenticationWithCustomerDataAndSignedInSetTo( bool isSignedIn,Customer customerData)
        {
            var authentication = MockRepository.GenerateStub<IAuthentication>();
            authentication.Stub(x => x.IsSignedIn()).Return(isSignedIn);


            authentication.Stub(x => x.CustomerData).Return(customerData);
            return authentication;
        }


    }
}