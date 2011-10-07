using System.Web.Routing;

namespace JONMVC.Website.Models.Utils
{
    public interface IWebHelpers
    {
        string RouteUrl(string routeName,RouteValueDictionary dictionary);
    }
}