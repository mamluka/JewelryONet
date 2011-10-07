using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Checkout;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using MvcContrib.TestHelper.Fakes;
using Rhino.Mocks;

namespace JONMVC.Website.Tests.Unit.Fakes
{
    public static class FakeFactory
    {
        private static object firstJewelInRepository;

        public static Jewel Jewel()
        {
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());
            return jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);
        } 

        public static FakeHttpContext FakeHttpContext()
        {
            var cookieColletion = new HttpCookieCollection();

            var fakeContext = new FakeHttpContext("/");
            var fakeRequest = new FakeHttpRequest("/", "get", new Uri("http://localhost"), new Uri("http://localhost"),
                                                  new NameValueCollection(), new NameValueCollection(), cookieColletion);

            var fakeResponse = new FakeHttpResponseForCookieHandeling();


            fakeContext.SetRequest(fakeRequest);
            fakeContext.SetResponse(fakeResponse);

            return fakeContext;
        }

        public static FakeHttpContext FakeHttpContextWithCustomerAuthenticationSetTo(bool authenticated)
        {
            var fakeIdentity = MockRepository.GenerateStub<IIdentity>();
            fakeIdentity.Stub(x => x.IsAuthenticated).Return(authenticated);

            var fakePrincipal = MockRepository.GenerateStub<IPrincipal>();
            fakePrincipal.Stub(x => x.Identity).Return(fakeIdentity);
            
            
            var fakeContext = new FakeHttpContext("/",fakePrincipal,null,null,null,null);

            return fakeContext;
        }

        public static FakeHttpContext FakeHttpContextWithCookie(HttpCookie cookie)
        {
          
            var cookieColletion = new HttpCookieCollection();
            cookieColletion.Add(cookie);


            var fakeContext = new FakeHttpContext("/");
            var fakeRequest = new FakeHttpRequest("/", "get", new Uri("http://localhost"), new Uri("http://localhost"),
                                                  new NameValueCollection(), new NameValueCollection(), cookieColletion);

            var fakeResponse = new FakeHttpResponseForCookieHandeling();
            fakeContext.SetResponse(fakeResponse);
            fakeContext.SetRequest(fakeRequest);
            return fakeContext;
        }

        public static FakeHttpContext FakeHttpContextWithSession(SessionStateItemCollection sessionStateItem  )
        {
            var fakeContext = new FakeHttpContext("/",null,null,null,null,sessionStateItem);                                                
            var fakeResponse = new FakeHttpResponseForCookieHandeling();
            fakeContext.SetResponse(fakeResponse);
            return fakeContext;
        }

        public static IShoppingCart ShoppingCartWith3Items()
        {
            var shoppingCart = MockRepository.GenerateStub<IShoppingCart>();
            shoppingCart.Stub(x => x.Items).Return(new List<ICartItem>()
                                                       {

                                                           CheckoutTestsBaseClass.FakeJewelCartItem(
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                               8000),
                                                           CheckoutTestsBaseClass.FakeDiamondCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID, 5000),
                                                           CheckoutTestsBaseClass.FakeCustomJewelCartItem(
                                                               Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                               Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,
                                                               10000)
                                                       }
                );



            return shoppingCart;
        }

        public static Jewel FirstJewelInRepository
        {
            get
            {
                return new FakeJewelRepository(new FakeSettingManager())
                    .GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);
            }
        }
    }

   
}
