using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;
using Ninject;
using Ninject.Modules;
using Mvc.Mailer;
namespace JONMVC.Website.Controllers
{
    [ExitHttpsIfNotRequired]
    public class HomeController : Controller
    {
        private readonly IJewelRepository _jewelRepository;
        private readonly IWebHelpers _webHelpers;

        public HomeController(IJewelRepository jewelRepository,IWebHelpers webHelpers)
        {
            _jewelRepository = jewelRepository;
            _webHelpers = webHelpers;
        }

        public ActionResult Index()
        {

            var viewModel = new EmptyViewModel();
            return View(viewModel);
        }

        public ActionResult BestOfferWidget()
        {
            var jewels =
                _jewelRepository.GetJewelsByDynamicSQL(new DynamicSQLWhereObject("ONBARGAIN = @0", true))
                .OrderBy(random => Guid.NewGuid());
;

            var viewModel = new BestOfferWidgetViewModel
                                {
                                    Jewels = jewels.Select(ToJewelDescriptor).Take(5).ToList(),
                                    Count = jewels.Count()
                                };
            return View(viewModel);
        }

        private JewelryItemDescriptor ToJewelDescriptor(Jewel jewel)
        {
            return new JewelryItemDescriptor
                       {
                           Icon = jewel.Media.IconURLForWebDisplay,
                           ItemUrl = _webHelpers.RouteUrl("JewelryItem", new RouteValueDictionary
                                                                            {
                                                                                {"id",jewel.ID},
                                                                                {"nullableMediaSet",jewel.Media.MediaSet}
                                                                            })
                       };
        }
    }

    public class BestOfferWidgetViewModel
    {
        public IList<JewelryItemDescriptor> Jewels { get; set; }
        public int Count { get; set; }
        
    }

    public class JewelryItemDescriptor
    {
        public string ItemUrl { get; set; }
        public string Icon { get; set; }
    }
}
