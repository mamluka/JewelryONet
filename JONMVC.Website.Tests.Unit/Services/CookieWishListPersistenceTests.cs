using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Tests.Unit.Fakes;
using MvcContrib.TestHelper.Fakes;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Services
{
    [TestFixture]
    public class CookieWishListPersistenceTests : CheckoutTestsBase
    {
        [Test]
        public void GetItemsInWishList_ShouldReturnIDsLikeInTheFakeCookieCollection()
        {
            //Arrange

             var fakeContext = CreateJONCookieInFakeHttpContextWith10ItemsInside();


            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            //Act

            var list = cookiePersistence.GetItemsOnWishList();

            //Assert

            list.Should().HaveCount(10);

        }

       
        [Test]
        public void SaveID_ShouldSaveTheIDtoTheCookieIfCookieExists()
        {
            //Arrange
            var fakeContext = CreateJONCookieInFakeHttpContextWith10ItemsInside();


            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            var newID = fixture.CreateAnonymous<int>();
            //Act
            cookiePersistence.SaveID(newID);
            
            //Assert
            
            var list = cookiePersistence.GetItemsOnWishList();
            list.Should().HaveCount(11);
        }

        [Test]
        public void SaveID_ShouldNotSaveTheSameIdTwice()
        {
            //Arrange
            var fakeContext = CreateJONCookieInFakeHttpContextWith10ItemsInside();


            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            var newID = fixture.CreateAnonymous<int>();
            //Act
            cookiePersistence.SaveID(newID);
            cookiePersistence.SaveID(newID);
            cookiePersistence.SaveID(newID);

            //Assert

            var list = cookiePersistence.GetItemsOnWishList();
            list.Should().HaveCount(11);
        }

        [Test]
        public void SaveID_ShouldSaveTheIDtoTheCookieAndCreateTheCookieIfCookieDoesntExists()
        {
            //Arrange
            var fakeContext = CreateFakeHttpContextWithoutTheCookie();

            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            var newID = fixture.CreateAnonymous<int>();
            //Act
            cookiePersistence.SaveID(newID);

            //Assert
            fakeContext.Response.Cookies["JON"].Should().NotBeNull();
        }

        [Test]
        public void RemoveID_ShouldRemoveIDFromTheCookieList()
        {
            //Arrange
            var fakeContext = CreateJONCookieInFakeHttpContextWith10ItemsInside();

            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            var jewelID = 7;
            //Act
            cookiePersistence.RemoveID(jewelID);

            //Assert
            var items = cookiePersistence.GetItemsOnWishList();
            items.Should().NotContain(7);

        }

        [Test]
        public void RemoveID_ShouldDoNothingIfCookieDoesntExistAndWeTryToDelete()
        {
            //Arrange
            var fakeContext = CreateFakeHttpContextWithoutTheCookie();

            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            var jewelID = 7;
            //Act
            cookiePersistence.RemoveID(jewelID);

            //Assert
            fakeContext.Response.Cookies["JON"].Should().BeNull();
        }

        [Test]
        public void Clear_ShouldClearAllTheItemsFromTheList()
        {
            //Arrange
            var fakeContext = CreateJONCookieInFakeHttpContextWith10ItemsInside();

            var cookiePersistence = new CookieWishListPersistence(fakeContext);

            //Act
            cookiePersistence.ClearWishList();
            //Assert

            fakeContext.Response.Cookies["JON"].Expires.Should().BeBefore(DateTime.Now);


        }

        private FakeHttpContext CreateFakeHttpContextWithoutTheCookie()
        {
           return  FakeFactory.FakeHttpContext();
        }

        private FakeHttpContext CreateJONCookieInFakeHttpContextWith10ItemsInside()
        {
            var cookie = new HttpCookie("JON");
            cookie["wishlistitems"] = String.Join(",", fixture.CreateMany<int>(10));

            var cookieColletion = new HttpCookieCollection();
            cookieColletion.Add(cookie);


            var fakeContext = new FakeHttpContext("/");
            var fakeRequest = new FakeHttpRequest("/", "get", new Uri("http://localhost"), new Uri("http://localhost"),
                                                  new NameValueCollection(), new NameValueCollection(), cookieColletion);

            var fakeResponse = new FakeHttpResponseForCookieHandeling();
            fakeContext.SetResponse(fakeResponse);
            fakeContext.SetRequest(fakeRequest);
            return fakeContext;
        }
    }
}