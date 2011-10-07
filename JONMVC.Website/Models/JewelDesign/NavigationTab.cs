using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.JewelDesign
{
    public class NavigationTab
    {
        public string HighlightState { get; set; }

        public string CssClass { get; set; }

        public string Title { get; set; }

        public bool HasEditAndViewLinks { get; set; }

        public string ModifyRoute { get; set; }

        public string ViewRoute { get; set; }

        public string Amount { get; set; }

        public NagivationTabType Type { get; set; }
    }
}