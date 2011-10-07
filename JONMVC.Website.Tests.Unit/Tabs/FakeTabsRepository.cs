using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JONMVC.Website.Models.Tabs;

namespace JONMVC.Website.Tests.Unit.Tabs
{
    class FakeTabsRepository:ITabsRepository
    {
        public List<Tab> GetTabsCollectionByKey(string tabKey)
        {
            return new List<Tab>();
        }
    }
}
