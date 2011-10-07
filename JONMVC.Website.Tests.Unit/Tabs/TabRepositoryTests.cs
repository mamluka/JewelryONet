using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using JONMVC.Website.Models.Tabs;
using FluentAssertions;
namespace JONMVC.Website.Tests.Unit.Tabs
{
    [TestFixture]
    public class TabRepositoryTests:TabTestsBase
    {
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// template behavior and state testing method
        /// </summary>
        [Test]
        public void GetTabCollectionByKey_ShouldReturnTheCorrectCollection()
        {

            
            var tabrepository = new TabsRepository(fakeXmlSourceFactory);

            var tabs = tabrepository.GetTabsCollectionByKey(TAB_KEY);

            Assert.That(tabs.Count,Is.EqualTo(3));

        }

       

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTabsCollectionByKey_ShouldRaiseExceptionIfXmlIsNull()
        {
            var xmlSourceFactory = MockRepository.GenerateStub<IXmlSourceFactory>();
            xmlSourceFactory.Stub(x => x.TabSource()).Return(null);

            var tabrepository = new TabsRepository(xmlSourceFactory);


        }

        [Test]
        public void GetTabCollectionByKey_ShouldReturnTheRightCaptionWithRightOrder()
        {
            var tabrepository = new TabsRepository(fakeXmlSourceFactory);

            var tabs = tabrepository.GetTabsCollectionByKey(TAB_KEY);

            Assert.That(tabs[0].Caption, Is.EqualTo("Rings"));
            Assert.That(tabs[1].Caption, Is.EqualTo("Studs"));
            Assert.That(tabs[2].Caption, Is.EqualTo("Pendants"));
        }

        [Test]
        public void GetTabCollectionByKey_ShouldReturnTheRightIDWithRightOrder()
        {
            var tabrepository = new TabsRepository(fakeXmlSourceFactory);

            var tabs = tabrepository.GetTabsCollectionByKey(TAB_KEY);

            Assert.That(tabs[0].Id, Is.EqualTo("engagement-rings"));
            Assert.That(tabs[1].Id, Is.EqualTo("diamod-studs"));
            Assert.That(tabs[2].Id, Is.EqualTo("diamod-pendants"));
        }
       
        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetTabCollectionByKey_ShouldThrowAnExceptionIfBadKey()
        {
            //Arrange

            var tabrepository = new TabsRepository(fakeXmlSourceFactory);

            tabrepository.GetTabsCollectionByKey(TAB_KEY+"badkey");

            //Act

            //Assert
        }
    }
}