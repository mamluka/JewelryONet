using System.Text;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.ViewModelUtils
{
    [TestFixture]
    public class UsingTitlePathBarResolverTests
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
        public void Generate_ShouldReturnTheCurrentTitleAsTheLinkTextAndAnEmptyLinkBecauseThisIsRoot()
        {
            //Arrange
            var resolver = new UsingDynamicTitlePathBarResolver();

            var viewModel = fixture.CreateAnonymous<PageViewModelBase>();
            //Act
            var list = resolver.GeneratePathBarDictionary(viewModel);
            //Assert
            list[0].Key.Should().Be(viewModel.PageTitle);
            list[0].Value.Should().Be("");
        }


    }
}