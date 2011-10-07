using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Controllers;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;

namespace JONMVC.Website.Tests.Unit.VIP
{
    [TestFixture]
    public class VIPControllerTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Club_ShouldRenderTheRightViewModel()
        {
            //Arrange
            var controller = new VIPController();
            //Act
            var result = controller.Club();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }


    }
}