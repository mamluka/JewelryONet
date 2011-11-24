using System;
using System.Text;
using System.Web.Routing;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    //TODO add a match checker to rihno mok expectations
    [TestFixture]
    public class TabsForJewelDesignNavigationTests : JewelDesignTestsBase
    {
        [Test]
        public void Build_ShouldBuildADefaultTabNavigationWhenAEmptyCustomJewelObjectIsPassedAndReturn3Tabs()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs.Should().HaveCount(3);
        }

        [Test]
        public void Build_ShouldBuildADefaultTabNavigationWhenAEmptyCustomJewelObjectIsPassedAndReturnTheCorrectTitles()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
           
            tabs[0].Title.Should().Be("Choose Diamond");
            tabs[1].Title.Should().Be("Choose a Setting");
            tabs[2].Title.Should().Be("Your Order");

        }

        [Test]
        public void Build_ShouldBuildADefaultTabNavigationWhenAEmptyCustomJewelObjectIsPassedAndReturnEmptyStringAmount()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].Title.Should().Be("Choose Diamond");
            tabs[1].Title.Should().Be("Choose a Setting");
            tabs[2].Title.Should().Be("Your Order");

        }

        [Test]
        public void Build_ShouldBuildADefaultTabNavigationWhenAEmptyCustomJewelObjectIsPassedAndReturnTheFirstTabHighlightStateAsOnAndTheOtherOff()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].HighlightState.Should().Be("on");
            tabs[1].HighlightState.Should().Be("off");
            tabs[2].HighlightState.Should().Be("off");

        }

        [Test]
        public void Build_ShouldHighlightTheTabWhenAskedForDepandingOnThePageEnum()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            builder.WhichTabToHighLight(NagivationTabType.YourOrder);
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].HighlightState.Should().Be("off");
            tabs[1].HighlightState.Should().Be("off");
            tabs[2].HighlightState.Should().Be("on");

        }

        [Test]
        public void Build_ShouldBuildADefaultTabNavigationWhenAEmptyCustomJewelObjectIsPassedAndReturnTheRightCSSClasses()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].CssClass.Should().Be("diamond");
            tabs[1].CssClass.Should().Be("setting");
            tabs[2].CssClass.Should().Be("end");

        }

        [Test]
        public void Build_ShouldBuildADefaultTabNavigationWhenAEmptyCustomJewelObjectIsPassedAndReturnFalseOnTheLinks()
        {
            //Arrange
            var builder = CreateDefaultTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].HasEditAndViewLinks.Should().BeFalse();
            tabs[1].HasEditAndViewLinks.Should().BeFalse();
            tabs[2].HasEditAndViewLinks.Should().BeFalse();

        }



        [Test]
        public void Build_DiamondTabShouldChangeTheTitleIfDiamondIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].Title.Should().Be("Your Diamond");


        }

        [Test]
        public void Build_DiamondTabShouldReturnPriceIfDiamondIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].Amount.Should().Be("$25,478");
        }


        [Test]
        public void Build_FinalTabShouldReturnPriceIfDiamondIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[2].Amount.Should().Be("$25,478");
        }

        [Test]
        public void Build_DiamondTabShouldReturnHasLinksIfDiamondIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].HasEditAndViewLinks.Should().BeTrue();
        }

        [Test]
        public void Build_DiamondTabShouldReturnViewRouteAndCallTheRightRouteIfDiamondIsPresent()
        {
            //Arrange
            var customJewel = new CustomJewelPersistenceBase();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateMock<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;

            webHelpers.Expect(
                x =>
                x.RouteUrl(Arg<string>.Is.Equal("Diamond"),
                           Arg<RouteValueDictionary>.Matches(d=> (int)d["DiamondID"] == customJewel.DiamondID && (int)d["SettingID"] == customJewel.SettingID))).Return(
                                                                     "/diamond/view/uri").Repeat.Any();


            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].ViewRoute.Should().Be("/diamond/view/uri");
            webHelpers.VerifyAllExpectations();
        }

        [Test]
        public void Build_DiamondTabShouldReturnModifyRouteAndCallTheRightRouteIfDiamondIsPresent()
        {
            //Arrange
            var customJewel = new CustomJewelPersistenceBase();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateMock<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;

            webHelpers.Expect(
                x =>
                x.RouteUrl(Arg<string>.Is.Equal("DiamondSearch"),
                           Arg<RouteValueDictionary>.Matches(d => (int)d["DiamondID"] == customJewel.DiamondID && (int)d["SettingID"] == customJewel.SettingID))).
                Return("/JewelDesign/DiamondSearch").Repeat.Any();


            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].ModifyRoute.Should().Be("/JewelDesign/DiamondSearch");
            webHelpers.VerifyAllExpectations();
        }

        //---------------------------------------------------------------

        [Test]
        public void Build_SettingTabShouldChangeTheTitleIfSettingIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDAndSettingIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[1].Title.Should().Be("Your Setting");


        }

        [Test]
        public void Build_SettingTabShouldReturnPriceIfSettingIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDAndSettingIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[1].Amount.Should().Be("$10,000");
        }

        [Test]
        public void Build_SettingTabShouldReturnPriceIfSettingIsPresentTriangulateWithDifferentID()
        {
            //Arrange
            var builder =
                CreateDefaultTabsForJewelDesignBuilderWithPersistenceAsAParameter(new CustomJewelPersistenceBase()
                                                                                      {
                                                                                          DiamondID = 2,
                                                                                          SettingID = 1113
                                                                                      });
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[1].Amount.Should().Be("$10,000");
        }

        [Test]
        public void Build_FinalTabShouldReturnPriceIfDiamondAndSettingIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDAndSettingIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[2].Amount.Should().Be("$35,478");
        }

        [Test]
        public void Build_SettingTabShouldReturnHasLinksIfSettingIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDAndSettingIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[1].HasEditAndViewLinks.Should().BeTrue();
        }

        [Test]
        public void Build_SettingabShouldReturnViewRouteAndCallTheRightRouteIfSettingIsPresent()
        {
            //Arrange
            var customJewel = new CustomJewelPersistenceBase();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateMock<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;
            customJewel.SettingID = SETTING_ID;

            webHelpers.Expect(
                x =>
                x.RouteUrl(Arg<string>.Is.Equal("Setting"),
                           Arg<RouteValueDictionary>.Matches(d=> (int)d["DiamondID"] == customJewel.DiamondID && (int)d["SettingID"] == customJewel.SettingID))).Return(
                                                                     "/jewelryitem/view/uri").Repeat.Any();


            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            //Act
            var tabs = builder.Build();
            //Assert
            webHelpers.VerifyAllExpectations();

            tabs[1].ViewRoute.Should().Be("/jewelryitem/view/uri");
        }

        [Test]
        public void Build_SettingTabShouldReturnModifyRouteAndCallTheRightRouteIfSettingIsPresent()
        {
            //Arrange
            var customJewel = new CustomJewelPersistenceBase();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateMock<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;
            customJewel.SettingID = SETTING_ID;

            webHelpers.Expect(
                x =>
                x.RouteUrl(Arg<string>.Is.Equal("ChooseSetting"),
                           Arg<RouteValueDictionary>.Is.Anything)).
                Return("/JewelDesign/DiamondSearch").Repeat.Any();


            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[1].ModifyRoute.Should().Be("/JewelDesign/DiamondSearch");
            webHelpers.VerifyAllExpectations();
        }

        [Test]
        public void Build_ShouldReturnCssClassesEvenIfAllIsPresent()
        {
            //Arrange
            var builder = CreateWithDiamondIDAndSettingIDTabsForJewelDesignBuilder();
            //Act
            var tabs = builder.Build();
            //Assert
            tabs[0].CssClass.Should().Be("diamond");
            tabs[1].CssClass.Should().Be("setting");
            tabs[2].CssClass.Should().Be("end");

        }

        private TabsForJewelDesignNavigationBuilder CreateDefaultTabsForJewelDesignBuilder()
        {
            var diamondRepository = new FakeDiamondRepository(mapper);
            var customJewel = new CustomJewelPersistenceBase();
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());
            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            return builder;
        }

        private TabsForJewelDesignNavigationBuilder CreateDefaultTabsForJewelDesignBuilderWithPersistenceAsAParameter(CustomJewelPersistenceBase customJewel)
        {
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());
            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            return builder;
        }



        private TabsForJewelDesignNavigationBuilder CreateWithDiamondIDTabsForJewelDesignBuilder()
        {
            var customJewel = new CustomJewelPersistenceBase();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());
            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                "not important for this test");

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;

            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository, jewelRepository, webHelpers);
            return builder;
        }

        private TabsForJewelDesignNavigationBuilder CreateWithDiamondIDAndSettingIDTabsForJewelDesignBuilder()
        {
            var customJewel = new CustomJewelPersistenceBase();
            var diamondRepository = new FakeDiamondRepository(mapper);
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            webHelpers.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return(
                "not important for this test");

            customJewel.DiamondID = FIRST_DIAMOND_IN_REP;
            customJewel.SettingID = SETTING_ID;

            var builder = new TabsForJewelDesignNavigationBuilder(customJewel, diamondRepository,jewelRepository, webHelpers);
            return builder;
        }
    }
}