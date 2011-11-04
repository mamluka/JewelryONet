using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Xml.Linq;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;
using NMoneys;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using JONMVC.Website.Models.Helpers;
using System.Linq;

namespace JONMVC.Website.Tests.Unit.Tabs
{

    [TestFixture]
    public class TabsViewModelBuilderTests:TabTestsBase
    {
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
          
        }

        [Test]
        public void PageTitle_ShouldReturnTheCorrentPageTitle()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();
            //Act
            
            var title = viewModel.Title;
            //Assert

            title.Should().Be("Diamond Jewelry");


        }

        [Test]
        public void PageTitle_ShouldReturnTheCorrentShortPageTitle()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var shortTitle = viewModel.ShortTitle;
            //Assert

            shortTitle.Should().Be("Short Diamond Jewelry");


        }

        

        [Test]
        public void Sprite_ShouldReturnTheRightSprite()
        {
            //Arrange

            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();
            //Act
            var sprite = viewModel.Sprite;
            //Assert

            sprite.Should().Be("diamond-jewelry");
        }



        [Test]
        public void Tabs_ShouldCallTheTabRepositoryWithTheCorrectKey()
        {
            //Arrange
            var tabsRepository = MockRepository.GenerateStrictMock<ITabsRepository>();
            var jewelryRepository = MockRepository.GenerateStub<IJewelRepository>();
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
            var faketablist = new List<Tab>();

            faketablist.Add(new Tab("test1", TAB_ID1, 1));
            faketablist.Add(new Tab("test2", TAB_ID2, 2));
            faketablist.Add(new Tab("test3", TAB_ID3, 3));

            tabsRepository.Expect(x => x.GetTabsCollectionByKey(Arg<String>.Is.Equal(TAB_KEY))).Repeat.Once().Return(faketablist);



            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs, tabsRepository, jewelryRepository,fileSystem);
            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var tabs = viewModel.Tabs;

            //Assert
            tabsRepository.VerifyAllExpectations();

        }

        [Test]
        public void Tabs_ShouldCallTheJewelryRepository()
        {
            //Arrange
            var jewelryRepository = MockRepository.GenerateMock<IJewelRepository>();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
     

            jewelryRepository.Expect(
                x =>
                x.GetJewelsByDynamicSQL(Arg<DynamicSQLWhereObject>.Is.Anything) ).Repeat.Once();

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs, tabsRepository, jewelryRepository,fileSystem);
            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var tabs = viewModel.JewelryInTabContainersCollection;

            //Assert
            jewelryRepository.VerifyAllExpectations();

        }

        [Test]
        public void Tabs_ShouldActivateTheRightTab()
        {
            //Arrange

            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = MockRepository.GenerateStub<IJewelRepository>();
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID2, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var tabs = viewModel.Tabs;

            //Assert
            tabs[0].Active.Should().BeFalse();
            tabs[1].Active.Should().BeTrue();
            tabs[2].Active.Should().BeFalse();
        }

        [Test]
        public void Tabs_ShouldSetViewModelToNotShowTabsWhenTheFeatureIsEnabledInsideTheXML()
        {
            //Arrange

            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = MockRepository.GenerateStub<IJewelRepository>();
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID2, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var tabs = viewModel.Tabs;

            //Assert
            tabs[0].Active.Should().BeFalse();
            tabs[1].Active.Should().BeTrue();
            tabs[2].Active.Should().BeFalse();
        }

        [Test]
        public void Tabs_ShouldShowTheTabsIfRegularTabsArePresented()
        {
            //Arrange

            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = MockRepository.GenerateStub<IJewelRepository>();
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var viewModel = tabsViewModelBuilder.Build();
            //Act

            viewModel.IsShowTabs.Should().BeTrue();


        }

        [Test]
        public void Tabs_ShouldLoadTheTabExtraText()
        {
            //Arrange

            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = MockRepository.GenerateStub<IJewelRepository>();
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var viewModel = tabsViewModelBuilder.Build();
            //Act

            viewModel.ExtraText.Should().Be("extratext");


        }
       

        [Test]
        public void PageAndItemsPerPage_ShouldCallItemsPerPageMethodOnTheRepository()
        {
            //Arrange
            IJewelRepository jewelRepository = MockRepository.GenerateMock<IJewelRepository>();

            var tabsRepository = CreateStubTabsRepository(TabKey);

            jewelRepository.Expect(
                x =>
                x.ItemsPerPage(Arg<int>.Is.Equal(21))).Repeat.Once();

            jewelRepository.Expect(
                x =>
                x.Page(Arg<int>.Is.Equal(1))).Repeat.Once();

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs, tabsRepository, jewelRepository,fileSystem);
            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var tabs = viewModel.JewelryInTabContainersCollection;

            //Assert
            jewelRepository.VerifyAllExpectations();

        }

        [Test]
        public void OrderBy_ShouldOrderByLowToHighWhenDefaultOrderingIsUsed()
        {
            //Arrange
            var jewelryRepository = MockRepository.GenerateMock<IJewelRepository>();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15001");
           

            var orderBy = new DynamicOrderBy("price","asc");

            jewelryRepository.Expect(x => x.OrderJewelryItemsBy(Arg<DynamicOrderBy>.Is.Equal(orderBy))).Repeat.Once();

            var viewModel = new TabsViewModel();
            viewModel.TabKey = TAB_KEY;
            viewModel.TabId = TAB_ID1;
            viewModel.MetalFilter = JewelMediaType.WhiteGold;
            

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(viewModel, xmldoc_regular3tabs, tabsRepository, jewelryRepository,fileSystem);
            viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabs = viewModel.JewelryInTabContainersCollection;

            //Assert
            jewelryRepository.VerifyAllExpectations();

        }

        [Test]
        [Ignore]
        public void Build_ShouldAddACustomFilterToTheViewModelIFPresentInTabs()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilderWithCustomFilterForGemstones();
            var viewModel = tabsViewModelBuilder.Build();
            //Act
            
            //Assert

        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheCorrentDescription()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();
            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.Description.Should().Be("title");


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheCorrentPrice()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();

            var price = Tests.AsMoney((decimal) 9999.99);
            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.Price.Should().Be(price);


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheRegularPriceEqualToPriceWhenTheItemIsStandard()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.RegularPrice.Should().Be(tabContainer1.Price);


        }

      
        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTrueOnSpecialIfTheItemIsOnSpecial()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilderWithAllMetalFilter();
            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection.Where(x => x.ID == Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER).SingleOrDefault();
            //Assert
            
            tabContainer1.OnSpecial.Should().BeTrue();


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnPriceEqualToSpecialPriceWhenOnSpecial()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilderWithAllMetalFilter();
            var viewModel = tabsViewModelBuilder.Build();

            var specialPrice = Tests.AsMoney(8000);
            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection.Where(x => x.ID == Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER).SingleOrDefault();
            //Assert
            
            tabContainer1.Price.Should().Be(specialPrice);


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnRegularPriceEqualToTheItemsRegularPriceWhenOnSpecial()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilderWithAllMetalFilter();
            var viewModel = tabsViewModelBuilder.Build();

            var price = Tests.AsMoney((decimal) 9999.99);
            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection.Where(x => x.ID == Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER).SingleOrDefault();
            //Assert
            
            tabContainer1.RegularPrice.Should().Be(price);


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldCalculateYouSaveRatio()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilderWithAllMetalFilter();
            var viewModel = tabsViewModelBuilder.Build();

            var precent = String.Format("{0:0.##}%", Math.Round(100 - (8000 / 9999.99) * 100));
            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection.Where(x=> x.ID == Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER).SingleOrDefault();
            //Assert
            
            tabContainer1.YouSave.Should().Be(precent);


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheCorrentPictureURL()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.PictureURL.Should().Contain("0101-15421-icon-wg.jpg");


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheCorrentID()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheCorrentMovieWhenAMovieIsPresent()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.Movie.Should().Contain("0101-15421-mov-wg.flv");


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnHasMovieToBeTrueWhenMovieFilePresent()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.HasMovie.Should().BeTrue();

        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnHasMovieToBeFalseWhenMovieFileIsNotPresent()
        {
            //Arrange
            var settingManager = new FakeSettingManager();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-pic-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-icon-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hires-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hand-wg.jpg",new MockFileData("")},
//                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-mov-wg.flv",new MockFileData("")}
                                                    });

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);

            var viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabContainer1 = viewModel.JewelryInTabContainersCollection[0];
            //Assert

            tabContainer1.HasMovie.Should().BeFalse();

        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnTheCorrectItemPartialViewNameWhenTabsAreRegular()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilder();
            //Act

          var viewModel = tabsViewModelBuilder.Build();
            //Assert

          viewModel.InTabPartialView.Should().Be("RegularInTabView");


        }

        [Test]
        public void MapJewelsToInTabContainers_ShouldReturnACustomPartialViewWhenTabsAreSpecial()
        {
            //Arrange
            var tabsViewModelBuilder = CreateDefaultTabsViewModelBuilderWithSpecialTabs();
            //Act

            var viewModel = tabsViewModelBuilder.Build();
            //Assert

            viewModel.InTabPartialView.Should().Be("CustomInTabContainer");


        }


        
        //TODO Add some error handling

        [Test]
        public void Build_IfGivenAViewModelWithNoMetalFilterSetToDefaultOf1()
        {
            //Arrange
            var jewelryRepository = MockRepository.GenerateMock<IJewelRepository>();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15001");

            var viewModel = new TabsViewModel();
            viewModel.TabKey = TAB_KEY;
            viewModel.TabId = TAB_ID1;

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(viewModel, xmldoc_regular3tabs, tabsRepository, jewelryRepository, fileSystem);
            viewModel = tabsViewModelBuilder.Build();

            //Act

            var tabsContainers = viewModel.JewelryInTabContainersCollection;

            //Assert
            tabsContainers.Should().OnlyContain(x => x.Metal == "White Gold 18K");

        }

        [Test]
        public void Build_IfGivenATabKeyWithSpecialTebsReturnZeroTabs()
        {
            //Arrange
            var jewelryRepository = MockRepository.GenerateMock<IJewelRepository>();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15001");

            var viewModel = new TabsViewModel();
            viewModel.TabKey = TAB_KEY;
            viewModel.TabId = SPECIAL_TABID1;

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(viewModel, xmldoc_specialtab, tabsRepository, jewelryRepository, fileSystem);
            viewModel = tabsViewModelBuilder.Build();

            //Act

            viewModel.Tabs.Should().HaveCount(0);

            //Assert


        }

        [Test]
        public void Build_IfGivenATabKeyWithSpecialTebsReturnIsShowTabsToFalse()
        {
            //Arrange
            var jewelryRepository = MockRepository.GenerateMock<IJewelRepository>();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15001");

            var viewModel = new TabsViewModel();
            viewModel.TabKey = TAB_KEY;
            viewModel.TabId = SPECIAL_TABID1;

            var  tabsViewModelBuilder = new TabsViewModelBuilder(viewModel, xmldoc_specialtab, tabsRepository, jewelryRepository, fileSystem);
            viewModel = tabsViewModelBuilder.Build();

            //Act

            viewModel.IsShowTabs.Should().BeFalse();
            //Assert

        }

        #region Helpers

        public static ITabsRepository CreateStubTabsRepository(string tabKey)
        {
            var tabsRepository = MockRepository.GenerateStub<ITabsRepository>();
            var faketablist = new List<Tab>();

            faketablist.Add(new Tab("test1", TAB_ID1, 1));
            faketablist.Add(new Tab("test2", TAB_ID2, 2));
            faketablist.Add(new Tab("test3", TAB_ID3, 3));

            tabsRepository.Stub(x => x.GetTabsCollectionByKey(Arg<String>.Is.Equal(tabKey))).Repeat.Once().Return(faketablist);

            return tabsRepository;
        }

        private TabsViewModelBuilder CreateDefaultTabsViewModelBuilder()
        {
            var settingManager = new FakeSettingManager();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);
            return tabsViewModelBuilder;
        }

        private TabsViewModelBuilder CreateDefaultTabsViewModelBuilderWithCustomFilterForGemstones()
        {
            var settingManager = new FakeSettingManager();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, TAB_ID1, xmldoc_tabswithgemstonefilter,
                                                                                 tabsRepository, jewelryRepository, fileSystem);
            return tabsViewModelBuilder;
        }

        private TabsViewModelBuilder CreateDefaultTabsViewModelBuilderWithAllMetalFilter()
        {
            var settingManager = new FakeSettingManager();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");

            var model = new TabsViewModel()
                            {
                                MetalFilter = JewelMediaType.All,
                                TabKey = TabKey,
                                TabId = TabID1
                                
                            };

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(model, xmldoc_regular3tabs,
                                                                                 tabsRepository, jewelryRepository, fileSystem);
            return tabsViewModelBuilder;
        }

        private TabsViewModelBuilder CreateDefaultTabsViewModelBuilderWithSpecialTabs()
        {
            var settingManager = new FakeSettingManager();
            var tabsRepository = CreateStubTabsRepository(TabKey);
            var jewelryRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");

            TabsViewModelBuilder tabsViewModelBuilder = new TabsViewModelBuilder(TAB_KEY, SPECIAL_TABID1, xmldoc_specialtab,
                                                                                 tabsRepository, jewelryRepository, fileSystem);
            return tabsViewModelBuilder;
        }

        #endregion
    }
}