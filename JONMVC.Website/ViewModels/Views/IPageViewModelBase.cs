using System.Collections.Generic;

namespace JONMVC.Website.ViewModels.Views
{
    public interface IPageViewModelBase
    {
        List<KeyValuePair<string, string>> PathBarItems { get; set; }
        string PageTitle { get; set; }
    }
}