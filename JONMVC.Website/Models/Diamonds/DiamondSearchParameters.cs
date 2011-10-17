using System.Collections.Generic;
using System.Linq;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;


namespace JONMVC.Website.Models.Diamonds
{
    public class DiamondSearchParameters : CustomJewelPersistenceForSetting
    {
        public List<string> Shape { get; set; }
        public List<string> Color { get; set; }
        public List<string> Clarity { get; set; }
        public List<string> Report { get; set; }
        public List<string> Cut { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public decimal WeightFrom { get; set; }
        public decimal WeightTo { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public DynamicOrderBy OrderBy { get; set; }
    }
}