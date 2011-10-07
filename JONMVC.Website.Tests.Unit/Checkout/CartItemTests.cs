using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class CartItemTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void PartialName_ShouldReturnTheRightPartialNameInJewelItem()
        {
            //Arrange
            var jewelCartItem = new JewelCartItemViewModel();
            //Act
            var partialName = jewelCartItem.PartialName;
            //Assert
            partialName.Should().Be("JewelCartItem");
        }

        

        [Test]
        public void PartialName_ShouldReturnTheRightPartialNameInDiamondItem()
        {
            //Arrange
            var diamondCartItem = new DiamondCartItemViewModel();
            //Act
            var partialName = diamondCartItem.PartialName;
            //Assert
            partialName.Should().Be("DiamondCartItem");
        }

        [Test]
        public void PartialName_ShouldReturnTheRightPartialNameInCustomJewel()
        {
            //Arrange
            var diamondCartItem = new CustomJewelCartItemViewModel();
            //Act
            var partialName = diamondCartItem.PartialName;
            //Assert
            partialName.Should().Be("CustomJewelCartItem");
        }

       

    }
}