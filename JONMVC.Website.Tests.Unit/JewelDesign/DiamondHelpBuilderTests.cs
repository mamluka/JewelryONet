using System;
using System.Text;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class DiamondHelpBuilderTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Build_ShouldReturnThreeHelpItems()
        {
            //Arrange
            var diamond = new Diamond()
                              {
                                  Color = "H",
                                  Clarity = "VVS1",
                                  Cut = "VG"
                              };

            var xmlSourceFactory = new FakeXmlSourceFactory();

            var builder = new DiamondHelpBuilder(xmlSourceFactory);
            //Act
            var list = builder.Build(diamond);
            //Assert
            list.Should().HaveCount(3);
        }

        [Test]
        public void Build_ReturnTheRightCurrectValueForColorAsAnExmpleForAllOtherAsItsTheSameAlgorithm()
        {
            //Arrange
            var diamond = new Diamond()
            {
                Color = "H",
                Clarity = "VVS1",
                Cut = "VG"
            };

            var xmlSourceFactory = new FakeXmlSourceFactory();

            var builder = new DiamondHelpBuilder(xmlSourceFactory);
            //Act
            var list = builder.Build(diamond);
            //Assert
            list["color"].BodyText.Should().Be("help for H");
        }

        [Test]
        public void Build_ReturnTheRightBodyForColorAsAnExmpleForAllOtherAsItsTheSameAlgorithm()
        {
            //Arrange
            var diamond = new Diamond()
            {
                Color = "H",
                Clarity = "VVS1",
                Cut = "VG"
            };

            var xmlSourceFactory = new FakeXmlSourceFactory();

            var builder = new DiamondHelpBuilder(xmlSourceFactory);
            //Act
            var list = builder.Build(diamond);
            //Assert
            list["color"].CurrentValueOfHelp.Should().Be("H");
        }

        [Test]
        public void Build_ReturnTheRightTitleForColorAsAnExmpleForAllOtherAsItsTheSameAlgorithm()
        {
            //Arrange
            var diamond = new Diamond()
            {
                Color = "H",
                Clarity = "VVS1",
                Cut = "VG"
            };

            var xmlSourceFactory = new FakeXmlSourceFactory();

            var builder = new DiamondHelpBuilder(xmlSourceFactory);
            //Act
            var list = builder.Build(diamond);
            //Assert
            list["color"].Title.Should().Be("Color");
        }

        [Test]
        public void Build_ReturnNAForTheBodyTextWhenTheXmlDoesntContainTheDefinition()
        {
            //Arrange
            var diamond = new Diamond()
                              {
                                  Color = "P",
                                  Clarity = "VVS1",
                                  Cut = "VG"
                              };

            var xmlSourceFactory = new FakeXmlSourceFactory();

            var builder = new DiamondHelpBuilder(xmlSourceFactory);
            //Act
            var list = builder.Build(diamond);
            //Assert
            list["color"].BodyText.Should().Be("N/A");

        }


        [Test]
        public void Build_ReturnTheAllTheValuesForColorAsAnExmpleForAllOtherAsItsTheSameAlgorithm()
        {
            //Arrange
            var diamond = new Diamond()
            {
                Color = "H",
                Clarity = "VVS1",
                Cut = "VG"
            };

            var xmlSourceFactory = new FakeXmlSourceFactory();

            var builder = new DiamondHelpBuilder(xmlSourceFactory);
            //Act
            var list = builder.Build(diamond);
            //Assert
            list["color"].HelpValues.Should().Contain("E");
            list["color"].HelpValues.Should().Contain("F");
            list["color"].HelpValues.Should().Contain("G");
            list["color"].HelpValues.Should().Contain("H");
        }




    }
}