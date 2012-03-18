using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace JONMVC.Website.Tests.Unit.Items
{
    [TestFixture]
    public class MetalTests
    {

        private const string MetalFromDb = "";
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {

        }

        [Test]
        public void GetShortCode_ShouldReturnTheRightShortCodeForPlatinum()
        {
            //Arrange

            const string metalName1 = "Platinum";


            Metal metal = new Metal(metalName1); 

            //Act
            string shortcode = metal.GetShortCode();
            //Assert
            
            Assert.That(shortcode,Is.EqualTo("plt"));

        }

        [Test]
        public void GetShortCode_ShouldReturnTheRightShortCodeForWhiteGold()
        {
            //Arrange

            const string metalName3 = "White Gold 18 Karat";

            Metal metal = new Metal(metalName3);

            //Act
            string shortcode = metal.GetShortCode();
            //Assert

            Assert.That(shortcode, Is.EqualTo("wg"));

        }


        [Test]
        public void GetShortCode_ShouldReturnTheRightShortCodeForYellowGold()
        {
            //Arrange

            const string metalName2 = "Yellow Gold 18 Karat";

            Metal metal = new Metal(metalName2);

            //Act
            string shortcode = metal.GetShortCode();
            //Assert

            Assert.That(shortcode, Is.EqualTo("yg"));

        }

        [Test]
        public void GetFullName_ShouldReturnTheCorrectFullName()
        {
            const string metalName1 = "Platinum";


            Metal metal = new Metal(metalName1);

            //Act
            string shortcode = metal.GetFullName();
            //Assert

            Assert.That(shortcode, Is.EqualTo(metalName1));

        }

        [Test]
        public void GetFullName_ShouldReturnWhiteGoldWhenAllIsPresent()
        {
            //Arrange
            var metal = new Metal(JewelMediaType.All, JewelMediaType.All, MetalFromDb);
            //Act
            var metalFullName = metal.GetFullName();
            //Assert
            metalFullName.Should().Be("White Gold 18 Karat");
           
        }

        [Test]
        public void GetFullName_ShouldReturnWhiteGoldWhenWhiteGoldIRequestedAndPresent()
        {
            //Arrange
            var metal = new Metal(JewelMediaType.WhiteGold, JewelMediaType.WhiteGold, MetalFromDb);
            //Act
            var metalFullName = metal.GetFullName();
            //Assert
            metalFullName.Should().Be("White Gold 18 Karat");

        }

        [Test]
        public void GetFullName_ShouldReturnWhiteGoldWhenAllIsRequestedAndWhiteGoldPresent()
        {
            //Arrange
            var metal = new Metal(JewelMediaType.All, JewelMediaType.WhiteGold, MetalFromDb);
            //Act
            var metalFullName = metal.GetFullName();
            //Assert
            metalFullName.Should().Be("White Gold 18 Karat");

        }

        [Test]
        public void GetFullName_ShouldReturnYellowGoldWhenYellowGoldIRequestedAndPresent()
        {
            //Arrange
            var metal = new Metal(JewelMediaType.YellowGold, JewelMediaType.YellowGold, MetalFromDb);
            //Act
            var metalFullName = metal.GetFullName();
            //Assert
            metalFullName.Should().Be("Yellow Gold 18 Karat");

        }

        [Test]
        public void GetFullName_ShouldReturnYellowGoldWhenYellowGoldIsRequestedAndAlldPresent()
        {
            //Arrange
            var metal = new Metal(JewelMediaType.YellowGold, JewelMediaType.All, MetalFromDb);
            //Act
            var metalFullName = metal.GetFullName();
            //Assert
            metalFullName.Should().Be("Yellow Gold 18 Karat");

        }



       
    }
}