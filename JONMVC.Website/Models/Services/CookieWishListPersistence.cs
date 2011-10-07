using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
namespace JONMVC.Website.Models.Services
{
    public class CookieWishListPersistence : IWishListPersistence
    {
        private readonly HttpContextBase httpContext;
        private readonly string wishListKey = "wishlistitems";

        public CookieWishListPersistence(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
            
        }

        public List<int> GetItemsOnWishList()
        {
            return TryToGetItemsListFromCookie();
        }

        private List<int> TryToGetItemsListFromCookie()
        {
            try
            {
                var cookie = GetCookie();
                var ids = cookie[wishListKey].Split(',').Select(x => Convert.ToInt32(x)).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }

        public void SaveID(int jewelID)
        {
            if (Exists(jewelID))
            {
                return;
            }

            var ids = TryToGetItemsListFromCookie();
            ids.Add(jewelID);
            PersistToCookie(ids);

        }

        public void RemoveID(int jewelID)
        {
            var ids = TryToGetItemsListFromCookie();
            if (ids.Contains(jewelID))
            {
                ids.RemoveAll(x => x == jewelID);
                PersistToCookie(ids);
            }
            

        }

        private void PersistToCookie(IEnumerable<int> ids)
        {
            var cookie = GetCookie();
            if (cookie != null)
            {
                cookie[wishListKey] = String.Join(",", ids);
                httpContext.Response.SetCookie(cookie);
            }
            else
            {
                cookie = new HttpCookie("JON");
                cookie[wishListKey] = String.Join(",", ids);
                httpContext.Response.AppendCookie(cookie);
            }
        }

        private HttpCookie GetCookie()
        {
            var cookie = httpContext.Request.Cookies["JON"];
            return cookie;
        }

        public bool Exists(int jewelID)
        {
            var ids = TryToGetItemsListFromCookie();
            if (ids.Contains(jewelID))
            {
                return true;
            }
            return false;
        }

    }
}