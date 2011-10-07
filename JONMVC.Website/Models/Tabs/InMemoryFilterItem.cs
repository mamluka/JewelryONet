using System.Collections.Generic;
using JONMVC.Website.Models.Helpers;

namespace JONMVC.Website.Models.Tabs
{
    public class InMemoryFilterItem
    {
        public int FilterItemID { get; private set; }

        public string FilterDisplayName { get; set; }

        public DynamicSQLWhereObject DynamicSQL { get; private set; }

        public InMemoryFilterItem(int id,string displayname,string pattern,List<object> valueslist )
        {
            FilterItemID = id;
            FilterDisplayName = displayname;
            DynamicSQL = new DynamicSQLWhereObject(pattern,valueslist);
            

        }


    }
}