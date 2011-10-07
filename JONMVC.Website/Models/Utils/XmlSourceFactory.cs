using System.Xml.Linq;

namespace JONMVC.Website.Models.Utils
{
    public class XmlSourceFactory : IXmlSourceFactory
    {
        public XDocument TabSource()
        {
            var settingManager = new SettingManager();
            return XDocument.Load(settingManager.GetTabXmlPath());
        }

        public XDocument DiamondHelpSource()
        {
            var settingManager = new SettingManager();
            return XDocument.Load(settingManager.GetDiamondHelpXmlPath());
        }
    }
}