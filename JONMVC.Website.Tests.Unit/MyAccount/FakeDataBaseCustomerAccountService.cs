using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.MyAccount
{
    public class FakeDataBaseCustomerAccountService:ICustomerAccountService
    {
        private readonly IMappingEngine mapper;

        private static Fixture fixture = new Fixture();

        private readonly List<usr_CUSTOMERS> dbCustomerMock = new List<usr_CUSTOMERS>()
                                                                  {
                                                                      fixture.Build<usr_CUSTOMERS>().With(
                                                                          x => x.password, Tests.SAMPLE_PASSWORD)
                                                                          .With(x => x.email, Tests.SAMPLE_EMAIL_ADDRESS)
                                                                          .Without(x => x.EntityKey)
                                                                          .Without(x=> x.sys_COUNTRYReference)
                                                                          .Without(x=>x.sys_STATEReference)
                                                                          .Without(x=>x.sys_STATE)
                                                                          .Without(x=>x.sys_COUNTRY)
                                                                          .Without(x=> x.sys_COUNTRY1Reference)
                                                                          .Without(x=>x.sys_STATE1Reference)
                                                                          .Without(x=>x.sys_STATE1)
                                                                          .Without(x=>x.sys_COUNTRY1)
                                                                          .CreateAnonymous(),
                                                                      fixture.Build<usr_CUSTOMERS>().With(
                                                                          x => x.password, Tests.SAMPLE_PASSWORD+"2")
                                                                          .With(x => x.email, Tests.SAMPLE_EMAIL_ADDRESS+"2")
                                                                          .Without(x => x.EntityKey)
                                                                          .Without(x=> x.sys_COUNTRYReference)
                                                                          .Without(x=>x.sys_STATEReference)
                                                                          .Without(x=>x.sys_STATE)
                                                                          .Without(x=>x.sys_COUNTRY)
                                                                          .Without(x=> x.sys_COUNTRY1Reference)
                                                                          .Without(x=>x.sys_STATE1Reference)
                                                                          .Without(x=>x.sys_STATE1)
                                                                          .Without(x=>x.sys_COUNTRY1)
                                                                          .CreateAnonymous(),
                                                                      fixture.Build<usr_CUSTOMERS>().With(
                                                                          x => x.password, Tests.SAMPLE_PASSWORD+"3")
                                                                          .With(x => x.email, Tests.SAMPLE_EMAIL_ADDRESS+"3")
                                                                          .Without(x => x.EntityKey)
                                                                          .Without(x=> x.sys_COUNTRYReference)
                                                                          .Without(x=>x.sys_STATEReference)
                                                                          .Without(x=>x.sys_STATE)
                                                                          .Without(x=>x.sys_COUNTRY)
                                                                          .Without(x=> x.sys_COUNTRY1Reference)
                                                                          .Without(x=>x.sys_STATE1Reference)
                                                                          .Without(x=>x.sys_STATE1)
                                                                          .Without(x=>x.sys_COUNTRY1)
                                                                          .CreateAnonymous(),
                                                                  };

        private readonly List<v_orders_list> dbOrderMock = new List<v_orders_list>()
                                                               {
                                                                   fixture.Build<v_orders_list>().With(x=>x.OrderNumber,Tests.FAKE_ORDERNUMBER).With(x=> x.CustomerEmail,Tests.SAMPLE_EMAIL_ADDRESS).Without(x=>x.EntityKey).CreateAnonymous()
                                                     
                                                               };

        public FakeDataBaseCustomerAccountService(IMappingEngine mapper)
        {
            this.mapper = mapper;
        }


        public bool ValidateCustomer(string email, string password)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                return false;
            }
            try
            {
                var validatedCustomer  = GetCustomers().Where(x => x.email == email && x.password == password).SingleOrDefault();
                if (validatedCustomer != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to validate the cutomer using a password and email= " + email + " the following error occured\r\n" + ex.Message); 
            }
            

        }

        private List<usr_CUSTOMERS> GetCustomers()
        {
            return dbCustomerMock;
        }

        public bool ValidateCustomerUsingOrderNumber(string email, string orderNumber)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(orderNumber))
            {
                return false;
            }
            try
            {
                var orderNumberForDB = Convert.ToInt32(orderNumber);
                var validatedCustomer = GetOrders().Where(x => x.CustomerEmail == email && x.OrderNumber == orderNumberForDB).SingleOrDefault();
                if (validatedCustomer != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("When asked to validate the cutomer using a password and email= " + email + " the following error occured\r\n" + ex.Message);
            }
        }

        private List<v_orders_list> GetOrders()
        {
            return dbOrderMock;
        }

        public MembershipCreateStatus CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public MembershipCreateStatus UpdateCustomer(ExtendedCustomer customer)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByEmail(string email)
        {
            try
            {
                var customer = GetCustomers().Where(x => x.email == email).SingleOrDefault();
                if (customer != null)
                {
                    return mapper.Map<usr_CUSTOMERS, Customer>(customer);
                }
                throw new Exception("Customer not found");
                
            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to get a customer with the email="+email + " an error occured\r\n" + ex.Message);
            }
            


        }

        public string RecoverPassword(string email)
        {
            try
            {
                var customer = GetCustomers().Where(x => x.email == email).SingleOrDefault();
                if (customer!=null)
                {
                    return customer.password;
                }
                throw new Exception("Customer ws not found");
            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to recover password for user:" + email + " an error occured:\r\n" + ex.Message);
            }
        }

        public ExtendedCustomer GetExtendedCustomerByEmail(string email)
        {
            try
            {
                var customer = GetCustomers().Where(x => x.email == email).SingleOrDefault();
                if (customer != null)
                {
                    return mapper.Map<usr_CUSTOMERS, ExtendedCustomer>(customer);
                }
                throw new Exception("Customer not found");

            }
            catch (Exception ex)
            {

                throw new Exception("When asked to get an extended customer with the email=" + email + " an error occured\r\n" + ex.Message);
            }
        }
    }
}