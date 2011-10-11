using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using Mvc.Mailer;
namespace JONMVC.Website.Controllers
{

    public class ServicesController : Controller
    {
        private readonly IViewModelBuilder wishListBuilder;
        private readonly IPathBarGenerator pathBarGenerator;
        private readonly IUserMailer userMailer;
        private readonly ISettingManager settingManager;
        //
        // GET: /Services/

        public ServicesController(IViewModelBuilder wishListBuilder, IPathBarGenerator pathBarGenerator, IUserMailer userMailer,ISettingManager settingManager)
        {
            this.wishListBuilder = wishListBuilder;
            this.pathBarGenerator = pathBarGenerator;
            this.userMailer = userMailer;
            this.settingManager = settingManager;
        }

        public ActionResult Wishlist()
        {
            var viewModel = wishListBuilder.Build();

            return View(viewModel);
        }

        public ActionResult Policy()
        {
            var viewModel = new EmptyViewModel();
            viewModel.PageTitle = "Enjoy Risk Free Online Shopping";
            viewModel.PathBarItems = pathBarGenerator.GenerateUsingSingleTitle<UsingTitlePathBarResolver>("Risk Free Online Shopping");


            return View(viewModel);
        }

          public ActionResult GetInTouch()
        {
            var viewModel = new EmptyViewModel();
            viewModel.PageTitle = "Customer Service";
            viewModel.PathBarItems = pathBarGenerator.GenerateUsing<UsingDynamicTitlePathBarResolver, dynamic>(viewModel);
            return View(viewModel);
        }

          public ActionResult FAQ()
          {
              var viewModel = new EmptyViewModel();
              viewModel.PageTitle = "frequently asked questions";
              viewModel.PathBarItems = pathBarGenerator.GenerateUsing<UsingDynamicTitlePathBarResolver, dynamic>(viewModel);
              return View(viewModel);
          }

          public ActionResult AskQuestion(string name, string email, string phone, string question)
        {
            try
            {
                var adminEmail = settingManager.AdminEmail();

                userMailer.AskQuestion(adminEmail, new AskQuestionEmailTemplateViewModel()
                                                       {
                                                           Email = email,
                                                           Name = name,
                                                           Phone = phone,
                                                           Question = question
                                                       })
                                                       .Send();
                return Json(new OporationWithoutReturnValueJsonModel());
            }
            catch (Exception ex)
            {
                return Json(new OporationWithoutReturnValueJsonModel(true, ex.Message));
            }
        }


        public ActionResult Feedbacks()
        {
            return View(new FeedBackskViewModel());
        }

        public ActionResult Affiliate()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult AboutUs()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult CustomOrders()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult ConflictFreeDiamonds()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult Engraving()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult Search(string searchTerm)
        {
            var viewModel = new SearchViewModel();
            viewModel.Term = searchTerm;
            return View(viewModel);
        }
    }
}
