using System.Collections.Generic;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Json.Builders;

namespace JONMVC.Website.Models.JewelryItem
{
    public interface IMediaSetBuilder
    {
        IEnumerable<JsonMedia> Build(string itemNumberForSet, JewelMediaType mediaSetsOwnedByJewel);
    }
}