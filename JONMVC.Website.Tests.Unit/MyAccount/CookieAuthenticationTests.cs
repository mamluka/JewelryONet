using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Tests.Unit.Fakes;
using MvcContrib.TestHelper.Fakes;
using NUnit.Framework;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.MyAccount
{
    [TestFixture]
    public class CookieAuthenticationTests
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
        public void Signin_ShouldCreateTheCookieTicket()
        {
            //Arrange
            var fakeHttpContext = FakeFactory.FakeHttpContext();
            

            var cookieAuth = new CookieAuthentication(fakeHttpContext);

            var email = fixture.CreateAnonymous<string>();
            var customerData = fixture.CreateAnonymous<Customer>();
            //Act
            cookieAuth.Signin(email,customerData);
            //Assert

            fakeHttpContext.Response.Cookies[FormsAuthentication.FormsCookieName].Should().NotBeNull();

        }

        [Test]
        public void IsSignedIn_ShouldReturnFalseWhenNotSignedIn()
        {
            //Arrange
            var fakeHttpContext = FakeFactory.FakeHttpContextWithCustomerAuthenticationSetTo(false);
            
            var cookieAuth = new CookieAuthentication(fakeHttpContext);

            var email = fixture.CreateAnonymous<string>();
            var customerData = fixture.CreateAnonymous<Customer>();
            //Act
            var signedin = cookieAuth.IsSignedIn();
            //Assert

            signedin.Should().BeFalse();

        }

        [Test]
        public void IsSignedIn_ShouldReturnTrueWhenTheCustomerIsSignedIn()
        {
            //Arrange
            var fakeHttpContext = FakeFactory.FakeHttpContextWithCustomerAuthenticationSetTo(true);

            var cookieAuth = new CookieAuthentication(fakeHttpContext);

            var email = fixture.CreateAnonymous<string>();
            var customerData = fixture.CreateAnonymous<Customer>();
            //Act
            var signedin = cookieAuth.IsSignedIn();
            //Assert

            signedin.Should().BeTrue();

        }

        [Test]
        public void IsSignedIn_ShouldReturnTheCustomerDataFromCookieWhenCustomerIsSignedIn()
        {
            var fakeHttpContext = FakeFactory.FakeHttpContext();

            var cookieAuth = new CookieAuthentication(fakeHttpContext);

            var email = fixture.CreateAnonymous<string>();
            var customerData = fixture.CreateAnonymous<Customer>();
            //Act
            cookieAuth.Signin(email, customerData);

            var returnCustomerData = cookieAuth.CustomerData;

            //Assert

            returnCustomerData.Country.Should().Be(customerData.Country);
            returnCustomerData.CountryID.Should().Be(customerData.CountryID);
            returnCustomerData.Email.Should().Be(customerData.Email);
            returnCustomerData.Firstname.Should().Be(customerData.Firstname);
            returnCustomerData.State.Should().Be(customerData.State);
            returnCustomerData.StateID.Should().Be(customerData.StateID);
            returnCustomerData.Lastname.Should().Be(customerData.Lastname);



        }

        [Test]
        public void Signin_ShouldCreateTheCookieTicketWithTheRightUserData()
        {
            //Arrange
            var fakeHttpContext = FakeFactory.FakeHttpContext();

            var cookieAuth = new CookieAuthentication(fakeHttpContext);

            var email = fixture.CreateAnonymous<string>();
            var customerData = fixture.CreateAnonymous<Customer>();
            //Act
            cookieAuth.Signin(email, customerData);
            //Assert

            var authCookie = fakeHttpContext.Response.Cookies[FormsAuthentication.FormsCookieName];

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var decryptedCustomerData = JsonConvert.DeserializeObject<Customer>(authTicket.UserData);

            decryptedCustomerData.Country.Should().Be(customerData.Country);
            decryptedCustomerData.State.Should().Be(customerData.State);
            decryptedCustomerData.Lastname.Should().Be(customerData.Lastname);
            decryptedCustomerData.Email.Should().Be(customerData.Email);
            decryptedCustomerData.Firstname.Should().Be(customerData.Firstname);
       


        }

        [Test]
        public void Signout_ShouldRemoveTheCookie()
        {
            //Arrange
            var fakeHttpContext = FakeFactory.FakeHttpContext();

            var cookieAuth = new CookieAuthentication(fakeHttpContext);

            var email = fixture.CreateAnonymous<string>();
            var customerData = fixture.CreateAnonymous<Customer>();
            //Act
            cookieAuth.Signin(email, customerData);
            cookieAuth.Signout();
            //Assert
            fakeHttpContext.Response.Cookies[FormsAuthentication.FormsCookieName].Should().BeNull();
            fakeHttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Should().BeNull();
            

        }





    }
}