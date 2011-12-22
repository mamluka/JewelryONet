using System;
using System.Collections.Generic;
using System.Text;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Admin;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Admin
{
    [TestFixture]
    public class AdminControllerTests:MapperAndFixtureBase
    {

        [Test]
        public void Index_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var csvParser = MockRepository.GenerateStub<ICSVParser>();
            var databasePersistence = MockRepository.GenerateStub<IDatabasePersistence>();
            var controller = new AdminController(csvParser,databasePersistence);
            //Act
            var viewModel = controller.Index();
            //Assert
            viewModel.AssertViewRendered().WithViewData<EmptyViewModel>();

        }

        [Test]
        public void UpdateDiamonds_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var csvParser = MockRepository.GenerateStub<ICSVParser>();
            var databasePersistence = MockRepository.GenerateStub<IDatabasePersistence>();
            var controller = new AdminController(csvParser, databasePersistence);
            //Act
            var viewModel = controller.UpdateDiamonds();
            //Assert
            viewModel.AssertViewRendered().WithViewData<UpdateDiamondsModel>();

        }




    }
}