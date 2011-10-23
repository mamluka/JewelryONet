using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Tests.Unit;
using JONMVC.Website.Tests.Unit.Checkout;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;

namespace JONMVC.Website.Tests.Integration.Checkout
{
    [TestFixture]
    public class DataBaseCustomerAccountServiceTests : MapperAndFixtureBase
    {
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
                                   Phone = "12345"
                                     
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
            customerRoundTrip.Phone.Should().Be(customer.Phone);

        }

        [Test]
        public void UpdateExtendedCustomer_ShouldTryToUpdateCustomerToDatabaseCheckUsingRoundTrip()
        {
            //Arrange
            var address1 = fixture.Build<Address>()
                .With(x => x.Address1, "Address1")
                .With(x => x.City, "city1")

                .With(x => x.ZipCode, "zipcode1")
                .With(x=> x.Phone,"phonexxx1")
                .CreateAnonymous()
                ;
            
             var address2 = fixture.Build<Address>()
                .With(x => x.Address1, "Address2")
                .With(x => x.City, "city2")

                .With(x => x.ZipCode, "zipcode2")
                .With(x=> x.Phone,"phone2xxx")
                .CreateAnonymous()
                ;


            var extendedCustomer = fixture.Build<ExtendedCustomer>()
                .With(x => x.BillingAddress, address1)
                .With(x => x.ShippingAddress, address2)
                .With(x => x.Email, "email8")
                .With(x => x.Firstname, "firstname")
                .With(x => x.Lastname, "lastname")
                .CreateAnonymous()
                ;

            var customerService = new DataBaseCustomerAccountService(mapper);
            //Act
            var result = customerService.UpdateCustomer(extendedCustomer);
            //Assert
            result.Should().Be(MembershipCreateStatus.Success);

            var customerRoundTrip = customerService.GetExtendedCustomerByEmail(extendedCustomer.Email);

            customerRoundTrip.Email.Should().Be(extendedCustomer.Email);
            customerRoundTrip.Firstname.Should().Be(extendedCustomer.Firstname);
            customerRoundTrip.Lastname.Should().Be(extendedCustomer.Lastname);

            customerRoundTrip.ShippingAddress.Address1.Should().Be(extendedCustomer.ShippingAddress.Address1);
            customerRoundTrip.ShippingAddress.City.Should().Be(extendedCustomer.ShippingAddress.City);
            customerRoundTrip.ShippingAddress.CountryID.Should().Be(extendedCustomer.ShippingAddress.CountryID);

            customerRoundTrip.ShippingAddress.Phone.Should().Be(extendedCustomer.ShippingAddress.Phone);
            customerRoundTrip.ShippingAddress.StateID.Should().Be(extendedCustomer.ShippingAddress.StateID);
            customerRoundTrip.ShippingAddress.Phone.Should().Be(extendedCustomer.ShippingAddress.Phone);

            customerRoundTrip.BillingAddress.Address1.Should().Be(extendedCustomer.BillingAddress.Address1);
            customerRoundTrip.BillingAddress.City.Should().Be(extendedCustomer.BillingAddress.City);
            customerRoundTrip.BillingAddress.CountryID.Should().Be(extendedCustomer.BillingAddress.CountryID);

            customerRoundTrip.BillingAddress.Phone.Should().Be(extendedCustomer.BillingAddress.Phone);
            customerRoundTrip.BillingAddress.StateID.Should().Be(extendedCustomer.BillingAddress.StateID);
            customerRoundTrip.BillingAddress.Phone.Should().Be(extendedCustomer.BillingAddress.Phone);

        }

    }
}