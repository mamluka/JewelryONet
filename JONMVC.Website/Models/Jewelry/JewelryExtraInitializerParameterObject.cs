using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.Jewelry
{
    public class JewelryExtraInitializerParameterObject
    {
        public string CS_Description { get; set; }
        public string CS_Type { get; set; }
        public string CS_Cut { get; set; }
        public int CS_Count { get; set; }
        public string CS_Color { get; set; }
        public string CS_Clarity { get; set; }
        public string CS_ColorFreeText { get; set; }
        public string CS_ClarityFreeText { get; set; }
        public double CS_Weight { get; set; }

        public string SS_Description { get; set; }
        public string SS_Type { get; set; }
        public string SS_Cut { get; set; }
        public int SS_Count { get; set; }
        public string SS_Color { get; set; }
        public string SS_Clarity { get; set; }
        public string SS_ColorFreeText { get; set; }
        public string SS_ClarityFreeText { get; set; }
        public double SS_Weight { get; set; }

        public bool HasSideStones { get; set; }
        public double TotalWeight { get; set; }
    }
}