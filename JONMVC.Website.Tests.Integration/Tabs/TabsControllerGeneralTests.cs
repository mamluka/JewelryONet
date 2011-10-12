using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Tabs;

namespace JONMVC.Website.Tests.Integration.Tabs
{
    [TestFixture]
    public class TabsControllerGeneralTests
    {
        private ISettingManager settingManager;
        private const string connectionString = "Data Source=(local);Initial Catalog=JONet;Persist Security Info=True;User ID=jon;Password=0953acb";
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            // string connectionString = "server=localhost;user=dbuser;password=dbpassword;initial catalog=MyDatabase;";
            NDbUnit.Core.INDbUnitTest mySqlDatabase = new NDbUnit.Core.SqlClient.SqlDbUnitTest(connectionString);

            mySqlDatabase.ReadXmlSchema(@"DBFixtures/JewelryItems.xsd");
            mySqlDatabase.ReadXml(@"DBFixtures/JewelryItemsAllCategories.xml");

            mySqlDatabase.PerformDbOperation(NDbUnit.Core.DbOperationFlag.CleanInsertIdentity);

            settingManager = MockRepository.GenerateStub<ISettingManager>();
        }

        [Test]
        public void PresentTabs_ShouldPresentTheDefaultNumberOfItems()
        {
            //Arrange

            var tabKey = "diamond-rings";
            var tabId = "engagement-rings";


            var xmlSource = new XmlSourceFactory();
            var tabsRepository = new TabsRepository(xmlSource);
            var jewelryRepository = new JewelRepository(new SettingManager());
            var fileSystem = new FileSystem();
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();
            var mapper = MockRepository.GenerateStub<IMappingEngine>();

            TabsController controller = new TabsController(tabsRepository,jewelryRepository,fileSystem,xmlSource,pathBarGenerator, mapper);

            var viewModel = MockRepository.GenerateStub<TabsViewModel>();
            viewModel.TabKey = tabKey;
            viewModel.TabId = tabId;

            //Act

            var resultview = controller.SearchTabs(viewModel);

            //Assert

            var model = resultview.Model as TabsViewModel;

            model.JewelryInTabContainersCollection.Count.ShouldBe(10);



        }

        //TODO Add a test for tabid missing and tabkey missing, check the default



        [Test]
        public void PresentTabs_ShouldReturntheRightNumberOfItemsAfterMetalWhiteGoldFilter()
        {
            //Arrange

            var tabKey = "diamond-rings";
            var tabId = "engagement-rings";

            var xmlSource = new XmlSourceFactory();
            var tabsRepository = new TabsRepository(xmlSource);
            var jewelryRepository = new JewelRepository(new SettingManager());
            var fileSystem = new FileSystem();
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var mapper = MockRepository.GenerateStub<IMappingEngine>();

            TabsController controller = new TabsController(tabsRepository, jewelryRepository, fileSystem, xmlSource,pathBarGenerator, mapper);

            var viewModel = new TabsViewModel();
            viewModel.TabKey = tabKey;
            viewModel.TabId = tabId;
            viewModel.MetalFilter = JewelMediaType.WhiteGold;

            //Act

            var resultview = controller.SearchTabs(viewModel);

            //Assert
            var model = resultview.Model as TabsViewModel;
            

            model.JewelryInTabContainersCollection.Should().HaveCount(9).And.OnlyContain(x=> x.PictureURL.Contains("wg"));



        }

    





    }
}