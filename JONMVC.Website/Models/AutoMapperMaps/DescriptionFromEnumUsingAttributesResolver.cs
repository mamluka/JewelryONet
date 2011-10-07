using System;
using AutoMapper;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DescriptionFromEnumUsingAttributesResolver : ValueResolver<Enum, string>
    {
        protected override string ResolveCore(Enum source)
        {
            return CustomAttributes.GetDescription(source);
        }
    }
}