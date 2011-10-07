using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using NUnit.Framework;
using Ninject;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit
{
    [TestFixture]
    public class NinjectTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [TestFixtureSetUp]
        public void TestInitialize()
        {
            
        }
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Kernel_ShouldTestIfICanInjectOnTheFly()
        {
            //Arrange
            var kernel = new StandardKernel();
            //Act
            kernel.Bind<IShoppingCartWrapper>().To<ShoppingCartWrapper>();
            //Assert

          //  var wrapper = kernel.

        }

    }
}