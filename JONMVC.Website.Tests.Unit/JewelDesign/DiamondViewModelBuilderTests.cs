using System.Text;
using AutoMapper;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]

    public class DiamondViewModelBuilderTests:JewelDesignTestsBase
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        /// 
        /// 
        [TestFixtureSetUp]
        public void InitializeFixture()
        {
            base.InitializeFixture();
        }

        [Test]
        public void Build_ShouldReturnTheRightDescriptionForTheDiamondBasedOnTheFields()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Description.Should().Be("A 1.25 Ct. Round H/VS1 Diamond");

        }



        [Test]
        public void Build_ShouldReturnTheRightClarityForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Clarity.Should().Be("VS1");

        }

        [Test]
        public void Build_ShouldReturnTheRightColorForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Color.Should().Be("H");

        }

        [Test]
        public void Build_ShouldReturnTheDepthFormattedForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Depth.Should().Be("58.20%");

        }


        [Test]
        public void Build_ShouldReturnTheHiResPictureFromAPreSetPicturesBasedOnTheShapeForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.DiamondHiResPicture.Should().Be("/jon-images/diamond/round-hires.png");

        }

        [Test]
        public void Build_ShouldReturnTheDiamondIDForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.DiamondID.Should().Be(FIRST_DIAMOND_IN_REP.ToString());

        }

        [Test]
        public void Build_ShouldReturnTheDimensionsForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Dimensions.Should().Be("4.25x4.87x5.36");

        }

        [Test]
        public void Build_ShouldReturnTheItemCodeForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.ItemCode.Should().Be(FIRST_DIAMOND_IN_REP.ToString());

        }

        [Test]
        public void Build_ShouldReturnThePictureFromAPreSetPicturesBasedOnTheShapeForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.MainDiamondPicture.Thumb.Should().Be("/jon-images/diamond/round.png");
            viewModel.MainDiamondPicture.LargePhoto.Should().Be("/jon-images/diamond/round-hires.png");

        }


        [Test]
        public void Build_ShouldReturnTheSymmetryForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Symmetry.Should().Be("VG");

        }

        [Test]
        public void Build_ShouldReturnThePriceForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Price.Should().Be("$25,478");

        }

        [Test]
        public void Build_ShouldReturnThePolishForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Polish.Should().Be("VG");

        }

        [Test]
        public void Build_ShouldReturnTheGradeForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Cut.Should().Be("VG");

        }

        [Test]
        public void Build_ShouldReturnTheReportForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Report.Should().Be("GIA");

        }

        [Test]
        public void Build_ShouldReturnTheShapeForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Shape.Should().Be("Round");

        }


        [Test]
        public void Build_ShouldReturnTheTableForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Table.Should().Be("25.80%");

        }

        [Test]
        public void Build_ShouldReturnTheWeightForTheDiamond()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Weight.Should().Be("1.25 Ct.");

        }

        [Test]
        public void Build_ShouldReturnThePageTitleConstructedFromDiamondProperties()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            string pageTitle = viewModel.Description + " - " +  viewModel.Price;
            viewModel.PageTitle.Should().Be(pageTitle);

        }

        [Test]
        public void Build_ShouldReturnThreeItemsInTheDiamondHelpDic()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.DiamondHelp.Should().HaveCount(3);

        }



        [Test]
        public void Build_ShouldReturnCorrectDiamondIDInPersistenceObject()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JewelPersistence.DiamondID = FIRST_DIAMOND_IN_REP;

        }

        [Test]
        public void Build_ShouldReturnCorrectSettingIDInPersistenceObject()
        {
            //Arrange
            var builder = CreateDefaultDiamondViewModelBuilderWithCustomJewelPersistenceAsAParameter(new CustomJewelPersistenceInDiamond()
                                                                                                         {
                                                                                                             DiamondID = FIRST_DIAMOND_IN_REP,
                                                                                                             SettingID = SETTING_ID
           

                                                                                                         });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JewelPersistence.SettingID = SETTING_ID;

        }
        private DiamondViewModelBuilder CreateDefaultDiamondViewModelBuilder()
        {
            var diamondRepository = new FakeDiamondRepository(mapper);
            var diamondHelpBuilder = new DiamondHelpBuilder(new FakeXmlSourceFactory());
            var customJewel = new CustomJewelPersistenceInDiamond();
            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository,
                                                                          jewelryRepository, webHelpers);
            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;

            var builder = new DiamondViewModelBuilder(customJewel, tabsForJewelDesignBuilder, diamondRepository, diamondHelpBuilder, mapper);
            return builder;
        }

        private DiamondViewModelBuilder CreateDefaultDiamondViewModelBuilderWithCustomJewelPersistenceAsAParameter(CustomJewelPersistenceInDiamond customJewel)
        {
            var diamondRepository = new FakeDiamondRepository(mapper);
            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());
            var diamondHelpBuilder = new DiamondHelpBuilder(new FakeXmlSourceFactory());
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository,
                                                                          jewelryRepository, webHelpers);

            var builder = new DiamondViewModelBuilder(customJewel, tabsForJewelDesignBuilder, diamondRepository, diamondHelpBuilder, mapper);
            return builder;
        }


    }
}