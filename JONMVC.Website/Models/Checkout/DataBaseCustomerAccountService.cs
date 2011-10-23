using System;
using System.Data;
using System.Web.Security;
using AutoMapper;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.DB;
using System.Linq;
namespace JONMVC.Website.Models.Checkout
{
    public class DataBaseCustomerAccountService : ICustomerAccountService
    {
        private readonly IMappingEngine mapper;

        public DataBaseCustomerAccountService(IMappingEngine mapper)
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
                using (var db = new JONEntities())
                {

                    
                    var validatedCustomer =
                        db.usr_CUSTOMERS.Where(x => x.email == email && x.password == password).SingleOrDefault();
                    if (validatedCustomer != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("When asked to validate the cutomer using a password and email= " + email + " the following error occured\r\n" + ex.Message);
            }
        }

        public bool ValidateCustomerUsingOrderNumber(string email, string orderNumber)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(orderNumber))
            {
                return false;
            }
            try
            {
                using (var db = new JONEntities())
                {
                    var orderNumberForDB = Convert.ToInt32(orderNumber);
                    var validatedCustomer =
                        db.v_orders_list.Where(x => x.CustomerEmail == email && x.OrderNumber == orderNumberForDB).
                            SingleOrDefault();
                    if (validatedCustomer != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("When asked to validate the cutomer using a password and email= " + email + " the following error occured\r\n" + ex.Message);
            }
        }

        public MembershipCreateStatus CreateCustomer(Customer customer)
        {
            var customerdto = mapper.Map<Customer, usr_CUSTOMERS>(customer);
            try
            {
                using (var db = new JONEntities())
                {
                    db.usr_CUSTOMERS.AddObject(customerdto);
                    db.SaveChanges();

                    return MembershipCreateStatus.Success;
                }
            }
            catch (Exception ex)
            {
                return MembershipCreateStatus.ProviderError;
            }
           
        }

        public MembershipCreateStatus UpdateCustomer(ExtendedCustomer customer)
        {
            //var customerdto = mapper.Map<ExtendedCustomer, usr_CUSTOMERS>(customer);
            try
            {
                using (var db = new JONEntities())
                {
                    var existingCustomer = db.usr_CUSTOMERS.Where(x => x.email == customer.Email).SingleOrDefault();

                    var source = new MergeExistingCustomerAndExtendedCustomer();
                    source.First = existingCustomer;
                    source.Second = customer;

                    var customerdto = mapper.Map<MergeExistingCustomerAndExtendedCustomer, usr_CUSTOMERS>(source);

                    db.usr_CUSTOMERS.Detach(existingCustomer);
                    db.usr_CUSTOMERS.Attach(customerdto);

                    db.ObjectStateManager.ChangeObjectState(customerdto, EntityState.Modified);

                 //   db.usr_CUSTOMERS.AddObject(customerdto);
                    db.SaveChanges();

                    return MembershipCreateStatus.Success;
                }
            }
            catch (Exception ex)
            {
                return MembershipCreateStatus.ProviderError;
            }

        }

        public Customer GetCustomerByEmail(string email)
        {
            try
            {
                using (var db = new JONEntities())
                {
                    var customer =db.usr_CUSTOMERS.Where(x => x.email == email).SingleOrDefault();
                    if (customer != null)
                    {
                        return mapper.Map<usr_CUSTOMERS, Customer>(customer);
                    }
                    throw new Exception("Customer not found");
                }

            }
            catch (Exception ex)
            {

                throw new Exception("When asked to get a customer with the email=" + email + " an error occured\r\n" + ex.Message);
            }
            
        }

        public string RecoverPassword(string email)
        {
            try
            {
                using (var db = new JONEntities())
                {
                    var customer = db.usr_CUSTOMERS.Where(x => x.email == email).SingleOrDefault();
                    if (customer != null)
                    {
                        return customer.password;
                    }
                    throw new Exception("Customer email was not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("When asked to recover password for customer: " + email + " an error occured:\r\n" + ex.Message);
            }
        }

        public ExtendedCustomer GetExtendedCustomerByEmail(string email)
        {
            try
            {
                using (var db = new JONEntities())
                {
                    var customer = db.usr_CUSTOMERS.Where(x => x.email == email).SingleOrDefault();
                    if (customer != null)
                    {
                        return mapper.Map<usr_CUSTOMERS, ExtendedCustomer>(customer);
                    }
                    throw new Exception("Customer not found");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("When asked to get an extended customer with the email=" + email + " an error occured\r\n" + ex.Message);
            }
        }
    }
}