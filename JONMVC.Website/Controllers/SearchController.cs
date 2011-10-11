using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Controllers
{
    [ExitHttpsIfNotRequired]
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View(new EmptyViewModel());
        }
    }
}
