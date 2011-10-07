using System.Collections.Generic;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.JewelryItem;

namespace JONMVC.Website.ViewModels.Views
{
    public class EndViewModel : PageViewModelBase, IJewelBeseInfo
    {

        public List<NavigationTab> TabsForJewelDesignNavigation { get; set; }

        public string ItemNumber { get; set; }

        public string Metal { get; set; }

        public string Weight { get; set; }

        public string Width { get; set; }

        public string DiamondID { get; set; }

        public string SettingPrice { get; set; }

        public string Shape { get; set; }

        public string DiamondWeight { get; set; }

        public string Color { get; set; }

        public string Clarity { get; set; }

        public string DiamondPrice { get; set; }

        public string TotalPrice { get; set; }

        public object Size { get; set; }

        public string DiamondIcon { get; set; }

        public string SettingIcon { get; set; }

        public CustomJewelPersistenceBase JewelPersistence { get; set; }
    }
}