using AutoMapper;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Diamonds;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DiamondDesriptionResolver : ValueResolver<v_jd_diamonds, string>
    {

        protected override string ResolveCore(v_jd_diamonds source)
        {
            return "A " + source.weight.ToString() + " Ct. " + source.shape + " " + source.color + "/" + source.clarity + " Diamond";
        }
    }
}