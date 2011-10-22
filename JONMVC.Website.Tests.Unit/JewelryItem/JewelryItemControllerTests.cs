using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using JONMVC.Website.Controllers;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using NMoneys;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    [TestFixture]
    public class JewelryItemControllerTests:JewelryItemTestsBase
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Index_ShouldReturnTheRightViewType()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var resultview = controller.Index(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,null);
            //Assert

            resultview.AssertViewRendered().WithViewData<JewelryItemViewModel>();

        }

       

        [Test]
        public void Index_ShouldReturnWhiteGoldMediaAsDefault()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var resultview = controller.Index(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,null);
            //Assert

            var actual = resultview as ViewResult;
            var model = actual.Model as JewelryItemViewModel;

            model.MainJewelPicture.Thumb.Should().Contain("wg");

        }

        [Test]
        public void Index_ShouldReturnYellowGoldMediaWhenRequested()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var resultview = controller.Index(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, JewelMediaType.YellowGold);
            //Assert

            var actual = resultview as ViewResult;
            var model = actual.Model as JewelryItemViewModel;

            model.MainJewelPicture.Thumb.Should().Contain("yg");

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Index_ShouldThrowExceptionIfWhiteGoldRequestedButNotPresent()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var resultview = controller.Index(1112, JewelMediaType.WhiteGold);
            //Assert

            

        }

        [Test]
        public void FindOutMediaOptions_ShouldReturn2MediaSetsBecauseTheFilesAreOnDisk()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var jsonresult = controller.MediaSets(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID) as JsonResult;
            //Assert
            var medialist = jsonresult.Data as MediaSetsJsonModel;

            medialist.MediaSets.Should().HaveCount(2);


        }

        [Test]
        public void FindOutMediaOptions_ShouldReturnTheRightPrice()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var jsonresult = controller.MediaSets(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID) as JsonResult;

            var price = new Money((decimal)9999.99, Currency.Usd).Format("{1}{0:#,0}");
            
            //Assert
            var medialist = jsonresult.Data as MediaSetsJsonModel;

            medialist.Price.Should().Be(price);


        }

        [Test]
        public void FindOutMediaOptions_ShouldReturnTheRightID()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var jsonresult = controller.MediaSets(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID) as JsonResult;

            //Assert
            var medialist = jsonresult.Data as MediaSetsJsonModel;

            medialist.ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);


        }

        [Test]
        public void FindOutMediaOptions_ShouldReturnTheRightTitle()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var jsonresult = controller.MediaSets(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID) as JsonResult;

            //Assert
            var medialist = jsonresult.Data as MediaSetsJsonModel;

            medialist.Title.Should().Be("title");


        }

        [Test]
        public void FindOutMediaOptions_ShouldReturnWhiteGoldRouteLinkFromItem()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var jsonresult = controller.MediaSets(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID) as JsonResult;

            //Assert
            var medialist = jsonresult.Data as MediaSetsJsonModel;

            medialist.MediaSetRouteLinkDictionary[Metal.GetFullName(JewelMediaType.WhiteGold)].Should().Contain(
                "WhiteGold");


        }

        [Test]
        public void FindYourRingSize_ShouldRenderAnEmptyView()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            //Act
            var result = controller.FindYourRingSize();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void PostBestOffer_ShouldCallTheBestOfferSaveAndEmailCustomerMethod()
        {
            //Arrange
            var model = fixture.CreateAnonymous<BestOfferViewModel>();

            var bestOffer = MockRepository.GenerateMock<IBestOffer>();
            bestOffer.Expect(
                x =>
                x.EmailToAdmin(
                    Arg<BestOfferViewModel>.Matches(
                        m =>
                        m.JewelID == model.JewelID &&
                        m.OfferEmail == model.OfferEmail &&
                        m.OfferPrice == model.OfferPrice
                        )));

            var controller = CreateJewelryItemControllerWithCustomBestOffer(bestOffer);

            //Act
            controller.PostBestOffer(model);
            //Assert
         //   result.AssertViewRendered().WithViewData<EmptyViewModel>();
            bestOffer.VerifyAllExpectations();
        }

      

        [Test]
        public void AddToWishList_ShouldReturnaJsonWithErrorIfBadIDIsCalled()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
           
            //Act

            var result = controller.AddToWishList(Tests.BAD_FAKE_JEWELRY_ID);

            //Assert

            var json = result as JsonResult;
            var actual = json.Data as OporationWithoutReturnValueJsonModel;
            actual.HasError = true;

        }

        [Test]
        public void AddToWishList_ShouldReturnaJsonWithoutErrorIfJewelISOK()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();

            //Act

            var result = controller.AddToWishList(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert

            var json = result as JsonResult;
            var actual = json.Data as OporationWithoutReturnValueJsonModel;
            actual.HasError = false;

        }

        [Test]
        public void AddToWishList_ShouldCallTheWishListPersistenceSaveIDMethod()
        {
            //Arrange


            var wishListPersistence = MockRepository.GenerateStrictMock<IWishListPersistence>();
            wishListPersistence.Expect(x => x.SaveID(Arg<int>.Is.Equal(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID))).
                Repeat.Once();

            var controller = CreateJewelryItemControllerWithCustomWishListPersistence(wishListPersistence);


            //Act

            var result = controller.AddToWishList(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            wishListPersistence.VerifyAllExpectations();

        }

        [Test]
        public void ClearWishList_ShouldCallTheWishListPersistenceClearMethod()
        {
            //Arrange


            var wishListPersistence = MockRepository.GenerateStrictMock<IWishListPersistence>();
            wishListPersistence.Expect(x => x.ClearWishList()).
                Repeat.Once();

            var controller = CreateJewelryItemControllerWithCustomWishListPersistence(wishListPersistence);


            //Act

            var result = controller.ClearWishList();

            //Assert
            wishListPersistence.VerifyAllExpectations();

        }

       

        [Test]
        public void RemoveFromishList_ShouldCallTheWishListPersistenceRemoveIDMethod()
        {
            //Arrange

            var wishListPersistence = MockRepository.GenerateStrictMock<IWishListPersistence>();
            wishListPersistence.Expect(x => x.RemoveID(Arg<int>.Is.Equal(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID))).Repeat.Once();


            var controller = CreateJewelryItemControllerWithCustomWishListPersistence(wishListPersistence);

            //Act

            var result = controller.RemoveFromWishList(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            wishListPersistence.VerifyAllExpectations();

        }

        [Test]
        public void RemoveFromishList_ShouldRedirectToWishListPageAfterRemoval()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();

            //Act

            var result = controller.RemoveFromWishList(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            result.AssertActionRedirect().ToAction("Wishlist").ToController("Services");

        }

        [Test]
        public void EmailRing_ShouldTryToSendTheEmail()
        {
            //Arrange
            var mailer = MockRepository.GenerateStub<IUserMailer>();
            mailer.Expect(x => x.EmailRing(Arg<string>.Is.Anything, Arg<EmailRingEmailTemplateViewModel>.Is.Anything)).
                Repeat.Once();

            var model = fixture.CreateAnonymous<EmailRingModel>();

            var controller = CreateJewelryItemControllerWithCustomerMailer(mailer);
            //Act
            controller.EmailRing(model);
            //Assert
            mailer.VerifyAllExpectations();  
        }

        [Test]
        public void EmailRing_ShouldSendWithTheRightEmail()
        {
            //Arrange
            var model = fixture.CreateAnonymous<EmailRingModel>();
            var mailer = MockRepository.GenerateStub<IUserMailer>();
            mailer.Expect(x => x.EmailRing(Arg<string>.Is.Equal(model.FriendEmail), Arg<EmailRingEmailTemplateViewModel>.Is.Anything)).
                Repeat.Once();

            

            var controller = CreateJewelryItemControllerWithCustomerMailer(mailer);
            //Act
            controller.EmailRing(model);
            //Assert
            mailer.VerifyAllExpectations();
        }

        [Test]
        public void EmailRing_ShouldReturnNoErrorOccuredJson()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            var model = fixture.Build<EmailRingModel>().With(x=> x.ID,Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID).CreateAnonymous();
            //Act
            var viewModel = controller.EmailRing(model) as JsonResult;
            //Assert
            var actual = viewModel.Data as OporationWithoutReturnValueJsonModel;
            actual.HasError.Should().BeFalse();
        }

        [Test]
        public void EmailRing_ShouldReturnHasErrorAsTrueWhenWrongJeweLID()
        {
            //Arrange
            var controller = CreateDefaultJewelryItemControllerSetup();
            var dosentExists = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID + 1000;

            var model = fixture.Build<EmailRingModel>().With(x => x.ID, dosentExists).CreateAnonymous();
            //Act
            var viewModel = controller.EmailRing(model) as JsonResult;
            //Assert
            var actual = viewModel.Data as OporationWithoutReturnValueJsonModel;
            actual.HasError.Should().BeTrue();
        }



        private JewelryItemController CreateJewelryItemControllerWithCustomBestOffer(IBestOffer bestOffer)
        {
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");
            var settingManager = new FakeSettingManager();
            var jewelryRepository = new FakeJewelRepository(settingManager);
            var webHelpers = GetWebHelpers();
            var mediaSetBuilder = GetMediaSetBuilder();
            var fakeMailer = MockRepository.GenerateStub<IUserMailer>();

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);
            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var controller = new JewelryItemController(jewelryRepository, mediaSetBuilder, webHelpers, fileSystem, bestOffer,
                                                       wishListPersistence, fakeTestimonailRepository, fakeMailer, pathbarGenerator, mapper);
            return controller;
        }

        private JewelryItemController CreateJewelryItemControllerWithCustomerMailer(IUserMailer mailer)
        {
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");
            var settingManager = new FakeSettingManager();
            var jewelryRepository = new FakeJewelRepository(settingManager);
            var webHelpers = GetWebHelpers();
            var mediaSetBuilder = GetMediaSetBuilder();
            var bestOffer = MockRepository.GenerateStub<IBestOffer>();

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);
            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var controller = new JewelryItemController(jewelryRepository, mediaSetBuilder, webHelpers, fileSystem, bestOffer,
                                                       wishListPersistence, fakeTestimonailRepository, mailer, pathbarGenerator, mapper);
            return controller;
        }

        private JewelryItemController CreateJewelryItemControllerWithCustomWishListPersistence(
           IWishListPersistence wishListPersistence)
        {
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");
            var settingManager = new FakeSettingManager();
            var jewelryRepository = new FakeJewelRepository(settingManager);
            var webHelpers = GetWebHelpers();
            var mediaSetBuilder = GetMediaSetBuilder();

            var bestOffer = MockRepository.GenerateStub<IBestOffer>();
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);
            var fakeMailer = MockRepository.GenerateStub<IUserMailer>();
            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var controller = new JewelryItemController(jewelryRepository, mediaSetBuilder, webHelpers, fileSystem,
                                                       bestOffer, wishListPersistence, fakeTestimonailRepository, fakeMailer, pathbarGenerator, mapper);
            return controller;
        }

        private JewelryItemController CreateDefaultJewelryItemControllerSetup()
        {
            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber(Tests.FAKE_JEWEL_ITEMNUMBER);
            var settingManager = new FakeSettingManager();
            var jewelryRepository = new FakeJewelRepository(settingManager);
            var webHelpers = GetWebHelpers();
            var mediaSetBuilder = GetMediaSetBuilder();
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);
            var bestOffer = MockRepository.GenerateStub<IBestOffer>();

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            var fakeMailer = MockRepository.GenerateStub<IUserMailer>();
            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var controller = new JewelryItemController(jewelryRepository, mediaSetBuilder, webHelpers, fileSystem, bestOffer, wishListPersistence, fakeTestimonailRepository, fakeMailer, pathbarGenerator, mapper);
            return controller;
        }

        private IMediaSetBuilder GetMediaSetBuilder()
        {
            //the parameters are not important for the test but must be included for the extract and override pattern

            var media1 = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                MediaSet = JewelMediaType.WhiteGold

            };

            var media2 = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-yg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-yg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-yg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-yg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-yg.flv",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-yg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-yg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-yg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-yg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-yg.flv",
                MediaSet = JewelMediaType.YellowGold

            };


            var fakeMediaSetBuilder = MockRepository.GenerateStub<IMediaSetBuilder>();

            fakeMediaSetBuilder.Stub(x => x.Build(Arg<string>.Is.Anything,Arg<JewelMediaType>.Is.Anything)).Return(new List<JsonMedia>()
                                                                    {
                                                                        {new JsonMedia(media1)},
                                                                        {new JsonMedia(media2)}
                                                                    });

            return fakeMediaSetBuilder;
        }

        private IWebHelpers GetWebHelpers()
        {
            var webHelper = MockRepository.GenerateStub<IWebHelpers>();
            webHelper.Stub(x => x.RouteUrl(Arg<string>.Is.Anything, Arg<RouteValueDictionary>.Is.Anything)).Return("/Buy/125478/WhiteGold");
            return webHelper;
        }


    }
}