using System.Collections.Generic;

namespace JONMVC.Website.Models.Services
{
    public interface IWishListPersistence
    {
        List<int> GetItemsOnWishList();
        void SaveID(int jewelID);
        void RemoveID(int jewelID);
        void ClearWishList();
    }
}