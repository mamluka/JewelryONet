using System.Collections.Generic;
using JONMVC.Website.Models.JewelDesign;

namespace JONMVC.Website.ViewModels.Views
{
    public class SettingViewModel : JewelryItemViewModel
    {
        public List<NavigationTab> TabsForJewelDesignNavigation { get; set; }
        public CustomJewelPersistenceBase JewelPersistence { get; set; }
    }
}