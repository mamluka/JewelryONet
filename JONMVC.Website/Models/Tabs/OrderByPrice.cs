using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Tabs
{
    public enum OrderByPrice
    {
       
        [Description("Low to high")]
        [OrderByDirection("asc")]
        [OrderByField("price")]
        LowToHigh=1,
        [Description("High to low")]
        [OrderByDirection("desc")]
        [OrderByField("price")]
        HighToLow=2
    }

}