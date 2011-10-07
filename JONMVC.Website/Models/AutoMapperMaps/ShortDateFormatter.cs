using System;
using AutoMapper;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class ShortDateFormatter:ValueFormatter<DateTime>
    {
        protected override string FormatValueCore(DateTime value)
        {
            return value.ToShortDateString();
        }
    }
}