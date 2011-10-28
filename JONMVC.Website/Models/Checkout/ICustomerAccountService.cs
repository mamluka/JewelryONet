using System.Web.Security;

namespace JONMVC.Website.Models.Checkout
{
    public interface ICustomerAccountService
    {
        bool ValidateCustomer(string email, string password);
        bool ValidateCustomerUsingOrderNumber(string email, string orderNumber);
        MembershipCreateStatus CreateCustomer(Customer customer);
        MembershipCreateStatus UpdateCustomer(ExtendedCustomer customer);
        Customer GetCustomerByEmail(string email);
        string RecoverPassword(string email);
        void ChangePassword(string email,string oldpassword, string newpassword);
        ExtendedCustomer GetExtendedCustomerByEmail(string email);
        MembershipCreateStatus CreateExtendedCustomer(ExtendedCustomer customer, string password);

    }
}