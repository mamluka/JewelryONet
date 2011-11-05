using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.ViewModels.Builders;

namespace JONMVC.Website.ViewModels.Views
{
    public class ChooseSettingViewModel:CustomJewelPersistenceBase,ITabsViewModel,IPageViewModelBase
    {

        public List<NavigationTab> TabsForJewelDesignNavigation { get; set; }

        public List<ICustomTabFilter> CustomFilters { get; set; }
       
        public string TabKey { get; set; }
        public string TabId { get; set; }
        [DisplayName("Metal Type:")]
        public JewelMediaType MetalFilter { get; set; }
        [DisplayName("Sort by Price:")]
        public OrderByPrice OrderByPrice { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public List<KeyValuePair<string, string>> PathBarItems { get; set; }
        public string PageTitle { get; set; }
        public string Sprite { get; set; }
        public List<JewelInTabContainer> JewelryInTabContainersCollection { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public List<Tab> Tabs { get; set; }
        public bool IsShowTabs { get; set; }
        public string InTabPartialView { get; set; }

        public string ExtraText { get; set; }

        public List<KeyValuePair<string, string>> MetalFilterItems { get; set; }
        public List<KeyValuePair<string, string>> OrderByPriceItems { get; set; }  

        private const string DEFAULT_JEWELDESIGN_TABKEY = "jewel-design-settings";

        public ChooseSettingViewModel()
        {
            MetalFilter = JewelMediaType.WhiteGold;
            OrderByPrice = OrderByPrice.LowToHigh;
            PageTitle = "";
            Sprite = "";
            Page = 1;
            ItemsPerPage = 21;
            TotalPages = 0;

            //use default tabkey because we only have one tabbase right now
            TabKey = DEFAULT_JEWELDESIGN_TABKEY;
        }      
    }
}