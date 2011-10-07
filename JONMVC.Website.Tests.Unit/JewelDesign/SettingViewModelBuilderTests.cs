using System;
using System.Text;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.JewelryItem;
using JONMVC.Website.Tests.Unit.Tabs;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class SettingViewModelBuilderTests:JewelDesignTestsBase
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
        public void Build_ShouldReturnThe3JewelDesignNavigationTabs()
        {
            //Arrange
            var builder = CreateDefaultSettingViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.TabsForJewelDesignNavigation.Should().HaveCount(3);

        }

      

        [Test]
        public void Build_ShouldReturnTheDiamondIDAsPassedByTheRoute()
        {
            //Arrange
            var builder = CreateDefaultSettingViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JewelPersistence.DiamondID.Should().Be(FIRST_DIAMOND_IN_REP);

        }

        [Test]
        public void Build_ShouldReturnTheSettingIDAsPassedByTheRoute()
        {
            //Arrange
            var builder = CreateDefaultSettingViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JewelPersistence.SettingID.Should().Be(SETTING_ID);

        }

        [Test]
        public void Build_ShouldHideTheCenterStoneInformation()
        {
            //Arrange
            var builder = CreateDefaultSettingViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.SpecsPool.Where(x => x.JewelComponentID == 1).ToList().Should().HaveCount(0);

        }

        //TODO add all the tests that duplicate the JewelItems tests to be mapping agnostic

        private SettingViewModelBuilder CreateDefaultSettingViewModelBuilder()
        {
            var settingManager = new FakeSettingManager();
            var jewelryRepository = new FakeJewelRepository(settingManager);
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var customJewelForSetting = new CustomJewelPersistenceForSetting()
                                            {
                                                DiamondID = FIRST_DIAMOND_IN_REP,
                                                SettingID = SETTING_ID
                                            };

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewelForSetting,
                                                                                    diamondRepository, jewelryRepository,
                                                                                    webHelpers);

            var fakeRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");

            var fakeTestimonialRepository = new FakeTestimonialRepository(mapper);

            var jewelryItemViewModelBuilder = new JewelryItemViewModelBuilder(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                                              fakeRepository, fakeTestimonialRepository,
                                                                              fileSystem, mapper);

            var builder = new SettingViewModelBuilder(customJewelForSetting, tabsForJewelDesignBuilder,
                                                      jewelryItemViewModelBuilder);
            return builder;
        }
    }
}