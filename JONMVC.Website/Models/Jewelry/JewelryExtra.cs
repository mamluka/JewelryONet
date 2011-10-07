using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.Jewelry
{
    public class JewelryExtra
    {
        public JewelComponentProperty CS { get; set; }
        public JewelComponentProperty SS { get; set; }

        public bool HasSideStones { get; set; }
        public double TotalWeight { get; set; }

        public JewelryExtra(JewelryExtraInitializerParameterObject initJewelExtra)
        {
            CS = new JewelComponentProperty()
                     {
                         Description = initJewelExtra.CS_Description,
                         Clarity = initJewelExtra.CS_Clarity,
                         ClarityFreeText = initJewelExtra.CS_ClarityFreeText,
                         Color = initJewelExtra.CS_Color,
                         ColorFreeText = initJewelExtra.CS_ColorFreeText,
                         Count = initJewelExtra.CS_Count,
                         Cut = initJewelExtra.CS_Cut,
                         Type = initJewelExtra.CS_Type,
                         Weight = initJewelExtra.CS_Weight
                         
                     };

            SS = new JewelComponentProperty()
                     {
                         Description = initJewelExtra.SS_Description,
                         Clarity = initJewelExtra.SS_Clarity,
                         ClarityFreeText = initJewelExtra.SS_ClarityFreeText,
                         Color = initJewelExtra.SS_Color,
                         ColorFreeText = initJewelExtra.SS_ColorFreeText,
                         Count = initJewelExtra.SS_Count,
                         Cut = initJewelExtra.SS_Cut,
                         Type = initJewelExtra.SS_Type,
                         Weight = initJewelExtra.SS_Weight
                     };

            HasSideStones = initJewelExtra.HasSideStones;
            TotalWeight = initJewelExtra.TotalWeight;
        }

        public class JewelComponentProperty
        {
            public string Description { get; set; }
            public string Type { get; set; }
            public string Cut { get; set; }
            public double Weight { get; set; }
            public int Count { get; set; }
            public string Color { get; set; }
            public string Clarity { get; set; }
            public string ColorFreeText { get; set; }
            public string ClarityFreeText { get; set; }

        }
    }

    
}