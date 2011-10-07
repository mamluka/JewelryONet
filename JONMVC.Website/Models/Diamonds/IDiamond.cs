using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.Diamonds
{
    public interface IDiamond
    {
        int DiamondID { get; set; }


        string Shape { get; set; }

        decimal Weight { get; set; }


        string Color { get; set; }


        string Clarity { get; set; }

        decimal Price { get; set; }


        string Report { get; set; }


        string ReportURL { get; set; }

        decimal Width { get; set; }

        decimal Length { get; set; }

        decimal Height { get; set; }

        decimal Depth { get; set; }

        decimal Table { get; set; }

        string Girdle { get; set; }

        string Culet { get; set; }

        string Polish { get; set; }


        string Symmetry { get; set; }


        string Fluorescence { get; set; }

        string Cut { get; set; }

        string ReportNumber { get; set; }
    }
}