using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Items;
using NUnit.Framework;
using FluentAssertions;

namespace JONMVC.Website.Tests.Unit.Jewelry
{
    [TestFixture]
    public class JewelTests
    {
        private ItemInitializerParameterObject itemInitializerParameterObject;


        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            itemInitializerParameterObject = FakeItemInitializerFactory.ItemInitializerParameterObject;
        }

        /// <summary>
        /// template behavior and state testing method
        /// </summary>
        [Test]
        public void Constructor_ShouldMapTheItemNumberCorrectly()
        {
            //Arrange
            

            //Act
            var jewel = new Jewel(itemInitializerParameterObject,null,null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.ItemNumber.Should().Be("0101-15421");

        }

        [Test]
        public void Constructor_ShouldMapTheIDCorrectly()
        {
            //Arrange
              

            //Act
            var jewel = new Jewel(itemInitializerParameterObject, null, null, null,JewelMediaType.WhiteGold);

            //Assert
            jewel.ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

        }

       
        [Test]
        public void Constructor_ShouldMapTheCategoriesCorrectly()
        {
            //Arrange

            //Act
            var jewel = new Jewel(itemInitializerParameterObject, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Category.Should().Be("category");
            jewel.CategoryID.Should().Be(2);

            jewel.SubCategory.Should().Be("subcategory");
            jewel.SubCategoryID.Should().Be(7);



        }

        [Test]
        public void Constructor_ShouldMapPriceCorrectly()
        {
            //Arrange

            //Act
            var jewel = new Jewel(itemInitializerParameterObject, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Price.Should().Be((decimal) 9999.99);

        }


        [Test]
        public void Constructor_ShouldMapTheTitleCorrectly()
        {
            //Arrange

            //Act
            var jewel = new Jewel(itemInitializerParameterObject, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Title.Should().Be("title");

        }

        [Test]
        public void Constructor_ShouldMapTheWeightCorrectly()
        {
            //Arrange

            //Act
            var jewel = new Jewel(itemInitializerParameterObject, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Weight.Should().BeInRange(10.50, 10.51);

        }

        [Test]
        public void GetMedia_ShouldReturnAMediaObject()
        {
            //Arrange
            var manager = new SettingManager();
            var mediaFactory = new MediaFactory(itemInitializerParameterObject.ItemNumber,manager);

            var media = mediaFactory.BuildMedia();
            var metal = new Metal(itemInitializerParameterObject.Metal);
            //Act
            var jewel = new Jewel(itemInitializerParameterObject, media, null, null, JewelMediaType.WhiteGold);

            //Assert

            jewel.Media.Should().NotBeNull();



        }




    }
}