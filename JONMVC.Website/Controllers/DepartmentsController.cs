using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IPathBarGenerator pathBarGenerator;
        //
        // GET: /Departments/

        public DepartmentsController(IPathBarGenerator pathBarGenerator)
        {
            this.pathBarGenerator = pathBarGenerator;
        }

        public ActionResult Diamonds()
        {
            var viewModel = new EmptyViewModel();

            return View(viewModel);
        }

        public ActionResult DiamondStuds()
        {
            var viewModel = new EmptyViewModel();

            return View(viewModel);
        }

        public ActionResult EngagementRings()
        {
            var viewModel = new EmptyViewModel();

            return View(viewModel);
        }

        public ActionResult WeddingAndAnniversary()
        {
            var viewModel = new EmptyViewModel();


            return View(viewModel);
        }
        public ActionResult DesignerJewelry()
        {
            var viewModel = new EmptyViewModel();

            return View(viewModel);
        }

        public ActionResult GiftIdeas()
        {
            var viewModel = new EmptyViewModel();

            return View(viewModel);
        }
    }
}
