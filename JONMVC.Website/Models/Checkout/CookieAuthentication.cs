using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace JONMVC.Website.Models.Checkout
{
    public class CookieAuthentication:IAuthentication
    {
        private readonly HttpContextBase httpContext;

        public CookieAuthentication(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public void Signin(string email, Customer userData)
        {

            var encodedString = JsonConvert.SerializeObject(userData);

            var authTicket = new FormsAuthenticationTicket(
                1, //version
                email, // user name
                DateTime.Now,             //creation
                DateTime.Now.AddMinutes(30), //Expiration
                true, encodedString
                //Persistent
                ); //since Classic logins don't have a "Friendly Name".  OpenID logins are handled in the AuthController.

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            httpContext.Response.Cookies.Add(cookie);
            httpContext.Request.Cookies.Add(cookie);
        }

        public bool IsSignedIn()
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }
            return false;
        }

        public Customer CustomerData
        {
            get { return GetCustomerData(); }
        }

        private Customer GetCustomerData()
        {
            try
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var decryptedCustomerData = JsonConvert.DeserializeObject<Customer>(authTicket.UserData);

                return new Customer()
                           {
                               Country = decryptedCustomerData.Country,
                               Firstname = decryptedCustomerData.Firstname,
                               Email = decryptedCustomerData.Email,
                               CountryID = decryptedCustomerData.CountryID,
                               StateID = decryptedCustomerData.StateID,
                               Lastname = decryptedCustomerData.Lastname,
                               State = decryptedCustomerData.State,
                           };
            }
            catch (Exception ex)
            {

                throw new Exception("When asked to read customer information from the cookie an error occured:\r\n" +
                                    ex.Message);
            }

        }

        public void Signout()
        {
            var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                httpContext.Response.Cookies.Add(cookie);
            }

        }
    }
}