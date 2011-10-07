using System;
using AutoMapper;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class WeightFormatter:ValueFormatter<decimal>
    {
        protected override string FormatValueCore(decimal value)
        {
            return String.Format("{0:0.00} Ct.", value);
        }
    }
}