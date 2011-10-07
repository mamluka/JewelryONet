namespace JONMVC.Website.Models.Checkout
{
    public interface IAuthentication
    {
        void Signin(string email, Customer userData);
        bool IsSignedIn();
        Customer CustomerData { get; }
        void Signout();
    }
}