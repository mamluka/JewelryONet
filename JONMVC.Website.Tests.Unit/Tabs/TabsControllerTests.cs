using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;
using JONMVC.Website.Controllers;

namespace JONMVC.Website.Tests.Unit.Tabs
{
    [TestFixture]
    public class TabsControllerTests:TabTestsBase
    {
        private Fixture fixture;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            fixture = new Fixture();
        }

        [Test]
        public void PresentTab_ShouldRetunAModelWithTheRightStrongTypeModel()
        {
            //Arrange
            var tabsController = CreateDefaultTabsController();

            var viewModel = MockRepository.GenerateStub<TabsViewModel>();
            viewModel.TabKey = TAB_KEY;
            viewModel.TabId = TAB_ID1;

            //Act
            var resultview = tabsController.SearchTabs(viewModel);
            //Assert

            resultview.AssertViewRendered().WithViewData<TabsViewModel>();

        }

       

        [Test]
        public void SearchTabsPost_ShoulRedirectToTheRightRoute()
        {
            //Arrange
            var tabsController = CreateDefaultTabsController();

            var viewModel = MockRepository.GenerateStub<TabsViewModel>();
            viewModel.TabKey = TAB_KEY;
            viewModel.TabId = TAB_ID1;

            //Act
            var resultview = tabsController.SearchTabsPost(viewModel);
            //Assert

            resultview.AssertActionRedirect().RouteName.Should().Be("Tabs");
        }

        [Test]
        public void SearchTabsPost_ShouldAddCustomFilterToRoute()
        {
            //Arrange
            var tabsController = CreateDefaultTabsController();

            var viewModel = fixture.Build<TabsViewModel>().With(x => x.CustomFilters,
                                                                new List<CustomTabFilterViewModel>()
                                                                    {
                                                                        new CustomTabFilterViewModel()
                                                                            {
                                                                                Name = "test",
                                                                                Value = 2
                                                                            },
                                                                        new CustomTabFilterViewModel()
                                                                            {
                                                                                Name = "test2",
                                                                                Value = 4
                                                                            }
                                                                    }).CreateAnonymous();

            //Act
            var resultview = tabsController.SearchTabsPost(viewModel);
            //Assert


            resultview.AssertActionRedirect().RouteValues.Should().Contain(new KeyValuePair<string, object>("CustomFilters[0].Value", 2));
            resultview.AssertActionRedirect().RouteValues.Should().Contain(new KeyValuePair<string, object>("CustomFilters[1].Value", 4));

            resultview.AssertActionRedirect().RouteValues.Should().Contain(new KeyValuePair<string, object>("CustomFilters[0].Name", "test"));
            resultview.AssertActionRedirect().RouteValues.Should().Contain(new KeyValuePair<string, object>("CustomFilters[1].Name", "test2"));
        }



      



    
        private TabsController CreateDefaultTabsController()
        {
            var tabsRepository = new TabsRepository(fakeXmlSourceFactory);
            var jewelryRepository = new FakeJewelRepository(new FakeSettingManager());

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();
            var tabsController = new TabsController(tabsRepository, jewelryRepository, fileSystem, fakeXmlSourceFactory,pathBarGenerator, mapper);
            return tabsController;
        }

       








    }

 
}