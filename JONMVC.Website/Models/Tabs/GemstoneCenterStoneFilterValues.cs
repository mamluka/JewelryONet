using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Tabs
{
    public enum GemstoneCenterStoneFilterValues
    {
        [Description("-")] 
        All = 0,
        
        [FilterFieldAndValue("cs_type", "ruby")] [Description("Ruby")] 
        Ruby = 1,
        
        [FilterFieldAndValue("cs_type", "sapphire")] [Description("Sapphire")] 
        Sapphire = 2,
        
        [FilterFieldAndValue("cs_type", "emerald")] [Description("Emerald")] 
        Emerald = 3
    }
}