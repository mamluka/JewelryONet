using System.Collections.Generic;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.ViewModels.Builders;

namespace JONMVC.Website.ViewModels.Views
{
    public interface ITabsViewModel
    {
        List<ICustomTabFilter> CustomFilters { get; set; }

        string TabKey { get; set; }
        string TabId { get; set; }
        JewelMediaType MetalFilter { get; set; }
        OrderByPrice OrderByPrice { get; set; }
        string Title { get; set; }
        string Sprite { get; set; }
        List<JewelInTabContainer> JewelryInTabContainersCollection { get; set; }
        int Page { get; set; }
        int TotalPages { get; set; }
        int ItemsPerPage { get; set; }
        List<Tab> Tabs { get; set; }
        bool IsShowTabs { get; set; }
        string InTabPartialView { get; set; }

        List<KeyValuePair<string, string>> MetalFilterItems { get; set; }
        List<KeyValuePair<string, string>> OrderByPriceItems { get; set; }

        string ExtraText { get; set; }
        string ShortTitle { get; set; }
    }
}