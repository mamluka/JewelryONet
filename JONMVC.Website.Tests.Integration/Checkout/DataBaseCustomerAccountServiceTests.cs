using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Tests.Unit.Checkout;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Integration.Checkout
{
    [TestFixture]
    public class DataBaseCustomerAccountServiceTests : CheckoutTestsBaseClass
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
        public void CreateCustomer_ShouldTryToWriteCustomerToDatabaseCheckUsingRoundTrip()
        {
            //Arrange
            var customer = new Customer()
                               {
                                   CountryID = 10,
                                   StateID = 5,
                                   Email = "DavidMZ",
                                   Firstname = "Firstname",
                                   Password = "123",
                                   Lastname = "LastName",
                                     
                               };

            var customerService = new DataBaseCustomerAccountService(mapper);
            //Act
            var result = customerService.CreateCustomer(customer);
            //Assert
            result.Should().Be(MembershipCreateStatus.Success);

            var customerRoundTrip = customerService.GetCustomerByEmail(customer.Email);

            customerRoundTrip.CountryID.Should().Be(customer.CountryID);
            customerRoundTrip.Email.Should().Be(customer.Email);
            customerRoundTrip.Firstname.Should().Be(customer.Firstname);
            customerRoundTrip.Lastname.Should().Be(customer.Lastname);
            customerRoundTrip.StateID.Should().Be(customer.StateID);

        }

    }
}