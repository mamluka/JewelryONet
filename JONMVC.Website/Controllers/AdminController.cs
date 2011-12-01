using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Models.Admin;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult UpdateDiamonds()
        {
            return View(new UpdateDiamondsModel());
        }
        [HttpPost]
        public ActionResult UpdateDiamonds(UpdateDiamondsModel model)
        {

            return View();
        }

    }
}
