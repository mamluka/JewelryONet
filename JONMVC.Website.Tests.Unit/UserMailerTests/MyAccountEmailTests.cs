using System.Collections.Generic;
using System.Net.Mail;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.UserMailerTests
{
    [TestFixture]
    public class MyAccountEmailTests:EmailTestsBase
    {

        [Test]
        public void RecoverPassword_ShouldSetTheRightEmailAndSubject()
        {
            //Arrange
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            var message = mailer.RecoverPassword(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING,Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert

            message.Subject.Should().Be("Your account password for JewelryONet.com");
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);

        }

        [Test]
        public void RecoverPassword_ShouldSetTheViewBagCorrectlyWithParameters()
        {
            //Arrange
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.RecoverPassword(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING,Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert
            string email = mailer.ViewBag.Email;
            string password = mailer.ViewBag.LostPassword;
            string name = mailer.ViewBag.Name;

            email.Should().Be(Tests.SAMPLE_EMAIL_ADDRESS);
            password.Should().Be(Tests.SAMPLE_PASSWORD);
            name.Should().Be(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING);


        }

        [Test]
        public void RecoverPassword_ShouldRenderTheRightView()
        {
            //Arrange
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("RecoverPassword"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.RecoverPassword(Tests.STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING,Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert
            mailer.VerifyAllExpectations();

        }

        [Test]
        public void NewCustomer_ShouldSetTheRightEmailAndSubject()
        {
            //Arrange
            var customer = fixture.Build<Customer>().With(x=> x.Email,Tests.SAMPLE_EMAIL_ADDRESS).CreateAnonymous();

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            var message = mailer.NewCustomer(customer);
            //Assert

            message.Subject.Should().Be("Thank you for registering to JewelryONet");
            message.To.Should().HaveElementAt(0, customer.Email);

        }

        [Test]
        public void NewCustomer_ShouldSetTheViewBagCorrectlyWithParameters()
        {
            //Arrange
            var customer = fixture.Build<Customer>().With(x => x.Email, Tests.SAMPLE_EMAIL_ADDRESS).CreateAnonymous();

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.NewCustomer(customer);
            //Assert
            string name = mailer.ViewBag.Name;
            string email = mailer.ViewBag.Email;
            string password = mailer.ViewBag.Password;

            name.Should().Be(customer.FirstName + " " + customer.LastName);
            email.Should().Be(customer.Email);
            password.Should().Be(customer.Password);


        }

        [Test]
        public void NewCustomer_ShouldRenderTheRightView()
        {
            //Arrange
            var customer = fixture.Build<Customer>().With(x => x.Email, Tests.SAMPLE_EMAIL_ADDRESS).CreateAnonymous();

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("NewCustomer"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            //Act
            mailer.NewCustomer(customer);
            //Assert
            mailer.VerifyAllExpectations();

        }


    }
}