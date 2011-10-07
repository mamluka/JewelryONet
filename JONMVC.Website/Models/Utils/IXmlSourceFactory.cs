using System.Xml.Linq;

namespace JONMVC.Website.Models.Utils
{
    public interface IXmlSourceFactory
    {
        XDocument TabSource();
        XDocument DiamondHelpSource();
    }
}