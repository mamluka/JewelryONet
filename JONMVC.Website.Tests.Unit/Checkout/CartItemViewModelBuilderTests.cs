using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
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
    public class CartItemViewModelBuilderTests : CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Build_ShouldReturnTheRightPriceForDiamondItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithDiamondCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as DiamondCartItemViewModel;
            item.Price.Should().Be("$25,478");

        }

       

        [Test]
        public void Build_ShouldReturnTheRightDescriptionForTheDiamondItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithDiamondCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as DiamondCartItemViewModel;
            item.Desciption.Should().Be("A 1.25 Ct. Round H/VS1 Diamond");

        }

        [Test]
        public void Build_ShouldReturnTheRightCartIDForTheDiamondItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithDiamondCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as DiamondCartItemViewModel;
            item.CartID.Should().Be(0);

        }

        [Test]
        public void Build_ShouldReturnTheRightIDForTheDiamondItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithDiamondCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as DiamondCartItemViewModel;
            item.ID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID.ToString());

        }


        [Test]
        public void Build_ShouldReturnTheRightIconForTheDiamondItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithDiamondCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as DiamondCartItemViewModel;
            item.Icon.Should().Be("/jon-images/diamond/"+"round-icon.png");

        }


        [Test]
        public void Build_ShouldReturnTheRightPriceForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.Price.Should().Be("$10,000");


        }

  


        [Test]
        public void Build_ShouldReturnTheRightSizeForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });


            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.JewelSize.Should().Be(Tests.SAMPLE_JEWEL_SIZE_725);


        }

        [Test]
        public void Build_ShouldReturnTheRightItemNumberForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });


            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.Itemnumber.Should().Be("0101-15421");


        }

        [Test]
        public void Build_ShouldReturnTheRightCartIDForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });


            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.CartID.Should().Be(0);


        }

        [Test]
        public void Build_ShouldReturnTheRightDescriptionForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });


            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.Desciption.Should().Be("title");


        }

        [Test]
        public void Build_ShouldReturnTheRightIconPathForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });


            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.Icon.Should().Be("/jon-images/jewel/0101-15421-icon-wg.jpg");

        }

        [Test]
        public void Build_ShouldReturnTheRightYellowGoldIconForTheJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.YellowGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });


            var builder = CartItemViewModelBuilderWithJewelCartItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as JewelCartItemViewModel;
            item.Icon.Should().Be("/jon-images/jewel/0101-15421-icon-yg.jpg");

        }

        [Test]
        public void Build_ShouldReturnTheRightDiamondIDForTheCustomJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithCustomJewelItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as CustomJewelCartItemViewModel;
            item.DiamondID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID.ToString());

        }


        [Test]
        public void Build_ShouldReturnTheRightSizeForTheCustomJewelItem()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithCustomJewelItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as CustomJewelCartItemViewModel;

            item.Size.Should().Be(Tests.SAMPLE_JEWEL_SIZE_725);

        }

        [Test]
        public void Build_ShouldReturnTheRightMediaTypeForTheSettingInTheItemWhenWhiteGoldOrDefault()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithCustomJewelItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as CustomJewelCartItemViewModel;

            item.Icon.Should().Match("*wg*");

        }

        [Test]
        public void Build_ShouldReturnTheRightMediaTypeForTheSettingInTheItemWhenYellowGold()
        {
            //Arrange
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.YellowGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var builder = CartItemViewModelBuilderWithCustomJewelItem();
            //Act
            var items = builder.Build(shoppingCart.Items);
            //Assert
            var item = items[0] as CustomJewelCartItemViewModel;

            item.Icon.Should().Match("*yg*");

        }




        private CartItemViewModelBuilder CartItemViewModelBuilderWithDiamondCartItem()
        {
            

            var jewelRepostory = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);


            var builder = new CartItemViewModelBuilder(jewelRepostory, diamondRepository, mapper);
            return builder;
        }

        private CartItemViewModelBuilder CartItemViewModelBuilderWithJewelCartItem()
        {
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, Tests.SAMPLE_JEWEL_SIZE_725,
                                                               JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var jewelRepostory = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);


            var builder = new CartItemViewModelBuilder(jewelRepostory, diamondRepository, mapper);
            return builder;
        }

        private CartItemViewModelBuilder CartItemViewModelBuilderWithCustomJewelItem()
        {
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {
                                                           FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                               Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT)
                                                       });

            var jewelRepostory = new FakeJewelRepository(new FakeSettingManager());
            var diamondRepository = new FakeDiamondRepository(mapper);


            var builder = new CartItemViewModelBuilder(jewelRepostory, diamondRepository, mapper);
            return builder;
        }


    }
}