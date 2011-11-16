using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using JONMVC.Website.Controllers;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;
using Rhino.Mocks.Exceptions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Services
{
    [TestFixture]
    public class ServicesControllerTests:CheckoutTestsBase
    {
        [Test]
        public void WishList_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.Wishlist();
            //Assert
            result.AssertViewRendered().WithViewData<WishListViewModel>();
        }

       

        [Test]
        public void Policy_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.Policy();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void AskQuestion_ShouldSendTheEmailToTheAdmin()
        {
            //Arrange
            var wishListBuilder = MockRepository.GenerateStub<IViewModelBuilder>();
            wishListBuilder.Stub(x => x.Build()).Return(new WishListViewModel());
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var settingManager = new FakeSettingManager();
            var adminEmailAddress = settingManager.AdminEmail();

            var userMailer = MockRepository.GenerateMock<IUserMailer>();
            userMailer.Expect(
                y =>
               y.AskQuestionAdminVersion(Arg<string>.Is.Equal(adminEmailAddress),
                              Arg<AskQuestionEmailTemplateViewModel>.Matches(
                                  x =>
                                  x.Email == "email" && x.Name == "name" && x.Phone == "phone" &&
                                  x.Question == "question"))).Repeat.Once();

            var conrtoller = new ServicesController(wishListBuilder, pathBarGenerator, userMailer, settingManager);
            
            //Act
            var result = conrtoller.AskQuestion("name","email","phone","question");
            //Assert
            userMailer.VerifyAllExpectations();
        }

        [Test]
        public void AskQuestion_ShouldSendTheEmailToTheCustomer()
        {
            //Arrange
            var wishListBuilder = MockRepository.GenerateStub<IViewModelBuilder>();
            wishListBuilder.Stub(x => x.Build()).Return(new WishListViewModel());
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var settingManager = new FakeSettingManager();

            var userMailer = MockRepository.GenerateMock<IUserMailer>();
            userMailer.Expect(
                y =>
               y.AskQuestionCustomerVersion(Arg<string>.Is.Equal("email"),
                              Arg<AskQuestionEmailTemplateViewModel>.Matches(
                                  x =>
                                  x.Email == "email" && x.Name == "name" && x.Phone == "phone" &&
                                  x.Question == "question"))).Repeat.Once();

            var conrtoller = new ServicesController(wishListBuilder, pathBarGenerator, userMailer, settingManager);

            //Act
            var result = conrtoller.AskQuestion("name", "email", "phone", "question");
            //Assert
            userMailer.VerifyAllExpectations();
        }

        [Test]
        public void AskQuestion_ShouldReturnAJsonWithErrorIfMailerThrowsException()
            
        {
            //Arrange
            var wishListBuilder = MockRepository.GenerateStub<IViewModelBuilder>();
            wishListBuilder.Stub(x => x.Build()).Return(new WishListViewModel());
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var settingManager = new FakeSettingManager();

            var userMailer = MockRepository.GenerateStub<IUserMailer>();
            userMailer.Stub(
                y =>
                y.AskQuestionAdminVersion(Arg<string>.Is.Anything,
                              Arg<AskQuestionEmailTemplateViewModel>.Is.Anything)).Throw(new Exception());
                             

            var conrtoller = new ServicesController(wishListBuilder, pathBarGenerator, userMailer, settingManager);

            //Act
            var result = conrtoller.AskQuestion("name", "email", "phone", "question") as JsonResult;
            //Assert
            var actual = result.Data as OporationWithoutReturnValueJsonModel;
            actual.HasError.Should().BeTrue();

        }

        [Test]
        public void Feedbacks_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.Feedbacks();
            //Assert
            result.AssertViewRendered().WithViewData<FeedBackskViewModel>();
        }

        [Test]
        public void Affiliate_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.Affiliate();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void AboutUs_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.AboutUs();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void CustomOrder_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.CustomOrders();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void ConflictFreeDiamonds_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.ConflictFreeDiamonds();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void Engraving_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            //Act
            var result = conrtoller.Engraving();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void Search_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var conrtoller = CreateDefaultServicesController();
            var term = fixture.CreateAnonymous("search");
            //Act
            var result = conrtoller.Search(term);
            //Assert
            result.AssertViewRendered().WithViewData<SearchViewModel>();
        }


        private static ServicesController CreateDefaultServicesController()
        {
            var wishListBuilder = MockRepository.GenerateStub<IViewModelBuilder>();
            wishListBuilder.Stub(x => x.Build()).Return(new WishListViewModel());
            var pathBarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();

            var userMailer = MockRepository.GenerateStub<IUserMailer>();

            var settingManager = new FakeSettingManager();

            var conrtoller = new ServicesController(wishListBuilder, pathBarGenerator, userMailer, settingManager);
            return conrtoller;
        }
    }
}