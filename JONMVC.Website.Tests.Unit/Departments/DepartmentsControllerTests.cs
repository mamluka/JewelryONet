using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;


namespace JONMVC.Website.Tests.Unit.Departments
{
    [TestFixture]
    public class DepartmentsControllerTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Diamonds_ShouldRedirectToAnEmptyView()
        {
            //Arrange
            var control = CreateDefaultDepartmentsController();
            //Act
            var resultview = control.Diamonds();
            //Assert
            resultview.AssertViewRendered().WithViewData<EmptyViewModel>();

        }


        [Test]
        public void DiamondStuds_ShouldRedirectToAnEmptyView()
        {
            //Arrange
            var control = CreateDefaultDepartmentsController();
            //Act
            var resultview = control.DiamondStuds();
            //Assert
            resultview.AssertViewRendered().WithViewData<EmptyViewModel>();

        }


        [Test]
        public void EngagementRings_ShouldRedirectToAnEmptyView()
        {
            //Arrange
            var control = CreateDefaultDepartmentsController();
            //Act
            var resultview = control.EngagementRings();
            //Assert
            resultview.AssertViewRendered().WithViewData<EmptyViewModel>();

        }

        [Test]
        public void WeddingAndAnniversary_ShouldRedirectToAnEmptyView()
        {
            //Arrange
            var control = CreateDefaultDepartmentsController();
            //Act
            var resultview = control.WeddingAndAnniversary();
            //Assert
            resultview.AssertViewRendered().WithViewData<EmptyViewModel>();

        }

        [Test]
        public void DesignerJewelry_ShouldRedirectToAnEmptyView()
        {
            //Arrange
            var control = CreateDefaultDepartmentsController();
            //Act
            var resultview = control.DesignerJewelry();
            //Assert
            resultview.AssertViewRendered().WithViewData<EmptyViewModel>();

        }

        [Test]
        public void GiftIdeas_ShouldRedirectToAnEmptyView()
        {
            //Arrange
            var control = CreateDefaultDepartmentsController();
            //Act
            var resultview = control.GiftIdeas();
            //Assert
            resultview.AssertViewRendered().WithViewData<EmptyViewModel>();

        }


        private static DepartmentsController CreateDefaultDepartmentsController()
        {
            var pathbarGenerator = MockRepository.GenerateStub<IPathBarGenerator>();
            var control = new DepartmentsController(pathbarGenerator);
            return control;
        }

    }
}