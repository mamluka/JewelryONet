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
using MvcContrib.TestHelper;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class ShoppingCartItemsFactoryTests : CheckoutTestsBaseClass
    {
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void FromIDAndSize_ShouldReturnTheRightIDForTheCartItem()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold);
            //Assert
        
            cartItem.ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);
        }

      

        [Test]
        public void FromIDAndSize_ShouldReturnTheRightSizeForTheItem()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold);
            //Assert
          
            cartItem.Size.Should().Be(Tests.SAMPLE_JEWEL_SIZE_725);
        }

        [Test]
        public void FromIDAndSize_ShouldReturnTheRightMediaTypeForTheItemWhenSetToYellowGold()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.YellowGold);
            //Assert
         
            cartItem.MediaType.Should().Be(JewelMediaType.YellowGold);
        }

        [Test]
        public void FromIDAndSize_ShouldReturnTheRightPriceForTheItem()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.YellowGold);
            //Assert

            cartItem.Price.Should().Be((decimal) 9999.99);
        }

        [Test]
        public void DiamondCartItem_ShouldReturnTheRightID()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.DiamondCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);
            //Assert
          
            cartItem.ID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);
        }

        [Test]
        public void DiamondCartItem_ShouldReturnTheRightPrice()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.DiamondCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);
            //Assert
    
            cartItem.Price.Should().Be(25478);
        }

        [Test]
        public void DiamondCartItem_ShouldReturnTheRightType()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.DiamondCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);
            //Assert

            cartItem.Type.Should().Be(CartItemType.Diamond);
        }

        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightPrice()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold);

            //Assert

            cartItem.Price.Should().Be((decimal) (9999.99 + 25478));

        }

        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightSize()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold);

            //Assert

            cartItem.Size.Should().Be(Tests.SAMPLE_JEWEL_SIZE_725);

        }


        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightType()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold);

            //Assert

            cartItem.Type.Should().Be(CartItemType.CustomJewel);

        }

        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightIDWhichIsProducedByPrimeMultiplicationOfTheDiamondAndSettingIDs()
        {
            //Arrange
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold);

            //Assert

            cartItem.ID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID * 3 + Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID * 7);

        }

        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightSettingID()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold);

            //Assert

            cartItem.SettingID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

        }


        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightDiamondID()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold);

            //Assert

            cartItem.DiamondID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);

        }
        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightMediaType()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold);

            //Assert

            cartItem.MediaType.Should().Be(JewelMediaType.WhiteGold);

        }

        [Test]
        public void CustomJewelCartItem_ShouldReturnTheRightMediaTypeWhenYellowGold()
        {
            //Arrange
            var factory = CreateDefaultShoppingCartItemsFactory();
            //Act
            var cartItem = factory.CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.YellowGold);

            //Assert

            cartItem.MediaType.Should().Be(JewelMediaType.YellowGold);

        }


        private ShoppingCartItemsFactory CreateDefaultShoppingCartItemsFactory()
        {
            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);
            var factory = new ShoppingCartItemsFactory(jewelryRepository,diamondRepository);
            return factory;
        }
    }
}