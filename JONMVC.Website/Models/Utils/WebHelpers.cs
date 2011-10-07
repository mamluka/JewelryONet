using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JONMVC.Website.Models.Utils
{
    public class WebHelpers : IWebHelpers
    {
        public string RouteUrl(string routeName,RouteValueDictionary dictionary)
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return url.RouteUrl(routeName, dictionary);
        }
    }
}