using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Routing;
using AutoMapper;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class DiamondSearchViewModelBuilderTests:JewelDesignTestsBase
    {
        /// <summary>
        /// Prepares mock repository
        /// </summary>

        [Test]
        public void Build_ShouldSetTheTabNAvigationModelProperty()
        {
            //Arrange
            var customJewel = new CustomJewelPersistenceForDiamondSearch();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                "not important for this test");

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);

            var builder = new DiamondSearchViewModelBuilder(customJewel, tabsForJewelDesignBuilder);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.TabsForJewelDesignNavigation.Should().HaveCount(3);
        }

        [Test]
        public void Build_ShouldReturnAJavaScriptJsonInitializerWithSettingIDBecauseItWasSpecifiedInTheRouteDic()
        {
            var customJewel = new CustomJewelPersistenceForDiamondSearch();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT);

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;
            customJewel.SettingID = SETTING_ID;

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);

            var builder = new DiamondSearchViewModelBuilder(customJewel, tabsForJewelDesignBuilder);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JSONClientScriptInitializer["SettingID"].Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID.ToString());

        }

        [Test]
        public void Build_ShouldReturnAJavaScriptJsonInitializerWithMediaBecauseItWasSpecifiedInTheRouteDic()

        {
            var customJewel = new CustomJewelPersistenceForDiamondSearch();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT);

            customJewel.MediaType  = JewelMediaType.YellowGold;

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);

            var builder = new DiamondSearchViewModelBuilder(customJewel, tabsForJewelDesignBuilder);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JSONClientScriptInitializer["MediaType"].Should().Be(customJewel.MediaType);

        }

        [Test]
        public void Build_ShouldReturnAJavaScriptJsonInitializerWithJewelSizeIDBecauseItWasSpecifiedInTheRouteDic()
        {
            var customJewel = new CustomJewelPersistenceForDiamondSearch();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT);


            customJewel.Size=Tests.SAMPLE_JEWEL_SIZE_725;

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);

            var builder = new DiamondSearchViewModelBuilder(customJewel, tabsForJewelDesignBuilder);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JSONClientScriptInitializer["Size"].Should().Be(customJewel.Size);

        }

        [Test]
        public void Build_ShouldReturnAJavaScriptJsonInitializerWithShapeForTheSearchInitBecauseItWasSpecifiedInTheRouteDic()
        {
            var customJewel = new CustomJewelPersistenceForDiamondSearch();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT);

            customJewel.Shape = Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING;

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);

            var builder = new DiamondSearchViewModelBuilder(customJewel, tabsForJewelDesignBuilder);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JSONClientScriptInitializer["Shape"].Should().Be(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING);

        }

        [Test]
        public void Build_ShouldReturnAJavaScriptJsonInitializerWithReportForTheSearchInitBecauseItWasSpecifiedInTheRouteDic()
        {
            var customJewel = new CustomJewelPersistenceForDiamondSearch();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                Tests.STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT);

            customJewel.Report = Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING;

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);

            var builder = new DiamondSearchViewModelBuilder(customJewel, tabsForJewelDesignBuilder);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.JSONClientScriptInitializer["Report"].Should().Be(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING);

        }

    }
}