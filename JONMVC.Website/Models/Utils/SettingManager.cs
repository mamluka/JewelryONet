using System.Configuration;
using System.Xml.Linq;

namespace JONMVC.Website.Models.Utils
{
    public class SettingManager : ISettingManager
    {

        public string GetJewelryBaseWebPath()
        {

            return ConfigurationManager.AppSettings["JewelryWebImagePath"];
            //return "/jon-images/jewelry/";
        }

        public string GetJewelryBaseDiskPath()
        {
            return ConfigurationManager.AppSettings["JewelryDiskImagePath"];
        }

        public string GetDiamondBaseWebPath()
        {
            return ConfigurationManager.AppSettings["DiamondWebImagePath"];
        }

        public string AdminEmail()
        {
            return ConfigurationManager.AppSettings["AdminEmail"];
        }

        public string GetTabXmlPath()
        {
            return ConfigurationManager.AppSettings["TabXMLPath"];
        }

        public string GetDiamondHelpXmlPath()
        {
            return ConfigurationManager.AppSettings["DiamondHelpXMLPath"];
        }
    }
}