using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JONMVC.Website.Models.Admin;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICSVParser csvParser;
        private readonly IDatabasePersistence databasePersistence;
        //
        // GET: /Admin/

        public AdminController(ICSVParser csvParser, IDatabasePersistence databasePersistence)
        {
            this.csvParser = csvParser;
            this.databasePersistence = databasePersistence;
        }

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

            var parser = new TempUpdateDiamodsHendler(model, HttpContext, csvParser, databasePersistence);
            parser.ParseAndSave();
            return View();
        }

    }
}
