using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class CustomJewelCartItemTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {

        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightPrice()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold, price);

            //Assert

            cartItem.Price.Should().Be(price);

        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightType()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold, price);

            //Assert

            cartItem.Type.Should().Be(CartItemType.CustomJewel);

        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightIDWhichIsProducedByPrimeMultiplicationOfTheDiamondAndSettingIDs()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold, price);

            //Assert

            cartItem.ID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID*3 +
                                    Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID*7);

        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightSettingID()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold, price);

            //Assert

            cartItem.SettingID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

        }


        [Test]
        public void Cunstructor_ShouldReturnTheRightDiamondID()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT, JewelMediaType.WhiteGold, price);

            //Assert

            cartItem.DiamondID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);

        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightSize()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING, JewelMediaType.WhiteGold, price);

            //Assert

            cartItem.Size.Should().Be(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING);

        }


        [Test]
        public void Cunstructor_ShouldUpdateTheSize()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING, JewelMediaType.WhiteGold, price);


            //Act
                        cartItem.SetSize("8");
            //Assert



            var size = cartItem.GetSize();

            size.Should().Be("8");

        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightMediaType()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;



            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING,
                                                   JewelMediaType.WhiteGold, price);
            //Assert


            cartItem.MediaType.Should().Be(JewelMediaType.WhiteGold);


        }

        [Test]
        public void Cunstructor_ShouldReturnTheRightMediaTypeWhenYellowGold()
        {
            //Arrange
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;



            //Act
            var cartItem = new CustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                   Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING,
                                                   JewelMediaType.YellowGold, price);
            //Assert


            cartItem.MediaType.Should().Be(JewelMediaType.YellowGold);


        }
    }
}