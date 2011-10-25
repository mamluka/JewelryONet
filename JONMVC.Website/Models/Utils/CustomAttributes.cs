using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace JONMVC.Website.Models.Utils
{
    public static class CustomAttributes
    {
        public static string GetDescription(Enum en)
        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {

                object[] attrs = memInfo[0].GetCustomAttributes(typeof (Description),
                                                                false);

                if (attrs.Length > 0)

                    return ((Description) attrs[0]).Text;

            }

            return en.ToString();

        }

        public static string Get<TAttribute>(Enum en) where TAttribute : NamedAttributeBase
        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {

                object[] attrs = memInfo[0].GetCustomAttributes(typeof (TAttribute),
                                                                false);

                if (attrs.Length > 0)

                    return ((TAttribute) attrs[0]).Text;

            }

            return en.ToString();

        }
    }


    public class NamedAttributeBase : Attribute
    {
        public string Text;
    }

    public class Description : NamedAttributeBase
    {
        public Description(string text)
        {

            Text = text;

        }

    }

    public class OrderByField : NamedAttributeBase
    {

        public OrderByField(string text)
        {

            Text = text;

        }

    }

    public class OrderByDirection : NamedAttributeBase
    {

        public OrderByDirection(string text)
        {

            Text = text;

        }

    }

   

    public class ExitHttpsIfNotRequiredAttribute : FilterAttribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // abort if it's not a secure connection
            if (!filterContext.HttpContext.Request.IsSecureConnection) return;

            // abort if a [RequireHttps] attribute is applied to controller or action
            if (
                filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof (RequireHttpsAttribute),
                                                                                        true).Length > 0) return;
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof (RequireHttpsAttribute), true).Length > 0)
                return;

            // abort if a [RetainHttps] attribute is applied to controller or action
            if (
                filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof (RetainHttpsAttribute),
                                                                                        true).Length > 0) return;
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof (RetainHttpsAttribute), true).Length > 0)
                return;

            // abort if it's not a GET request - we don't want to be redirecting on a form post
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            // redirect to HTTP
         
            string url = "http://" + filterContext.HttpContext.Request.Url.Host + 
                         filterContext.HttpContext.Request.RawUrl;

            filterContext.Result = new RedirectResult(url);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RetainHttpsAttribute : Attribute
    {

    }

    

}