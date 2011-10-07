using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Controllers;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;


namespace JONMVC.Website.Tests.Unit.Gifts
{
    [TestFixture]
    public class GiftsControllerTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void BirthDayGifts_ShouldRenderTheNewViewModel()
        {
            //Arrange
            var controller = new GiftsController();
            //Act
            var result = controller.BirthDayGifts();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }


    }
}