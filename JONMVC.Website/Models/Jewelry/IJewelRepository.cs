using System.Collections.Generic;
using JONMVC.Website.Models.Helpers;

namespace JONMVC.Website.Models.Jewelry
{
    public interface IJewelRepository
    {
        int CurrentPage { get; }
        int TotalItems { get; }
        List<Jewel> GetJewelsByDynamicSQL(DynamicSQLWhereObject dynamicSQL);
        void OrderJewelryItemsBy(JewelryDynamicOrderBy orderBy);
        void FilterJewelryBy(DynamicSQLWhereObject dynamicFilter);
        void ItemsPerPage(int itemsperpage);
        void Page(int currentpage);
        Jewel GetJewelByID(int jewelryID);
        void FilterMediaByMetal(JewelMediaType jewelMediaType);
    }
}