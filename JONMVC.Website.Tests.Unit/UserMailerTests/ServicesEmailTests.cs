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
        public void AskQuestion_ShouldSetTheRightEmailAddressAndSubject()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
           
            

            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            var message = mailer.AskQuestion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            message.Subject.Should().Be("Question from " + model.Name);
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);
        }

        [Test]
        public void AskQuestion_ShouldSetViewModelToTheModelPassed()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
          
            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            mailer.AskQuestion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            mailer.ViewData.Model.Should().NotBeNull();
        }


        [Test]
        public void AskQuestion_ShouldSetFromAddressCorrectly()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
         
            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            var message = mailer.AskQuestion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            message.From.Address.Should().Be(model.Email);
        }

        [Test]
        public void AskQuestion_ShouldRenderTheRightView()
        {
            //Arrange
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("AskQuestion"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
            var model = fixture.CreateAnonymous<AskQuestionEmailTemplateViewModel>();
            //Act
            mailer.AskQuestion(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert
            mailer.VerifyAllExpectations();
        }


    }
}