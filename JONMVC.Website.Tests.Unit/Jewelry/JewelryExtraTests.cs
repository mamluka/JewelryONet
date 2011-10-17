using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Jewelry
{
    [TestFixture]
    public class JewelryExtraTests:MapperAndFixtureBase
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Constructor_ShouldReturnRightCSClarity()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.Clarity.Should().Be("IF");

        }

        [Test]
        public void Constructor_ShouldReturnRightCSClarityFreeText()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.ClarityFreeText.Should().Be("CSFreeClarity");

        }

        [Test]
        public void Constructor_ShouldReturnRightCSColor()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.Color.Should().Be("F");

        }

        [Test]
        public void Constructor_ShouldReturnRightCSColorFreeText()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.ColorFreeText.Should().Be("CSFreeColor");

        }

        [Test]
        public void Constructor_ShouldReturnRightCSCount()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.Count.Should().Be(1);

        }

        [Test]
        public void Constructor_ShouldReturnRightCSCut()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.Cut.Should().Be("Round");

        }

        [Test]
        public void Constructor_ShouldReturnRightCSDescription()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.Description.Should().Be("CSDescription");

        }

        [Test]
        public void Constructor_ShouldReturnRightCSWeight()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.CS.Weight.Should().BeInRange(2, 2.1);

        }

        [Test]
        public void Constructor_ShouldReturnRightSSClarity()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.Clarity.Should().Be("VVS1");

        }

        [Test]
        public void Constructor_ShouldReturnRightSSClarityFreeText()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.ClarityFreeText.Should().Be("SSFreeClarity");

        }

        [Test]
        public void Constructor_ShouldReturnRightSSColor()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.Color.Should().Be("H");

        }

        [Test]
        public void Constructor_ShouldReturnRightSSColorFreeText()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.ColorFreeText.Should().Be("SSFreeColor");

        }

        [Test]
        public void Constructor_ShouldReturnRightSSCount()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.Count.Should().Be(8);

        }

        [Test]
        public void Constructor_ShouldReturnRightSSCut()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.Cut.Should().Be("Pricess");

        }

        [Test]
        public void Constructor_ShouldReturnRightSSDescription()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.SS.Description.Should().Be("SSDescription");

        }

        [Test]
        public void Constructor_ShouldReturnRightSSWeight()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);
            //Assert

            jewelExtra.SS.Weight.Should().BeInRange(1.5, 1.6);

        }

        [Test]
        public void Constructor_ShouldReturnRightHasSideStones()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.HasSideStones.Should().BeTrue();

        }

        [Test]
        public void Constructor_ShouldReturnRightTotalWeight()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.CreateAnonymous<ItemInitializerParameterObject>();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);

            //Assert

            jewelExtra.TotalWeight.Should().BeInRange(3.5, 3.6);

        }

        [Test]
        public void Constructor_ShouldReturnCSColorAndClarityAsFreeTextWhenTheJewelIsGemstone()
        {
            //Arrange
            var initJewelExtra = JewelryExtraInitializerParameterFactory();
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x=> x.CategoryID,8).CreateAnonymous();
            //Act

            var jewelExtra = new JewelryExtra(initJewelExtra, initObj);
            //Assert

            jewelExtra.CS.Clarity.Should().Be(initJewelExtra.CS_ClarityFreeText);
            jewelExtra.CS.Color.Should().Be(initJewelExtra.CS_ColorFreeText);

        }








        private JewelryExtraInitializerParameterObject JewelryExtraInitializerParameterFactory()
        {

            return FakeItemInitializerFactory.JewelryExtraInitializerParameterObject;
        }
    }
}