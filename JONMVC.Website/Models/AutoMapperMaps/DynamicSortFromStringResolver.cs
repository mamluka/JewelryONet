using System;
using System.Collections.Generic;
using AutoMapper;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Json.Builders;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DynamicSortFromStringResolver : ValueResolver<DiamondSearchParametersGivenByJson, DynamicOrderBy>
    {
        private Dictionary<string,string> orderFieldMapper = new Dictionary<string, string>
                                                                 {
                                                                     {"price","totalprice"},
                                                                     {"weight","weight"},
                                                                     {"id","diamondid"}
                                                                 }; 
        protected override DynamicOrderBy ResolveCore(DiamondSearchParametersGivenByJson source)
        {
            if (String.IsNullOrWhiteSpace(source.sord))
            {
                return new DynamicOrderBy("id", "asc");
            }
            return new DynamicOrderBy(orderFieldMapper[source.sidx],source.sord);
        }
    }
}