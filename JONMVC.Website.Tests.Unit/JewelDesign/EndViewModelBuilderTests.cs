using System;
using System.Text;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class EndViewModelBuilderTests:JewelDesignTestsBase
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            base.Initialize();
        }

        [Test]
        public void Build_ShouldReturnThe3NavigationTabs()
        {
            //Arrange
            var builder = CreateDefaultEditViewModelBuilder();
            //Act
            var viewModel = builder.Build();

            //Assert
            viewModel.TabsForJewelDesignNavigation.Should().HaveCount(3);

        }

       

        [Test]
        public void Build_ShouldSetTheTotalPriceCorrectly()
        {
            //Arrange
            var builder = CreateDefaultEditViewModelBuilder();
            //Act
            var viewModel = builder.Build();

            //Assert
            viewModel.TotalPrice.Should().Be("$35,478");

        }

        [Test]
        public void Build_ShouldSetTheJewelPersistenceCorrectly()
        {
            //Arrange
            var customJewelInEndPage = new CustomJewelPersistenceInEndPage
                                           {
                                               DiamondID = 1,
                                               SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                               MediaType = JewelMediaType.YellowGold,
                                               Size = Tests.SAMPLE_JEWEL_SIZE_725
                                           };

            var builder = CreateDefaultEditViewModelBuilderWithPresistenceAs(customJewelInEndPage);
            //Act
            var viewModel = builder.Build();

            //Assert
            viewModel.JewelPersistence.DiamondID.Should().Be(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID);
            viewModel.JewelPersistence.SettingID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);
            viewModel.JewelPersistence.Size.Should().Be(Tests.SAMPLE_JEWEL_SIZE_725);
            viewModel.JewelPersistence.MediaType.Should().Be(JewelMediaType.YellowGold);

        }

        [Test]
        public void Build_ShouldSetTheJewelMediaTypeCorrectly()
        {
            //Arrange
            var customJewelInEndPage = new CustomJewelPersistenceInEndPage
            {
                DiamondID = 1,
                SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                MediaType = JewelMediaType.YellowGold,
                Size = Tests.SAMPLE_JEWEL_SIZE_725
            };

            var builder = CreateDefaultEditViewModelBuilderWithPresistenceAs(customJewelInEndPage);
            //Act
            var viewModel = builder.Build();

            //Assert
            viewModel.SettingIcon.Should().Match("*yg*");

        }

        [Test]
        public void Build_ShouldSetTheJewelMediaTypeCorrectlyWithWhiteGold()
        {
            //Arrange
            var customJewelInEndPage = new CustomJewelPersistenceInEndPage
            {
                DiamondID = 1,
                SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                MediaType = JewelMediaType.WhiteGold,
                Size = Tests.SAMPLE_JEWEL_SIZE_725
            };

            var builder = CreateDefaultEditViewModelBuilderWithPresistenceAs(customJewelInEndPage);
            //Act
            var viewModel = builder.Build();

            //Assert
            viewModel.SettingIcon.Should().Match("*wg*");

        }

       



        //TODO add the tests for the mapping to be mapper agnostic

        private EndViewModelBuilder CreateDefaultEditViewModelBuilder()
        {
            var diamondRepository = new FakeDiamondRepository(mapper);

            var jewelryRepository = new FakeJewelRepository(new SettingManager());
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var customJewelInEndPage = new CustomJewelPersistenceInEndPage();

            customJewelInEndPage.DiamondID = 1;
            customJewelInEndPage.SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;
            customJewelInEndPage.MediaType = JewelMediaType.YellowGold;
            customJewelInEndPage.Size = Tests.SAMPLE_JEWEL_SIZE_725;

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewelInEndPage, diamondRepository,
                                                                                    jewelryRepository, webHelpers);


            var builder = new EndViewModelBuilder(customJewelInEndPage, tabsForJewelDesignBuilder, diamondRepository,
                                                   jewelryRepository,mapper);
            return builder;
        }

        private EndViewModelBuilder CreateDefaultEditViewModelBuilderWithPresistenceAs(CustomJewelPersistenceInEndPage customJewelPersistenceInEndPage)
        {
            var diamondRepository = new FakeDiamondRepository(mapper);

            var jewelryRepository = new FakeJewelRepository(new SettingManager());
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();



            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewelPersistenceInEndPage, diamondRepository,
                                                                                    jewelryRepository, webHelpers);


            var builder = new EndViewModelBuilder(customJewelPersistenceInEndPage, tabsForJewelDesignBuilder, diamondRepository,
                                                   jewelryRepository, mapper);
            return builder;
        }

    }
}