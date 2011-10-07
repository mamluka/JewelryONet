    using System;
using System.Collections.Generic;
using System.Text;
    using JONMVC.Website.Models.Checkout;
    using JONMVC.Website.Models.Jewelry;
    using JONMVC.Website.Models.Utils;
    using JONMVC.Website.Tests.Unit.Jewelry;
    using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class JewelCartItemTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Constructor_ShouldSetTheIDField()
        {
            //Arrange

            var price = Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT;
            var jewelCartItem = new JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, JewelMediaType.WhiteGold, price);
            //Act
            var id = jewelCartItem.ID;
            //Assert
            id.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);
        }

        [Test]
        public void Constructor_ShouldSetThePriceField()
        {
            //Arrange

            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            var jewelCartItem = new JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, JewelMediaType.WhiteGold, price);
            //Act
            var resultprice = jewelCartItem.Price;
            //Assert
            resultprice.Should().Be(price);
        }

        [Test]
        public void Constructor_ShouldSetTheTypeCorrectly()
        {
            //Arrange
     
            var price = Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT;
            var jewelCartItem = new JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, JewelMediaType.WhiteGold, price);
            //Act
            var type = jewelCartItem.Type;
            //Assert
            type.Should().Be(CartItemType.Jewelry);
        }

        [Test]
        public void Constructor_ShouldSetTheMediaTypeCorrectly()
        {
            //Arrange

            var price = Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT;
            var jewelCartItem = new JewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, JewelMediaType.YellowGold, price);
            //Act
            var type = jewelCartItem.MediaType;
            //Assert
            type.Should().Be(JewelMediaType.YellowGold);
        }


    }
}