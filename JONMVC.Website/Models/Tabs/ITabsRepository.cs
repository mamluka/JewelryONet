using System.Collections.Generic;

namespace JONMVC.Website.Models.Tabs
{
    public interface ITabsRepository
    {
        List<Tab> GetTabsCollectionByKey(string tabKey);
    }
}