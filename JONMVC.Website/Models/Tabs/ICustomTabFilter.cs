using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Tabs
{
    public interface ICustomTabFilter
    {
        CustomTabFilterViewModel ViewModel { get;  }
        DynamicSQLWhereObject DynamicSQLByFilterValue(int filterValue);
        string Key { get; }
      
    }
}