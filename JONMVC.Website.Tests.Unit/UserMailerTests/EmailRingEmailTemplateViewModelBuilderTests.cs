using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Mailers;
using JONMVC.Website.Tests.Unit.Fakes;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.UserMailerTests
{
    [TestFixture]
    public class EmailRingEmailTemplateViewModelBuilderTests:EmailTestsBase
    {

        [Test]
        public void Build_ShouldMapTheModelToTheTemplateViewModel()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());
            var model = fixture.Build<EmailRingModel>().With(x=> x.ID,Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID).CreateAnonymous();

            var builder = new EmailRingEmailTemplateViewModelBuilder(model,jewelRepository,mapper);
            //Act
            var template = builder.Build();
            //Assert
            template.YourName.Should().Be(model.YourName);
            template.YourEmail.Should().Be(model.YourEmail);
            template.ID.Should().Be(model.ID.ToString());
            template.Message.Should().Be(model.Message);
            template.FriendEmail.Should().Be(model.FriendEmail);
            template.FriendName.Should().Be(model.FriendName);

        }

        [Test]
        public void Build_ShouldMapTheJewelDetailsToTheViewModel()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager()); ;

            var model = fixture.Build<EmailRingModel>().With(x=> x.ID , Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID).CreateAnonymous();

            var builder = new EmailRingEmailTemplateViewModelBuilder(model, jewelRepository, mapper);
            //Act
            var template = builder.Build();
            //Assert
            var jewel = FakeFactory.FirstJewelInRepository;

            template.Description.Should().Be(jewel.Title);
            template.ItemNumber.Should().Be(jewel.ItemNumber);
            template.Price.Should().Be(Tests.AsMoney(jewel.Price));
            template.Icon.Should().Be(jewel.Media.IconURLForWebDisplay);
            template.MediaSet.Should().Be(jewel.Media.MediaSet);
            


        }

        [Test]
        [ExpectedException]
        public void Build_ShouldThrowAnExceptionIfTheJewelIdDoesntExists()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager()); ;

            var doesntExists = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID + 1000;
            var model = fixture.Build<EmailRingModel>().With(x => x.ID, doesntExists).CreateAnonymous();

            var builder = new EmailRingEmailTemplateViewModelBuilder(model, jewelRepository, mapper);
            //Act
            builder.Build();
        }


    }
}