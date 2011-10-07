using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.UserMailerTests
{
    [TestFixture]
    public class CheckoutEmailTests:EmailTestsBase
    {

        [Test]
        public void OrderConfirmation_ShouldSetTheRightEmailAndSubject()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            var model = fixture.CreateAnonymous<OrderConfirmationEmailTemplateViewModel>();
         
            //Act
            var message = mailer.OrderConfirmation(Tests.SAMPLE_EMAIL_ADDRESS,model);
            //Assert

            message.Subject.Should().Be("Confirm order number:" + model.OrderNumber);
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);

        }

        [Test]
        public void OrderConfirmation_ShouldSetTheViewModelCorrectly()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
            var model = new OrderConfirmationEmailTemplateViewModel();
            //Act
            mailer.OrderConfirmation(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert
            mailer.ViewData.Model.GetType().ToString().Should().Be(model.GetType().ToString());


        }



        [Test]
        public void BestOfferCustomer_ShouldRenderTheRightView()
        {
            //Arrange

            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Expect(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("OrderConfirmation"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));
            var model = new OrderConfirmationEmailTemplateViewModel();
            //Act
            mailer.OrderConfirmation(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert
            mailer.VerifyAllExpectations();
            
        }


    }
}