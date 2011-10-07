using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Extentions
{
    [TestFixture]
    public class MVCExtentionsTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void RingSizes_ShouldReturnASetOfRingSizes()
        {
            //Arrange
          
            //Act

            //Assert

        }

        public static HtmlHelper CreateHtmlHelper(ViewDataDictionary viewData)
        {
            var mocks = new MockRepository();

            var controllerContext = mocks.DynamicMock<ControllerContext>(
                mocks.DynamicMock<HttpContextBase>(),
                new RouteData(),
                mocks.DynamicMock<ControllerBase>());

            var mockViewContext = mocks.DynamicMock<ViewContext>(
                controllerContext,
                mocks.DynamicMock<IView>(),
                viewData,
                new TempDataDictionary());

            var mockViewDataContainer = mocks.DynamicMock<IViewDataContainer>();

            mockViewDataContainer.Expect(v => v.ViewData).Return(viewData);

            return new HtmlHelper(mockViewContext, mockViewDataContainer);
        }

    }
}