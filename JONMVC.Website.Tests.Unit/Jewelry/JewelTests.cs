using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Items;
using NUnit.Framework;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Jewelry
{
    [TestFixture]
    public class JewelTests:MapperAndFixtureBase
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
            jewel.JewelCategory.Should().Be("category");
            jewel.JewelCategoryID.Should().Be(2);

            jewel.JewelSubCategory.Should().Be("subcategory");
            jewel.JewelSubCategoryID.Should().Be(7);



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

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory2()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 2).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.Ring);

        }

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory3()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 3).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.Earring);

        }

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory4()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 4).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.Necklace);

        }

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory6()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 6).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.Pendant);

        }

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory8()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 8).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.Bracelet);

        }

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory10()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 10).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.SemiMounting);

        }

        [Test]
        public void Constructor_ShouldRenderJewelTypeAsRingForCategory11()
        {
            //Arrange
            var initObj = fixture.Build<ItemInitializerParameterObject>().With(x => x.JewelryCategoryID, 11).CreateAnonymous();
            //Act
            var jewel = new Jewel(initObj, null, null, null, JewelMediaType.WhiteGold);

            //Assert
            jewel.Type.Should().Be(JewelType.Stud);

        }





    }
}