using System;
using AutoMapper;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DoubleTypeZeroToNAFormatter:ValueFormatter<double>
    {
        protected override string FormatValueCore(double value)
        {
            return value == 0
                       ? "N/A"
                       : String.Format("{0:0.00}", value); 
        }
    }
}