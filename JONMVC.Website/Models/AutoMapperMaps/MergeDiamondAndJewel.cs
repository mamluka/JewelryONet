using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class MergeDiamondAndJewel:MergeTwoObjectsForAutoMapper<Diamond,Jewel>
    {

        public override Diamond First { get; set; }
        public override Jewel Second { get; set; }
    }
}