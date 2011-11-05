using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.ViewModels.Builders;

namespace JONMVC.Website.ViewModels.Views
{
    public class TabsViewModel : PageViewModelBase, ITabsViewModel
    {
        public List<ICustomTabFilter> CustomFilters { get; set; }

        #region Display

        public string TabKey { get; set; }
        public string TabId { get; set; }
        [DisplayName("Metal Type:")]
        public JewelMediaType MetalFilter { get; set; }
        [DisplayName("Sort by Price:")]
        public OrderByPrice OrderByPrice { get; set; }

        public string Title { get; set; }
        public string Sprite { get; set; }

        public List<JewelInTabContainer> JewelryInTabContainersCollection { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }

        public List<Tab> Tabs { get; set; }

        public bool IsShowTabs { get; set; }

        public string InTabPartialView { get; set; }

        #endregion

        #region Supporters

        public List<KeyValuePair<string,string>> MetalFilterItems { get; set; }
        public List<KeyValuePair<string, string>> OrderByPriceItems { get; set; }

        public string ExtraText { get; set; }

        public string ShortTitle { get; set; }

        #endregion


       

        public TabsViewModel()
        {
            MetalFilter = JewelMediaType.WhiteGold;
            OrderByPrice = OrderByPrice.LowToHigh;
            Title = "";
            Sprite = "";
            Page = 1;
            ItemsPerPage = 21;
            TotalPages = 0;

        }
        

       
    }

    public interface ICustomTabFilter
    {
        int Value { get; set; }
    }

    public class GemstoneCenterStoneFilter:ICustomTabFilter
    {
        public int Value { get; set; }
        
    }

    public enum GemstoneCenterStoneFilterValues
    {
        Ruby,
        Sapphire,
        Emerald
    }
}