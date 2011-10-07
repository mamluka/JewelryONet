using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;
using Ninject;
using Ninject.Modules;
using Mvc.Mailer;

namespace JONMVC.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPathBarGenerator pathBarGenerator;
        private readonly IUserMailer mailer;

        //
        // GET: /Home/
        public HomeController(IPathBarGenerator pathBarGenerator)
        {
            this.pathBarGenerator = pathBarGenerator;
        }

        public ActionResult Index()
        {

            var viewModel = new EmptyViewModel();
            return View(viewModel);
        }

    }

    
}
