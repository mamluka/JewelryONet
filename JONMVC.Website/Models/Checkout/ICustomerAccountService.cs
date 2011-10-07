using System.Web.Security;

namespace JONMVC.Website.Models.Checkout
{
    public interface ICustomerAccountService
    {
        bool ValidateCustomer(string email, string password);
        bool ValidateCustomerUsingOrderNumber(string email, string orderNumber);
        MembershipCreateStatus CreateCustomer(Customer customer);
        Customer GetCustomerByEmail(string email);
        string RecoverPassword(string email);
        ExtendedCustomer GetExtendedCustomerByEmail(string email);

    }
}