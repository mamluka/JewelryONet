using System;
using System.Text;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Utils;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.ViewModelUtils
{
    [TestFixture]
    public class PathBarGeneratorTests
    {
        private Fixture fixture;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            fixture = new Fixture();
        }

        [Test]
        public void GenerateUsingSingleTitle_ShouldReturnAPathBarWithSingleElementSameAsTitle()
        {
            //Arrange
            var webHelpers = MockRepository.GenerateStub<IWebHelpers>();
            var pathbarGenerator = new PathBarGenerator(webHelpers);

            var title = fixture.CreateAnonymous<string>();
            //Act

            var pathbar = pathbarGenerator.GenerateUsingSingleTitle<UsingTitlePathBarResolver>(title);
            //Assert

            pathbar[0].Key.Should().Be(title);
            pathbar[0].Value.Should().Be("");
        }

    }
}