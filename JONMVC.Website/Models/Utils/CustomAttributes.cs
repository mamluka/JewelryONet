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

                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description),
                false);

                if (attrs.Length > 0)

                    return ((Description)attrs[0]).Text;

            }

            return en.ToString();

        }

        public static string Get<TAttribute>(Enum en) where TAttribute:NamedAttributeBase
        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {

                object[] attrs = memInfo[0].GetCustomAttributes(typeof(TAttribute),
                false);

                if (attrs.Length > 0)

                    return ((TAttribute)attrs[0]).Text;

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

    public class CustomerArea : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return true;
        }
    }
}