using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Controllers;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;


namespace JONMVC.Website.Tests.Unit.Education
{
    [TestFixture]
    public class EducationControllerTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Index_ShouldRenderTheRightViewModel()
        {
            //Arrange
            var controller = new EducationController();
            //Act
            var result = controller.Index();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void Diamond_ShouldRenderTheRightViewModel()
        {
            //Arrange
            var controller = new EducationController();
            //Act
            var result = controller.Diamond();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }


        [Test]
        public void Gemstone_ShouldRenderTheRightViewModel()
        {
            //Arrange
            var controller = new EducationController();
            //Act
            var result = controller.Gemstone();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }


    }
}