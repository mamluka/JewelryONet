using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using JONMVC.Website.Models;
using Mvc.Mailer;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using JONMVC.Website.Mailers;


namespace JONMVC.Website.Tests.Unit.UserMailerTests
{
    [TestFixture]
    public class BestOfferEmailsTests:EmailTestsBase
    {

        [Test]
        public void BestOfferCustomer_ShouldSetTheRightEmailAddressAndSubject()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
         
            //Act
            var message = mailer.BestOfferCustomer(Tests.SAMPLE_EMAIL_ADDRESS,new BestOfferEmailTemplateViewModel());
            //Assert

            message.Subject.Should().Be("Best offer confirmation from JewelryONet.com");
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);
        }

        [Test]
        public void BestOfferCustomer_ShouldSetViewModelToTheModelPassed()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
         
            //Act
            mailer.BestOfferCustomer(Tests.SAMPLE_EMAIL_ADDRESS, new BestOfferEmailTemplateViewModel());
            //Assert

            mailer.ViewData.Model.Should().NotBeNull();
        }

        [Test]
        public void BestOfferCustomer_ShouldRenderTheRightViewModel()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("BestOfferCustomer"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.BestOfferCustomer(Tests.SAMPLE_EMAIL_ADDRESS, new BestOfferEmailTemplateViewModel());
            //Assert

            mailer.ViewData.Model.Should().NotBeNull();
        }

     

    }
}