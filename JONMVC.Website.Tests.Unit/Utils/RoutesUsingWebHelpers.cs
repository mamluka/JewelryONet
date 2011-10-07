using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Utils
{
    [TestFixture]
    public class RoutesUsingWebHelpers
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void DiamondRoute_ShouldReturnTheRightRouteWhenDiamondIDIsGiven()
        {
            //Arrange
            var webHelpers = new WebHelpers();
            //Act

            //Assert

        }

    }
}