using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models;
using Mvc.Mailer;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.UserMailerTests
{
    [TestFixture]
    public class ServicesEmailTests : EmailTestsBase
    {
        [Test]
        public void AskQuestionAdminVersion_ShouldSetTheRightEmailAddressAndSubject()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
           
            

            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            var message = mailer.AskQuestionAdminVersion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            message.Subject.Should().Be("Question from " + model.Name);
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);
        }

        [Test]
        public void AskQuestionAdminVersion_ShouldSetViewModelToTheModelPassed()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
          
            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            mailer.AskQuestionAdminVersion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            mailer.ViewData.Model.Should().NotBeNull();
        }


        

        [Test]
        public void AskQuestionAdminVersion_ShouldRenderTheRightView()
        {
            //Arrange
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("AskQuestionAdmin"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            mailer.AskQuestionAdminVersion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert
            mailer.VerifyAllExpectations();
        }

        [Test]
        public void AskQuestionCustomerVersion_ShouldSetTheRightEmailAddressAndSubject()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));



            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            var message = mailer.AskQuestionCustomerVersion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            message.Subject.Should().Be("JewelryONet.com has recieved your question");
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);
        }

        [Test]
        public void AskQuestionCustomerVersion_ShouldSetViewModelToTheModelPassed()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            mailer.AskQuestionCustomerVersion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            mailer.ViewData.Model.Should().NotBeNull();
        }




        [Test]
        public void AskQuestionCustomerVersion_ShouldRenderTheRightView()
        {
            //Arrange
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("AskQuestionCustomer"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            mailer.AskQuestionCustomerVersion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert
            mailer.VerifyAllExpectations();
        }


    }
}