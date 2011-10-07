using System.Collections.Generic;

namespace JONMVC.Website.ViewModels.Views
{
    public class PageViewModelBase : IPageViewModelBase
    {
        public List<KeyValuePair<string,string>> PathBarItems { get; set; }
        public string PageTitle { get; set; }
    }
}