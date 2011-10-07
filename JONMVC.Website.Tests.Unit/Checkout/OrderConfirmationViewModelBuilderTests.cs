using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class OrderConfirmationViewModelBuilderTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Build_ShouldReturnTheCorrectOrderNumberAndEmail()
        {
            //Arrange
            var orderNumber = Tests.NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING;
            var checkoutDetails = new CheckoutDetailsModel()
                                      {
                                          Email = Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING
                                      };
            var builder = new OrderConfirmationViewModelBuilder(orderNumber,checkoutDetails);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Email.Should().Be(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING);
            viewModel.OrderNumber.Should().Be(orderNumber.ToString());
        }

        //TODO error handling for nulls and stuff
    }
}