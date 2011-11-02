using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using Mvc.Mailer;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    [TestFixture]
    public class BestOfferTests
    {
        private IMappingEngine mapper;
        private Fixture fixture;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        /// 
        [TestFixtureSetUp]
        public void InitializeFixture()
        {
            //Bootstrapper.Excluding.Assembly("JONMVC.Core.Configurations").With.AutoMapper().Start();
            MapsContainer.CreateAutomapperMaps();

            mapper = Mapper.Engine;
        }
        [SetUp]
        public void Initialize()
        {
            fixture = new Fixture();
            MailerBase.IsTestModeEnabled = true;
        }

        [Test]
        public void EmailToAdmin_ShouldCallTheUserMailerWithTheRightMethodToSendTheEmail()
        {
            //Arrange
            
            
            var mailer = MockRepository.GenerateStrictMock<IUserMailer>();
            mailer.Expect(x => x.BestOfferAdmin(Arg<string>.Is.Anything, Arg<BestOfferEmailTemplateViewModel>.Is.Anything));

            var bestOffer = CreateDefaultBestOffer(mailer);

            var bestOfferViewModel = DefaultBestOfferViewModelWithRealJewel();
            //Act
            bestOffer.EmailToAdmin(bestOfferViewModel);
            //Assert
            mailer.VerifyAllExpectations();
        }

       
        [Test]
        public void EmailToAdmin_ShouldCallTheUserMailerWithTheCorrectEmail()
        {
            //Arrange
            var currectEmailForAdmin = new FakeSettingManager().AdminEmail();

            var mailer = MockRepository.GenerateStrictMock<IUserMailer>();
            mailer.Expect(x => x.BestOfferAdmin(Arg<string>.Is.Equal(currectEmailForAdmin), Arg<BestOfferEmailTemplateViewModel>.Is.Anything));

            var bestOfferViewModel = DefaultBestOfferViewModelWithRealJewel();

            var bestOffer = CreateDefaultBestOffer(mailer);
            //Act
            bestOffer.EmailToAdmin(bestOfferViewModel);
            //Assert
            mailer.VerifyAllExpectations();
        }

        [Test]
        public void CreateEmailTemplateModel_ShouldCreateTheModelForTheEmailUsingTheGivenDataFromTheViewModel()
        {
            //Arrange



            var mailer = MockRepository.GenerateStub<IUserMailer>();

            var todayDate = fixture.CreateAnonymous<DateTime>();

            var bestOffer = CreateDefaultBestOffer(mailer);

            var bestOfferViewModel = DefaultBestOfferViewModelWithRealJewel();

            bestOffer.SetTodayString(todayDate);
            //Act
            var template = bestOffer.CreateEmailModel(bestOfferViewModel);
            //Assert
            template.Email.Should().Be(Tests.SAMPLE_EMAIL_ADDRESS);
            template.Description = "title";
            template.ItemNumber = "0101-15421";
            template.TruePrice.Should().Be("$10,000");
            template.OfferDate.Should().Be(todayDate.ToShortDateString());
            template.OfferPrice.Should().Be("$2,000");
            template.OfferNumber.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID.ToString());

        }

        private BestOffer CreateDefaultBestOffer(IUserMailer mailer)
        {
            var settingManager = new FakeSettingManager();
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var bestOffer = new BestOffer(mailer, settingManager, jewelRepository, mapper);
            return bestOffer;
        }


        private static BestOfferViewModel DefaultBestOfferViewModelWithRealJewel()
        {
            var bestOfferViewModel = new BestOfferViewModel()
                                         {
                                             JewelID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                             OfferEmail = Tests.SAMPLE_EMAIL_ADDRESS,
                                             OfferPrice = 2000
                                         };
            return bestOfferViewModel;
        }
    }
}