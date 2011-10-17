using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    [TestFixture]
    public class RingSizePartialViewModelTests
    {

        [Test]
        public void For_ShouldReturnTheRightSelectNameAsNeededByField()
        {
            //Arrange
            var viewModell = new RingSizePartialViewModel();
            //Act
            viewModell.For<MyTestClass>(x => x.TestProperty);
            //Assert
            viewModell.SelectName.Should().Be("TestProperty");
        }

        

        [Test]
        public void For_ShouldParseACompositeSelectNameWithDotsForInternalProperties()
        {
            //Arrange
            var viewModell = new RingSizePartialViewModel();
            //Act
            viewModell.For<MyTestClass>(x => x.InternalType.InternalProperty);
            //Assert
            viewModell.SelectName.Should().Be("InternalType.InternalProperty");

        }

        private class MyTestClass
        {
            public string TestProperty { get; set; }
            public Internal InternalType { get; set; }

            public class Internal
            {
                public string InternalProperty { get; set; } 
            }
        }

    }
}