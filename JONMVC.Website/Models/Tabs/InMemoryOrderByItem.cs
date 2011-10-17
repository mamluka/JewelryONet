using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.Tabs
{
    //TODO refractor this in the future seem like have a super class in common

    public class InMemoryOrderByItem
    {
        public int OrderByItemID { get; private set; }

        public string OrderByDisplayName { get; set; }

        public DynamicOrderBy OrderBySQL { get; private set; }

        public InMemoryOrderByItem(int id,string displayname,string field,string direction )
        {
            OrderByItemID = id;
            OrderByDisplayName = displayname;
            OrderBySQL = new DynamicOrderBy(field,direction);
            

        }


    }
}
