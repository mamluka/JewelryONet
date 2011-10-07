using System;
using System.Web;
using Rhino.Mocks;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;

namespace JONMVC.Website.Tests.Unit.Helpers
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext(this MockRepository mocks)
        {
            HttpContextBase context = mocks.PartialMock<HttpContextBase>();
            HttpRequestBase request = mocks.PartialMock<HttpRequestBase>();
            HttpResponseBase response = mocks.PartialMock<HttpResponseBase>();
            HttpSessionStateBase session = mocks.PartialMock<HttpSessionStateBase>();
            HttpServerUtilityBase server = mocks.PartialMock<HttpServerUtilityBase>();

            SetupResult.For(context.Request).Return(request);
            SetupResult.For(context.Response).Return(response);
            SetupResult.For(context.Session).Return(session);
            SetupResult.For(context.Server).Return(server);

            mocks.Replay(context);
            return context;
        }

        public static HttpContextBase FakeHttpContext(this MockRepository mocks, string url)
        {
            HttpContextBase context = FakeHttpContext(mocks);
            context.Request.SetupRequestUrl(url);
            return context;
        }

        public static void SetFakeControllerContext(this MockRepository mocks, Controller controller)
        {
            var httpContext = mocks.FakeHttpContext();
            ControllerContext context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0, url.IndexOf("?"));
            else
                return url;
        }

        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                NameValueCollection parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            SetupResult.For(request.HttpMethod).Return(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (!url.StartsWith("~/"))
                throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

            SetupResult.For(request.QueryString).Return(GetQueryStringParameters(url));
            SetupResult.For(request.AppRelativeCurrentExecutionFilePath).Return(GetUrlFileName(url));
            SetupResult.For(request.PathInfo).Return(string.Empty);
        }

    }
}