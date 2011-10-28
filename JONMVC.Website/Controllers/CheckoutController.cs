using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using Ninject;
using Mvc.Mailer;
namespace JONMVC.Website.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IShoppingCartWrapper shoppingCartWrapper;
        private readonly ShoppingCartItemsFactory cartItemsFactory;
        private readonly IJewelRepository jewelRepository;
        private readonly IDiamondRepository diamondRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMappingEngine mapper;
        private readonly IUserMailer mailer;
        private readonly IAuthentication authentication;
        private readonly ICustomerAccountService accountService;

        //
        // GET: /Checkout/

        public CheckoutController(IShoppingCartWrapper shoppingCartWrapper, ShoppingCartItemsFactory cartItemsFactory, IJewelRepository jewelRepository, IDiamondRepository diamondRepository, IOrderRepository orderRepository, IMappingEngine mapper, IUserMailer mailer, IAuthentication authentication,ICustomerAccountService accountService)
        {
            this.shoppingCartWrapper = shoppingCartWrapper;
            this.cartItemsFactory = cartItemsFactory;
            this.jewelRepository = jewelRepository;
            this.diamondRepository = diamondRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.mailer = mailer;
            this.authentication = authentication;
            this.accountService = accountService;
        }

        [RequireHttps]
        public ActionResult ShoppingCart()
        {
            var shoppingCart = shoppingCartWrapper.Get();
            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelRepository, diamondRepository, mapper);
            

            var builder = new ShoppingCartViewModelBuilder(shoppingCart, jewelRepository, cartItemViewModelBuilder, authentication, mapper);
            var viewModel = builder.Build();

            return View(viewModel);
        }

        public ActionResult ShoppingCartAddJewel(int? id, string size, JewelMediaType? mediaType)
        {
            var jewelMediaType = mediaType ?? JewelMediaType.WhiteGold;
            var shoppingCart = shoppingCartWrapper.Get();
            if (id != null && id > 0)
            {

                var cartItem = cartItemsFactory.JewelCartItem((int)id, size, jewelMediaType);
                shoppingCart.AddItem(cartItem);

                PersistShoppingCart(shoppingCart);
            }

            return RedirectToAction("ShoppingCart");
        }

        public ActionResult ShoppingCartAddDiamond(int diamondid)
        {
            var shoppingCart = shoppingCartWrapper.Get();
            if (diamondid > 0)
            {

                var cartItem = cartItemsFactory.DiamondCartItem(diamondid);
                shoppingCart.AddItem(cartItem);

                PersistShoppingCart(shoppingCart);
            }

            return RedirectToAction("ShoppingCart");
        }

        public ActionResult ShoppingCartAddCustomJewel(int diamondid,int settingid,string size,JewelMediaType mediaType)
        {
            var shoppingCart = shoppingCartWrapper.Get();
            if (diamondid > 0)
            {

                var cartItem = cartItemsFactory.CustomJewelCartItem(diamondid,settingid,size,mediaType);
                shoppingCart.AddItem(cartItem);

                PersistShoppingCart(shoppingCart);
            }

            return RedirectToAction("ShoppingCart");
        }

        public ActionResult RemoveShoppingCartItem(int cartid)
        {
            var shoppingCart = shoppingCartWrapper.Get();
            shoppingCart.Remove(cartid);
            shoppingCartWrapper.Presist(shoppingCart, HttpContext);
            return RedirectToAction("ShoppingCart");
        }

        [RequireHttps]
        
        public ActionResult Billing(CheckoutDetailsModel checkoutDetailsModel)
        {
            if (!String.IsNullOrEmpty(Request.Form["LoginEmail"]))
            {
                var js = new JavaScriptSerializer();

                var jsonModel = js.Serialize(checkoutDetailsModel);

                return RedirectToAction("ProcessSignin", "MyAccount", new RouteValueDictionary
                                                                   {
                                                                       {"RedirectMode",RedirectMode.Route},
                                                                       {"RouteController","Checkout"},
                                                                       {"RouteAction","Billing"},
                                                                       {"Email",Request.Form["LoginEmail"]},
                                                                       {"Password",Request.Form["Password"]},
                                                                       {"JSONEncodedRouteValues",jsonModel},
                                                                       {"RouteValuesModelClassName",checkoutDetailsModel.GetType().FullName}
                                                                    
                                                                   });

            }

            var builder = new BillingViewModelBuilder(checkoutDetailsModel, authentication,accountService,mapper);
            var viewModel = builder.Build();

            return View(viewModel);
        }
        [RequireHttps]
        public ActionResult ReviewOrder(CheckoutDetailsModel checkoutDetailsModel)
        {
            var shoppingCart = shoppingCartWrapper.Get();

            var cartItemViewModelBuilder = new CartItemViewModelBuilder(jewelRepository, diamondRepository, mapper);
            var builder = new ReviewOrderViewModelBuilder(checkoutDetailsModel,shoppingCart,cartItemViewModelBuilder,mapper);
            var viewModel = builder.Build();

            if (checkoutDetailsModel.PaymentMethod == PaymentMethod.PayPal)
            {
                var orderNumber = SaveOrderAndEmail(checkoutDetailsModel, shoppingCart);
                viewModel.OrderNumber = orderNumber;
            }

            return View(viewModel);
        }

        [RequireHttps]
        public ActionResult OrderConfirmation(CheckoutDetailsModel checkoutDetailsModel)
        {
            var shoppingCart = shoppingCartWrapper.Get();

            var orderNumber = SaveOrderAndEmail(checkoutDetailsModel, shoppingCart);
            

            var builder = new OrderConfirmationViewModelBuilder(orderNumber, checkoutDetailsModel);
            var viewModel = builder.Build();

            shoppingCartWrapper.Clear();

            return View(viewModel);
        }

        
        public ActionResult ThankYouForUsingPaypal(int orderNumber)
        {


            return View();
        }



        private int SaveOrderAndEmail(CheckoutDetailsModel checkoutDetailsModel, IShoppingCart shoppingCart)
        {
            var orderBuilder = new OrderBuilder(shoppingCart, authentication, mapper);
            var orderdto = orderBuilder.Build(checkoutDetailsModel);

            var orderNumber = orderRepository.Save(orderdto);

            if (orderNumber > 0)
            {
                var cartItemBuilder = new CartItemViewModelBuilder(jewelRepository, diamondRepository, mapper);
                var emailTemplateBuilder = new OrderConfirmationEmailTemplateViewModelBuilder(orderNumber.ToString(),
                                                                                              checkoutDetailsModel,
                                                                                              shoppingCart,
                                                                                              cartItemBuilder);

                var emailTemplateViewModel = emailTemplateBuilder.Build();
                mailer.OrderConfirmation(checkoutDetailsModel.Email, emailTemplateViewModel).Send();
            }

            return orderNumber;
        }

        [HttpPost]
        public ActionResult JewelSize(int cartid,string size)
        {
            try
            {
                var shoppingCart = shoppingCartWrapper.Get();

                var cartItem = shoppingCart.Items[cartid];
                cartItem.SetSize(size);
                shoppingCart.Update(cartid, cartItem);

                return Json(new OporationWithoutReturnValueJsonModel());
            }
            catch (Exception ex)
            {

                return Json(new OporationWithoutReturnValueJsonModel(true,ex.Message)); 
            }
            

            
        }


        private void PersistShoppingCart(IShoppingCart shoppingCart)
        {
            // Models.Checkout.ShoppingCart.Persist(HttpContext, shoppingCart);
            shoppingCartWrapper.Presist(shoppingCart, HttpContext);
        }

        [Authorize]
        [RequireHttps]
        public ActionResult OrdersStatus(int orderNumber)
        {
            var builder = new OrderStatusViewModelBuilder(mapper, orderRepository);
            var viewModel = builder.Build(orderNumber);
            return View(viewModel);
        }


        public ActionResult CartItemCount()
        {
            var shoppingCart = shoppingCartWrapper.Get();
            var totalItems = shoppingCart.Count;
            return View("CartItemCount",totalItems);
            
        }
    }
}
