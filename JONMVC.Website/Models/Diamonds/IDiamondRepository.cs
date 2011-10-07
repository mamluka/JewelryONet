using System.Collections.Generic;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Diamonds
{
    public interface IDiamondRepository
    {
        List<Diamond> DiamondsBySearchParameters(DiamondSearchParameters mappedSearchParameters );
        int LastOporationTotalPages { get; }
        int TotalRecords { get; }
        Diamond GetDiamondByID(int diamondID);
    }
}