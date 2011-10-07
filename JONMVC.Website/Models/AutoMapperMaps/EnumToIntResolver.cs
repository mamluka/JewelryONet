using System;
using AutoMapper;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class EnumToIntResolver:ValueResolver<Enum,int>
    {
        protected override int ResolveCore(Enum source)
        {
            return Convert.ToInt32(source);
        }
    }
}