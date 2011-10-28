using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Extensions
{
    public static class MVCExtentions
    {
        public static IHtmlString InfoPart(this HtmlHelper html, JewelComponentInfoPart infoPart)
        {
            var str = String.Format("<tr class=\"info\"><td>{0}</td><td>{1}</td></tr>",infoPart.Title,infoPart.Property);

            return MvcHtmlString.Create(str);


        }

        public static IHtmlString CreateNavigationTab(this HtmlHelper html, NavigationTab navigationTab)
        {

            //parent
            var container = new TagBuilder("div");
            container.AddCssClass("cell");
            container.AddCssClass("tab");
            container.AddCssClass(navigationTab.HighlightState);

            if (navigationTab.Type == NagivationTabType.YourOrder)
            {
                //container.AddCssClass("right");
            }

            var tabdiv = new TagBuilder("div");
            tabdiv.AddCssClass("frame");
            tabdiv.AddCssClass(navigationTab.CssClass);

            var title = new TagBuilder("span");
            title.AddCssClass("title");
            title.SetInnerText(navigationTab.Title);

            tabdiv.InnerHtml = title.ToString(TagRenderMode.Normal);

            if (navigationTab.HasEditAndViewLinks)
            {
                var modify = new  TagBuilder("a");
                modify.SetInnerText("Modify");
                modify.MergeAttribute("href",navigationTab.ModifyRoute);
                tabdiv.InnerHtml += modify.ToString(TagRenderMode.Normal);

                var view = new TagBuilder("a");
                view.SetInnerText("View");
                view.MergeAttribute("href", navigationTab.ViewRoute);
                tabdiv.InnerHtml += view.ToString(TagRenderMode.Normal);

            }

            if (!String.IsNullOrWhiteSpace(navigationTab.Amount))
            {
                var amount = new TagBuilder("span");
                amount.AddCssClass("amount");
                amount.AddCssClass("inline");
                amount.AddCssClass("right");
                amount.SetInnerText(navigationTab.Amount);
                tabdiv.InnerHtml += amount.ToString(TagRenderMode.Normal);
            }

            container.InnerHtml += tabdiv.ToString(TagRenderMode.Normal);





            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));


        }

        public static string AbsoluteAction(this UrlHelper url, string action, string controller)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;

            string absoluteAction = string.Format("{0}://{1}{2}",
                                                  requestUrl.Scheme,
                                                  requestUrl.Authority,
                                                  url.Action(action, controller));

            return absoluteAction;
        }


      
     

    }
}