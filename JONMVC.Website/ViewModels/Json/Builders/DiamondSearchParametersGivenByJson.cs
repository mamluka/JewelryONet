using System.Collections.Generic;
using JONMVC.Website.Models.JewelDesign;

namespace JONMVC.Website.ViewModels.Json.Builders
{
    public class DiamondSearchParametersGivenByJson : CustomJewelPersistenceForSetting
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
        public int page { get; set; }
        public int rows { get; set; }
        public string sidx { get; set; }
        public string sort { get; set; }
    }
}