using System;
using System.Text;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Tabs;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class ChooseSettingViewModelBuilderTests:JewelDesignTestsBase
    {

        private string NOT_IMPORTANT_FOR_THIS_TEST = "not important for this test";
        private const string JEWEL_DESIGN_TABID_01 = "solitaire-settings";
        private const string JEWEL_DESIGN_TABKEY = "jewel-design-settings";

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            base.Initialize();
        }


        [Test]
        public void Builder_ShouldReturn3TabsAsInTheStudTabRepository()
        {
            //Arrange
            


            var builder = CreateDefaultChooseSettingViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Tabs.Should().HaveCount(3);

        }
        [Test]
        public void Builder_ShouldReturn3TabsForNavigation()
        {
            //Arrange
            var builder = CreateDefaultChooseSettingViewModelBuilder();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.TabsForJewelDesignNavigation.Should().HaveCount(3);

        }

        [Test]
        public void Builder_ShouldStillReturn3TabsUsingTheDefaultTabKeyWhenNotSpecified()
        {
            //Arrange
            var builder = CreateDefaultChooseSettingViewModelBuilderWithChooseSettingModelAsAParameter(new ChooseSettingViewModel()
                                                                                                           {
                                                                                                               TabId = JEWEL_DESIGN_TABID_01
                                                                                                           });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Tabs.Should().HaveCount(3);

        }

        [Test]
        public void Builder_ShouldStillReturnDiamondIDIfPresentInTheInputModelOrRoute()
        {
            //Arrange
            var builder = CreateDefaultChooseSettingViewModelBuilderWithChooseSettingModelAsAParameter(new ChooseSettingViewModel()
            {
                TabId = JEWEL_DESIGN_TABID_01,
                DiamondID =FIRST_DIAMOND_IN_REP
                
            });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.DiamondID.Should().Be(FIRST_DIAMOND_IN_REP);

        }

        [Test]
        public void Builder_ShouldStillReturnSettingIDAndDiamondIDIfPresentInTheInputModelOrRoute()
        {
            //Arrange
            var builder = CreateDefaultChooseSettingViewModelBuilderWithChooseSettingModelAsAParameter(new ChooseSettingViewModel()
            {
                TabId = JEWEL_DESIGN_TABID_01,
                DiamondID = FIRST_DIAMOND_IN_REP,
                SettingID = SETTING_ID

            });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.SettingID.Should().Be(SETTING_ID);

        }
        //mapping tests after the regular tab builder


        private ChooseSettingViewModelBuilder CreateDefaultChooseSettingViewModelBuilder()
        {
            var chooseSettingViewModel = new ChooseSettingViewModel();
            chooseSettingViewModel.TabId = JEWEL_DESIGN_TABID_01;
            chooseSettingViewModel.TabKey = JEWEL_DESIGN_TABKEY;

            var settingManager = new FakeSettingManager();
            var tabsRepository = TabsViewModelBuilderTests.CreateStubTabsRepository(JEWEL_DESIGN_TABKEY);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fakeTabXmlFactory = new FakeTabXmlFactory();
            var xmldoc_jeweldesign = fakeTabXmlFactory.JewelDesign3Tabs(JEWEL_DESIGN_TABKEY);


            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");


            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(chooseSettingViewModel,
                                                                                 xmldoc_jeweldesign,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(chooseSettingViewModel,
                                                                                    diamondRepository, jewelryRepository,
                                                                                    webHelpers);


            var builder = new ChooseSettingViewModelBuilder(chooseSettingViewModel, tabsViewModelBuilder, tabsForJewelDesignBuilder);
            return builder;
        }

        private ChooseSettingViewModelBuilder CreateDefaultChooseSettingViewModelBuilderWithChooseSettingModelAsAParameter(ChooseSettingViewModel chooseSettingViewModel)
        {

            var settingManager = new FakeSettingManager();
            var tabsRepository = TabsViewModelBuilderTests.CreateStubTabsRepository(JEWEL_DESIGN_TABKEY);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fakeTabXmlFactory = new FakeTabXmlFactory();
            var xmldoc_jeweldesign = fakeTabXmlFactory.JewelDesign3Tabs(JEWEL_DESIGN_TABKEY);


            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");


            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(chooseSettingViewModel,
                                                                                 xmldoc_jeweldesign,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(chooseSettingViewModel,
                                                                                    diamondRepository, jewelryRepository,
                                                                                    webHelpers);


            var builder = new ChooseSettingViewModelBuilder(chooseSettingViewModel, tabsViewModelBuilder, tabsForJewelDesignBuilder);
            return builder;
        }
    }
}