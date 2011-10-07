using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.JewelDesign
{
    public class CustomJewelPersistenceBase
    {
        public int DiamondID { get; set; }

        public int SettingID { get; set; }

        public string Size { get; set; }

        public JewelMediaType MediaType { get; set; }
    }
}