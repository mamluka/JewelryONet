using System.Collections.Generic;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;
namespace JONMVC.Website.Tests.Unit.Tabs
{
    [TestFixture]
    public class SpecialOffersBannervViewModelBuilderTests : MapperAndFixtureBase
    {
        [Test]
        public void Build_ShouldCallTheRepositoryWithTheCorreclyDynamicSQLExpression()
        {
            //Arrange
            var jewelRepostory = MockRepository.GenerateMock<IJewelRepository>();
            jewelRepostory.Expect(
                x => x.GetJewelsByDynamicSQL(Arg<DynamicSQLWhereObject>.Matches(a => a.Pattern == "onspecial = true"))).
                Repeat.Once();

            var builder = new SpecialOffersBannervViewModelBuilder(jewelRepostory,mapper);
            //Act
            builder.Build();
            //Assert
            jewelRepostory.VerifyAllExpectations();
        }

        [Test]
        public void Build_ShouldMapTheViewModelFromJEwelCorrectly()
        {
            //Arrange
            var jewel = fixture.CreateAnonymous<Jewel>();

            var jewelRepostory = MockRepository.GenerateStub<IJewelRepository>();
            jewelRepostory.Stub(
                x => x.GetJewelsByDynamicSQL(Arg<DynamicSQLWhereObject>.Matches(a => a.Pattern == "onspecial = true"))).
                Return(new List<Jewel>() {jewel});

            var builder = new SpecialOffersBannervViewModelBuilder(jewelRepostory, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Icon.Should().Be(jewel.Media.HiResURLForWebDisplay);
            viewModel.ID.Should().Be(jewel.ID.ToString());
            viewModel.Title.Should().Be(jewel.Title);

        }


    }
}