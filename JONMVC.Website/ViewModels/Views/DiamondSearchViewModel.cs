using System.Collections.Generic;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.JewelDesign;

namespace JONMVC.Website.ViewModels.Views
{
    public class DiamondSearchViewModel:PageViewModelBase
    {
        public Dictionary<string,object> JSONClientScriptInitializer { get; set; }
        public DiamondSearchParameters DiamondSearchParameters { get; set; }
        public List<NavigationTab> TabsForJewelDesignNavigation { get; set; }
    }
}