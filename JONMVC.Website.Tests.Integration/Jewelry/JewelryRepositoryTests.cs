using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using JONMVC.Website.Models.Utils;
using NDbUnit.Core.SqlClient;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Helpers;

namespace JONMVC.Website.Tests.Integration.Jewelry
{
    [TestFixture]
    public class JewelryRepositoryTests
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
        public void GetItemsByDynamicSQL_ShouldReturnTheRightAmoutOfItems()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act
            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);   
            //Assert
            jewelrylist.Should().HaveCount(10);

        }

        [Test]
        public void OrderJewelryItemsBy_ShouldReturnTheItemsInTheCorrectOrderByFilterPriceDesc()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            JewelryDynamicOrderBy orderBy = new JewelryDynamicOrderBy("price","desc");

            //Act
            jewelRepository.OrderJewelryItemsBy(orderBy);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert
            var first = jewelrylist[0].Price;
            var last = jewelrylist[jewelrylist.Count - 1].Price;

            first.Should().Be(7500);
            last.Should().Be(1500);




        }

        [Test]
        public void FilterJewelryItemsBy_ShouldOnlyReturnTheRightMetalMediaWhiteGold()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            jewelRepository.FilterMediaByMetal(JewelMediaType.WhiteGold);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert

            jewelrylist.Should().OnlyContain(x => x.Media.IconURLForWebDisplay.Contains("wg")).And.HaveCount(9);
        }

        [Test]
        public void FilterJewelryItemsBy_ShouldOnlyReturnTheRightMetalMediaYellowGold()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            jewelRepository.FilterMediaByMetal(JewelMediaType.YellowGold);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert

            jewelrylist.Should().OnlyContain(x => x.Media.IconURLForWebDisplay.Contains("yg")).And.HaveCount(9);
        }

        [Test]
        public void FilterJewelryItemsBy_ShouldOnlyReturnTheRightMetalMediaAllMetals()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            jewelRepository.FilterMediaByMetal(JewelMediaType.All);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert

            jewelrylist.Should().HaveCount(10);
        }

        [Test]
        public void FilterJewelryItemsBy_ShouldOnlyReturnWhiteGoldMetalString()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            jewelRepository.FilterMediaByMetal(JewelMediaType.WhiteGold);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert

            jewelrylist.Should().OnlyContain(x => x.MetalFullName() == "White Gold 18 Karat").And.HaveCount(9);
        }

        [Test]
        public void FilterJewelryItemsBy_ShouldOnlyReturnYellowGoldMetalString()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            jewelRepository.FilterMediaByMetal(JewelMediaType.YellowGold);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert

            jewelrylist.Should().OnlyContain(x => x.MetalFullName() == "Yellow Gold 18 Karat").And.HaveCount(9);
        }

        [Test]
        public void FilterJewelryItemsBy_ShouldOnlyReturnNineWhiteGoldAndOneYellowGoldMetalString()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            jewelRepository.FilterMediaByMetal(JewelMediaType.All);

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);
            //Assert

            jewelrylist.Where(x => x.MetalFullName() == "White Gold 18 Karat").Should().HaveCount(9);
            jewelrylist.Where(x => x.MetalFullName() == "Yellow Gold 18 Karat").Should().HaveCount(1);
        }

        [Test]
        public void Pages_ShouldReturnTheRightAmountOfPages()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            jewelRepository.ItemsPerPage(3);
            jewelRepository.Page(2);
            //Act

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);

            //Assert
            jewelrylist.Should().HaveCount(3);

        }

        [Test]
        public void Pages_ShouldReturnTheCorrectNumberOfPagesInTheLastPage()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            jewelRepository.ItemsPerPage(3);
            jewelRepository.Page(4);
            //Act

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);

            //Assert
            jewelrylist.Should().HaveCount(10-3*3);

        }

        [Test]
        public void CurrentPage_ShouldReturnTheCurrentPage()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            jewelRepository.ItemsPerPage(3);
            jewelRepository.Page(2);
            //Act

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);

            var currectpage = jewelRepository.CurrentPage;

            //Assert

            currectpage.Should().Be(2);


        }

        [Test]
        public void TotalItems_ShouldReturnTheTotalNumbersOfItems()
        {
            //Arrange
            var dynamicSQL = DynamicSQLWhereObject();

            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            JewelRepository jewelRepository = new JewelRepository(settingManager);

            //Act

            var jewelrylist = jewelRepository.GetJewelsByDynamicSQL(dynamicSQL);

            var totalitems = jewelRepository.TotalItems;

            //Assert

            totalitems.Should().Be(10);

        }

        [Test]
        public void GetJewelByID_ShouldReturnTheRightMediaSetAll()
        {
            //Arrange
            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");
            settingManager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            //Act
            var jewel = jewelRepository.GetJewelByID(59316);
            //Assert
            jewel.MediaSetsOwnedByJewel.Should().Be(JewelMediaType.All);

        }

        [Test]
        public void GetJewelByID_ShouldReturnTheRightMediaSetWhiteGold()
        {
            //Arrange
            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");
            settingManager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            //Act
            var jewel = jewelRepository.GetJewelByID(59324);
            //Assert
            jewel.MediaSetsOwnedByJewel.Should().Be(JewelMediaType.WhiteGold);

        }

        [Test]
        public void GetJewelByID_ShouldReturnTheRightMediaSetYellowGold()
        {
            //Arrange
            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");
            settingManager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            //Act
            var jewel = jewelRepository.GetJewelByID(59325);
            //Assert
            jewel.MediaSetsOwnedByJewel.Should().Be(JewelMediaType.YellowGold);

        }

        [Test]
        public void GetJewelByID_ShouldReturnNullIfWhiteGoldRequestedButBotPresent()
        {
            //Arrange
            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");
            settingManager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            jewelRepository.FilterMediaByMetal(JewelMediaType.WhiteGold);
            //Act
            var jewel = jewelRepository.GetJewelByID(59325);
            //Assert
            jewel.Should().BeNull();

        }

        [Test]
        public void GetJewelByID_ShouldReturnNullIfYellowGoldRequestedButBotPresent()
        {
            //Arrange
            settingManager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");
            settingManager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            JewelRepository jewelRepository = new JewelRepository(settingManager);
            jewelRepository.FilterMediaByMetal(JewelMediaType.YellowGold);
            //Act
            var jewel = jewelRepository.GetJewelByID(59324);
            //Assert
            jewel.Should().BeNull();

        }



       
        #region Helpers

        private static DynamicSQLWhereObject DynamicSQLWhereObject()
        {
            const string pattern = "jeweltype_id = @0 and category_id = @1";
            var values = new List<object>() { 2, 7 };
            var dynamicSQL = new DynamicSQLWhereObject(pattern, values);
            return dynamicSQL;
        }

        #endregion

       
    }
}