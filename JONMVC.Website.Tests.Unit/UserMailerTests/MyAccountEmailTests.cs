using System.Collections.Generic;
using System.Net.Mail;
using JONMVC.Website.Mailers;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.UserMailerTests
{
    [TestFixture]
    public class MyAccountEmailTests:EmailTestsBase
    {

        [Test]
        public void RecoverPassword_ShouldSetTheRightEmailAndSubject()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            var message = mailer.RecoverPassword(Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert

            message.Subject.Should().Be("Your account password for JewelryONet.com");
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);

        }

        [Test]
        public void RecoverPassword_ShouldSetTheViewBagCorrectlyWithParameters()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.RecoverPassword(Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert
            string email = mailer.ViewBag.Email;
            string password = mailer.ViewBag.LostPassword;

            email.Should().Be(Tests.SAMPLE_EMAIL_ADDRESS);
            password.Should().Be(Tests.SAMPLE_PASSWORD);


        }

        [Test]
        public void RecoverPassword_ShouldRenderTheRightView()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("RecoverPassword"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.RecoverPassword(Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert
            mailer.VerifyAllExpectations();

        }


    }
}