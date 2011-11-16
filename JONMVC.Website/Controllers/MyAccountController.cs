using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using Mvc.Mailer;

namespace JONMVC.Website.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IAuthentication authentication;
        private readonly ICustomerAccountService customerAccountService;
        private readonly IUserMailer mailer;
        private readonly IMappingEngine mapper;
        private readonly IOrderRepository orderReporistory;
        //
        // GET: /MyAccount/

        public MyAccountController(IAuthentication authentication, ICustomerAccountService customerAccountService, IUserMailer mailer, IMappingEngine mapper, IOrderRepository orderReporistory)
        {
            this.authentication = authentication;
            this.customerAccountService = customerAccountService;
            this.mailer = mailer;
            this.mapper = mapper;
            this.orderReporistory = orderReporistory;
        }

        [Authorize]
        [RequireHttps]
        public ActionResult Index()
        {
            var email = authentication.CustomerData.Email;
            var builder = new MyAccountViewModelBuilder(email, customerAccountService, orderReporistory, mapper);
            var viewModel = builder.Build();

            return View(viewModel);
        }
        [RequireHttps]
        public ActionResult CheckMyOrderStatus()
        {
            if (authentication.IsSignedIn())
            {
                return RedirectToAction("Index");
            }
            var viewModel = new CheckMyOrderStatusViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult CheckMyOrderStatus(string email, string ordernumber)
        {
            try
            {
                if (customerAccountService.ValidateCustomerUsingOrderNumber(email, ordernumber))
                {
                    var customerData = customerAccountService.GetCustomerByEmail(email);
                    authentication.Signin(email, customerData);
                    return RedirectToAction("OrdersStatus", "Checkout",
                                            new RouteValueDictionary() {{"orderNumber", ordernumber}});
                }
                var viewModel = new CheckMyOrderStatusViewModel();
                viewModel.HasError = true;
                return View(viewModel);
            }
            catch (Exception)
            {
                //TODO add the error view here
                var viewModel = new CheckMyOrderStatusViewModel();
                viewModel.HasError = true;
                return View(viewModel);
            }
            
            
        }
        [RequireHttps]
        public ActionResult Register()
        {
            var viewModel = new RegisterCustomerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult Register(RegisterCustomerViewModel model)
        {
            var viewModel = new RegisterCustomerViewModel();
            var customer = mapper.Map<RegisterCustomerViewModel, Customer>(model);

            var returnStatus = customerAccountService.CreateCustomer(customer);
            if (returnStatus == MembershipCreateStatus.Success)
            {
                var customerData = mapper.Map<RegisterCustomerViewModel, Customer>(model);
                mailer.NewCustomer(customer).Send();
                authentication.Signin(customer.Email, customerData);
                return RedirectToAction("ThankYouForJoining");
            }
            viewModel.HasError = true;
            viewModel.CreateStatus = new CustomerCreationError(returnStatus);
            return View(viewModel);
        }


        [RequireHttps]
        public ActionResult Signin(SigninViewModel viewModel)
        {
            if (String.IsNullOrEmpty(viewModel.RouteController))
            {
                viewModel = new SigninViewModel();
                viewModel.RouteAction = "Index";
                viewModel.RouteController = "MyAccount";
                viewModel.RedirectMode = RedirectMode.Route;
            }
            return View(viewModel);
        }

        //TODO add remember me to the login control
        
        [RequireHttps]
        public ActionResult ProcessSignin(SigninViewModel model)
        {

            var isValidLogin = CheckifValidPasswordAndSignInUsingModel(model);

            if (isValidLogin)
            {
                if (model.RedirectMode == RedirectMode.Link)
                {
                    return Redirect(model.ReturnURL);
                }
                object routeValues = null;

                if (!String.IsNullOrWhiteSpace(model.JSONEncodedRouteValues))
                {
                    var js = new JavaScriptSerializer();

                    routeValues = js.Deserialize(model.JSONEncodedRouteValues,
                                                 Type.GetType(model.RouteValuesModelClassName));
                }
                return RedirectToAction(model.RouteAction, model.RouteController, routeValues);
            }

            model.HasError = true;
            model.Password = String.Empty;

            return RedirectToAction("Signin", model);
        }

        public bool CheckifValidPasswordAndSignInUsingModel(SigninViewModel model)
        {
            var isValidLogin = customerAccountService.ValidateCustomer(model.Email, model.Password);

            if (isValidLogin)
            {
                var customer = customerAccountService.GetCustomerByEmail(model.Email);
                authentication.Signin(model.Email, customer);
            }
            return isValidLogin;
        }

        [RequireHttps]
        public ActionResult RecoverPassword()
        {
            var viewModel = new RecoverPasswordViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult RecoverPassword(RecoverPasswordViewModel model)
        {
            try
            {
                var lostPassword = customerAccountService.RecoverPassword(model.Email);
                var customer = customerAccountService.GetCustomerByEmail(model.Email);
                mailer.RecoverPassword(customer.FirstName + " " + customer.LastName, model.Email, lostPassword).Send();

                return View("RecoverPasswordSuccess",(object)model.Email);
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.ErrorMessage = ex.Message;
                return View(model);
            }

        }

        public ActionResult Signout()
        {
            authentication.Signout();

            return RedirectToAction("Index", "Home");
        }

        [ExitHttpsIfNotRequired]
        public ActionResult ThankYouForJoining()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult ThankYouForSubscribingEithUs()
        {
            return View();
        }

        public ActionResult SigninTopManuLink()
        {
            ViewBag.IsSignedIn = authentication.IsSignedIn();

            return View();
        }
        
        public ActionResult UpdateCustomerDetails(ExtendedCustomer extendedCustomer)
        {
            var status = customerAccountService.UpdateCustomer(extendedCustomer);
            if (status == MembershipCreateStatus.Success)
            {
                return RedirectToAction("Index","MyAccount");    
            };
            return RedirectToAction("ReportError","Services",new ReportErrorViewModel
                                                                 {
                                                                     ErrorMessage = "Can't update user details"
                                                                 });

        }

        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                customerAccountService.ChangePassword(model.Email, model.OldPassword, model.NewPassword);
                return Json(new OporationWithoutReturnValueJsonModel());
            }
            catch (Exception ex)
            {
                return Json(new OporationWithoutReturnValueJsonModel(true, ex.Message));
            }


        }
    }
}
