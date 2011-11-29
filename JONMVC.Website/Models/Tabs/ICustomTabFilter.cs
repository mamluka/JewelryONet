using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Tabs
{
    public interface ICustomTabFilter
    {
        CustomTabFilterViewModel ViewModel(int currentValue);
        DynamicSQLWhereObject DynamicSQLByFilterValue(int filterValue);
        string Key { get; }
      
    }
}