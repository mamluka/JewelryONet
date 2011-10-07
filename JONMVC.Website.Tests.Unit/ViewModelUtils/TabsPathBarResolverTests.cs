using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Routing;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.ViewModelUtils
{
    [TestFixture]
    public class TabsPathBarResolverTests
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
        public void GeneratePathBarDictionary_GenerateCorrectLinkToTheParentTabThatComesAfterHome()
        {
            //Arrange
            var resolver = CreateDetaultTabsPathBarResolver();

            var viewModel = CreateViewModel(Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT);

            //Act
            var list = resolver.GeneratePathBarDictionary(viewModel);
            //Assert
            list[0].Key.Should().Be("shorttitle");
            list[0].Value.Should().Be("link");



        }

        [Test]
        public void GeneratePathBarDictionary_GenerateCorrectLinkToTheCurrectTab()
        {
            //Arrange
            var resolver = CreateDetaultTabsPathBarResolver();

            var currentTabCaption = fixture.CreateAnonymous<string>();

            var viewModel = CreateViewModel(currentTabCaption);
            //Act
            var list = resolver.GeneratePathBarDictionary(viewModel);
            //Assert
            list[1].Key.Should().Be(currentTabCaption);
            list[1].Value.Should().Be("");



        }

        //TODO add some error handling for sure there can be found some

        private TabsViewModel CreateViewModel(string caption)
        {
            var tabID = fixture.CreateAnonymous<string>();

            var currentTab = new Tab(caption, tabID, 0);
            fixture.Customize<TabsViewModel>(m => m.With(x=> x.Tabs, new List<Tab>()).Do(x => x.Tabs.Add(currentTab)).With(x => x.TabId, tabID).With(x=>x.ShortTitle,"shorttitle"));

            var viewModel = fixture.CreateAnonymous<TabsViewModel>();
            return viewModel;
        }

        private static TabsPathBarResolver CreateDetaultTabsPathBarResolver()
        {
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Equal("Tabs"), Arg<RouteValueDictionary>.Is.Anything)).Return
                ("link");

            var resolver = new TabsPathBarResolver();
            resolver.WebHelpers = webHelpers;
            return resolver;
        }
    }
}