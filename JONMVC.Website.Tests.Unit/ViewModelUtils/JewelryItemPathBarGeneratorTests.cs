using System;
using System.Collections.Generic;
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
    public class JewelryItemPathBarGeneratorTests
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
        public void Generate_ShouldGenerateANonLinkFromTitle()
        {
            //Arrange
            var resolver = new JewelryItemPathBarGenerator();

            var viewModel = fixture.CreateAnonymous<JewelryItemViewModel>();
            //Act
            var list = resolver.GeneratePathBarDictionary(viewModel);
            //Assert
            list[0].Key.Should().Be(viewModel.Title);
            list[0].Value.Should().Be("");

        }

    }

    public class JewelryItemPathBarGenerator:PathBarResolver<JewelryItemViewModel>
    {
        public override List<KeyValuePair<string, string>> GeneratePathBarDictionary(JewelryItemViewModel model)
        {
            try
            {
                var list = new List<KeyValuePair<string, string>>();

                var currentTabNonLink = new KeyValuePair<string, string>(model.Title, "");

                list.Add(currentTabNonLink);

                return list;
            }
            catch (Exception ex)
            {

                throw new Exception("When try to create the path bar for item with id: " + model.ID + " an error occured\r\n" + ex.Message);
            }
        }
    }
}