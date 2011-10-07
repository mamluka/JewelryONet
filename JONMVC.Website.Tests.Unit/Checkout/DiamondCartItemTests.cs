using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Tests.Unit.Diamonds;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class DiamondCartItemTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Constructor_ShouldReturnTheRightIO()
        {
            //Arrange
            var diamondID = Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID;
            var price = Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT;
            //Act
            var cartItem = new DiamondCartItem(diamondID, price);
            //Assert
            cartItem.ID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);
        }

        [Test]
        public void Constructor_ShouldReturnTheRightPrice()
        {
            //Arrange
            var diamondID = Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID;
            var price = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            //Act
            var cartItem = new DiamondCartItem(diamondID, price);
            //Assert
            cartItem.Price.Should().Be(price);
        }

        [Test]
        public void Constructor_ShouldReturnTheRightType()
        {
            //Arrange
            var diamondID = Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID;
            var price = Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT;
            //Act
            var cartItem = new DiamondCartItem(diamondID, price);
            //Assert
            cartItem.Type.Should().Be(CartItemType.Diamond);
        }

    }
}