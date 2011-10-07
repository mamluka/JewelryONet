using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Extensions;
using JONMVC.Website.Models.JewelDesign;

namespace JONMVC.Website.ViewModels.Views
{
    public class DiamondViewModel:PageViewModelBase
    {
        public List<NavigationTab> TabsForJewelDesignNavigation { get; set; }

        public prettyPhotoMedia MainDiamondPicture { get; set; }

        public string DiamondHiResPicture { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string DiamondID { get; set; }

        public string ItemCode { get; set; }

        public string Shape { get; set; }

        public string Weight { get; set; }

        public string Color { get; set; }

        public string Clarity { get; set; }

        public string Fluorescence { get; set; }

        public string Polish { get; set; }

        public string Symmetry { get; set; }

        public string Table { get; set; }

        public string Depth { get; set; }

        public string Dimensions { get; set; }

        public string Cut { get; set; }

        public string Report { get; set; }

        public CustomJewelPersistenceBase JewelPersistence { get; set; }

        public Dictionary<string,DiamondHelpViewModel> DiamondHelp { get; set; }
    }

    public class DiamondHelpViewModel
    {
        public string Title { get; set; }
        public string CurrentValueOfHelp { get; set; }
        public string BodyText { get; set; }
        public List<string> HelpValues { get; set; }
    }
}