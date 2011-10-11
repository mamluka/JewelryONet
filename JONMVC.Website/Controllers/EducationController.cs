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
    public class EducationController : Controller
    {
        //
        // GET: /Education/

        public ActionResult Index()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult Diamond()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult Gemstone()
        {
            return View(new EmptyViewModel());
        }
    }
}
