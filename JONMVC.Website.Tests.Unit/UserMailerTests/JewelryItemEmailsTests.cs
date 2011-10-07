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
    public class JewelryItemEmailsTests:EmailTestsBase
    {

        [Test]
        public void EmailRing_ShouldSetTheSubjectAndTheEamilToAddress()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            var model = fixture.CreateAnonymous<EmailRingEmailTemplateViewModel>();
            //Act
            var message = mailer.EmailRing(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            message.Subject.Should().Be(model.YourName + " wants you to checkout this jewel on JewelryONet.com");
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);

        }

        [Test]
        public void EmailRing_ShouldRenderTheRightView()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Equal("EmailRing"), Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            var model = fixture.CreateAnonymous<EmailRingEmailTemplateViewModel>();
            //Act
            var message = mailer.EmailRing(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert

            message.Subject.Should().Be(model.YourName + " wants you to checkout this jewel on JewelryONet.com");
            message.To.Should().HaveElementAt(0, Tests.SAMPLE_EMAIL_ADDRESS);

        }

        [Test]
        public void EmailRing_ShouldAddTheViewModel()
        {
            var mailer = MockRepository.GeneratePartialMock<UserMailer>();
            mailer.Stub(x => x.PopulateBody(Arg<MailMessage>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything));

            var model = fixture.CreateAnonymous<EmailRingEmailTemplateViewModel>();
            //Act
            mailer.EmailRing(Tests.SAMPLE_EMAIL_ADDRESS, model);
            //Assert
            mailer.ViewData.Model.Should().BeSameAs(model);


        }
    

    }
}