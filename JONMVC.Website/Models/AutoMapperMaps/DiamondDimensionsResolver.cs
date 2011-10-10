using System;
using System.Collections.Generic;
using AutoMapper;
using JONMVC.Website.Models.Diamonds;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DiamondDimensionsResolver : ValueResolver<Diamond,string>
    {
        protected override string ResolveCore(Diamond source)
        {

            //TODO figure out what is the right format for the display
            var list = new List<string>();
            if (source.Length > 0)
            {
                list.Add(String.Format("{0:0.00}", source.Length));
            }
            if (source.Width> 0)
            {
                list.Add(String.Format("{0:0.00}", source.Width));
            }
            if (source.Height > 0)
            {
                list.Add(String.Format("{0:0.00}", source.Height));
            }

            return String.Join("x", list);
        }
    }
}