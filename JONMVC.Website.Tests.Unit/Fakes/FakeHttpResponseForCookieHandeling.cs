using System.Web;
using MvcContrib.TestHelper.Fakes;

namespace JONMVC.Website.Tests.Unit.Fakes
{
    public class FakeHttpResponseForCookieHandeling:FakeHttpResponse
    {
        private readonly HttpCookieCollection cookies = new HttpCookieCollection();
        public override HttpCookieCollection Cookies
        {
            get { return cookies; }
        }
        public override void AppendCookie(HttpCookie cookie)
        {
            Cookies.Add(cookie);
        }

        public override void SetCookie(HttpCookie cookie)
        {
            Cookies.Set(cookie);
        }
    }
}