using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using AutoMapper.Mappers;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.JewelryItem;
using JONMVC.Website.Tests.Unit.Tabs;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class JewelDesignControllerTests:JewelDesignTestsBase
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        /// 
        ///  [Test]
        public void Index_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var controller = CreateDefaultJewelDesignController();
            //Act
            var result = controller.Index();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();


        }

        [Test]
        public void DiamondSearch_ShouldReturnTheRightModel()
        {
            //Arrange
            var controller = CreateDefaultJewelDesignController();
            //Act
            var result = controller.DiamondSearch(new CustomJewelPersistenceForDiamondSearch());
            //Assert
            result.AssertViewRendered().WithViewData<DiamondSearchViewModel>();

           
        }

       

        [Test]
        public void Diamonds_ShouldReturnJsonResult()
        {
            //Arrange
            var controller = CreateDefaultJewelDesignController();



            //Act
            var result = controller.Diamonds(new DiamondSearchParametersGivenByJson());
            //Assert
            result.AssertResultIs<JsonResult>();
        }

        [Test]
        public void Diamond_ShouldRenderTheViewWithTheRightModel()
        {
            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var customJewelInDiamond = new CustomJewelPersistenceInDiamond();
            customJewelInDiamond.DiamondID = 1;
            ;

            //Act
            var result = controller.Diamond(customJewelInDiamond);
            //Assert
            result.AssertViewRendered().WithViewData<DiamondViewModel>();

        }

        [Test]
        public void ChooseSetting_ShouldRenderTheViewWithTheRightModel()
        {
           
            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var chooseSettingViewModel = new ChooseSettingViewModel();
            chooseSettingViewModel.TabKey = TabTestsBase.TabKey;
            chooseSettingViewModel.TabId = TabTestsBase.TabID1;

            //Act
            var result = controller.ChooseSetting(chooseSettingViewModel);
            //Assert
            result.AssertViewRendered().WithViewData<ChooseSettingViewModel>();

        }

        [Test]
        public void ChooseSetting_ShouldRedirectToChooseSetting()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var chooseSettingViewModel = new ChooseSettingViewModel();
            chooseSettingViewModel.TabKey = TabTestsBase.TabKey;
            chooseSettingViewModel.TabId = TabTestsBase.TabID1;

            //Act
            var result = controller.ChooseSettingPost(chooseSettingViewModel);
            //Assert
            result.AssertActionRedirect();

        }

        [Test]
        [Ignore]
        //TODO hard to code with current schema of tests
        public void Setting_ShouldTellTheRepositoryAboutTheMediaSet()
        {

            //Arrange
            var jewelRepository = MockRepository.GenerateMock<IJewelRepository>();
            
            jewelRepository.Expect(x => x.FilterMediaByMetal(Arg<JewelMediaType>.Is.Equal(JewelMediaType.YellowGold)));
           

            var controller = CreateDefaultJewelDesignControllerWithCustomRepository(jewelRepository);

            var customJewelForSetting = new CustomJewelPersistenceForSetting();

            customJewelForSetting.DiamondID = 1;
            customJewelForSetting.SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;


            //Act
            var result = controller.Setting(customJewelForSetting);
            //Assert
            result.AssertViewRendered().WithViewData<SettingViewModel>();

        }

        [Test]
        public void Setting_ShouldRenderTheViewWithTheRightModel()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var customJewelForSetting = new CustomJewelPersistenceForSetting();

            customJewelForSetting.DiamondID = 1;
            customJewelForSetting.SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;


            //Act
            var result = controller.Setting(customJewelForSetting);
            //Assert
            result.AssertViewRendered().WithViewData<SettingViewModel>();

        }

        [Test]
        public void End_ShouldRenderTheViewWithTheRightModel()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var customJewelForSetting = new CustomJewelPersistenceInEndPage();

            customJewelForSetting.DiamondID = 1;
            customJewelForSetting.SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;
            customJewelForSetting.MediaType = JewelMediaType.WhiteGold;
            customJewelForSetting.Size = Tests.SAMPLE_JEWEL_SIZE_725;


            //Act
            var result = controller.End(customJewelForSetting);
            //Assert
            result.AssertViewRendered().WithViewData<EndViewModel>();

        }

        [Test]
        public void End_ShouldRenderTheViewWithTheRightModelEvenIfMediaTypeIsMissing()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var customJewelForSetting = new CustomJewelPersistenceInEndPage();

            customJewelForSetting.DiamondID = 1;
            customJewelForSetting.SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;
            //customJewelForSetting.MediaType = JewelMediaType.WhiteGold;
            customJewelForSetting.Size = Tests.SAMPLE_JEWEL_SIZE_725;


            //Act
            var result = controller.End(customJewelForSetting);
            //Assert
            result.AssertViewRendered().WithViewData<EndViewModel>();

        }

        [Test]
        public void RedirectSetting_ShouldRedirectWithRightParametersForCommandOneWhereWeGoToEnd    ()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var redirectSettingModel = new RedirectSettingModel()
                                           {
                                               DiamondID = Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                               MediaType = JewelMediaType.YellowGold,
                                               CommandID = 1,
                                               SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                               Size = "8"

                                           };




            //Act
            var result = controller.RedirectSetting(redirectSettingModel);
            //Assert
            result.AssertActionRedirect()
                .WithParameter("DiamondID",redirectSettingModel.DiamondID);

            result.AssertActionRedirect()
               .WithParameter("SettingID", redirectSettingModel.SettingID);

            result.AssertActionRedirect()
               .WithParameter("Size", redirectSettingModel.Size);

            result.AssertActionRedirect()
               .WithParameter("MediaType", redirectSettingModel.MediaType);

        }

        [Test]
        public void RedirectSetting_ShouldRedirectWithRightParametersForCommandTwoWhereWeGoToDiamondSearch()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var redirectSettingModel = new RedirectSettingModel()
            {
                DiamondID = Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                MediaType = JewelMediaType.YellowGold,
                CommandID = 2,
                SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                Size = "8"

            };




            //Act
            var result = controller.RedirectSetting(redirectSettingModel);
            //Assert
            result.AssertActionRedirect()
                .WithParameter("DiamondID", 0);

            result.AssertActionRedirect()
               .WithParameter("SettingID", redirectSettingModel.SettingID);

            result.AssertActionRedirect()
               .WithParameter("Size", redirectSettingModel.Size);

            result.AssertActionRedirect()
               .WithParameter("MediaType", redirectSettingModel.MediaType);

        }

        [Test]
        public void RedirectSetting_ShouldRedirectWithRightParametersForCommandThreeWhereWeGoToShoppingCart()
        {

            //Arrange
            var controller = CreateDefaultJewelDesignController();

            var redirectSettingModel = new RedirectSettingModel()
            {
                DiamondID = Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                MediaType = JewelMediaType.YellowGold,
                CommandID = 3,
                SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                Size = "8"

            };




            //Act
            var result = controller.RedirectSetting(redirectSettingModel);
            //Assert
            result.AssertActionRedirect().ToController("Checkout").ToAction("ShoppingCartAddJewel");

            result.AssertActionRedirect()
               .WithParameter("id", redirectSettingModel.SettingID);

            result.AssertActionRedirect()
               .WithParameter("size", redirectSettingModel.Size);

            result.AssertActionRedirect()
               .WithParameter("MediaType", redirectSettingModel.MediaType);

        }



        private JewelDesignController CreateDefaultJewelDesignController()
        {
            var diamondRepository = new FakeDiamondRepository(mapper);
            var formatter = MockRepository.GenerateStub<IJONFormatter>();
            var jewelryRepository = new FakeJewelRepository(new SettingManager());
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();

            formatter.Stub(x => x.ToCaratWeight(Arg<decimal>.Is.Anything)).Return("Not important for this test");
            formatter.Stub(x => x.ToGramWeight(Arg<decimal>.Is.Anything)).Return("Not important for this test");
            formatter.Stub(x => x.ToMilimeter(Arg<decimal>.Is.Anything)).Return("Not important for this test");

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
            var xmlSourceFactory = new FakeXmlSourceFactory();
            var tabRepository = TabsViewModelBuilderTests.CreateStubTabsRepository(TabTestsBase.TabKey);

            var diamondHelpBuilder = new DiamondHelpBuilder(new FakeXmlSourceFactory());

            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);


            var controller = new JewelDesignController(diamondRepository, formatter, mapper, jewelryRepository, webHelpers,
                                                       xmlSourceFactory, fileSystem, diamondHelpBuilder, tabRepository,pathbarGenerator, fakeTestimonailRepository);
            return controller;
        }

        private JewelDesignController CreateDefaultJewelDesignControllerWithCustomRepository(IJewelRepository jewelRepository)
        {
            var diamondRepository = new FakeDiamondRepository(mapper);
            var formatter = MockRepository.GenerateStub<IJONFormatter>();
         
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();

            formatter.Stub(x => x.ToCaratWeight(Arg<decimal>.Is.Anything)).Return("Not important for this test");
            formatter.Stub(x => x.ToGramWeight(Arg<decimal>.Is.Anything)).Return("Not important for this test");
            formatter.Stub(x => x.ToMilimeter(Arg<decimal>.Is.Anything)).Return("Not important for this test");

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
            var xmlSourceFactory = new FakeXmlSourceFactory();
            var tabRepository = TabsViewModelBuilderTests.CreateStubTabsRepository(TabTestsBase.TabKey);

            var diamondHelpBuilder = new DiamondHelpBuilder(new FakeXmlSourceFactory());

            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);


            var controller = new JewelDesignController(diamondRepository, formatter, mapper, jewelRepository, webHelpers,
                                                       xmlSourceFactory, fileSystem, diamondHelpBuilder, tabRepository, pathbarGenerator, fakeTestimonailRepository);
            return controller;
        }

    }
}