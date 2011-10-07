using System;
using System.Linq;
using System.Xml.Linq;

namespace JONMVC.Website.Models.Tabs
{
    public class TabBase
    {
        protected XDocument tabsource;

        protected XElement GetRawTabPageByKey(string tabKey)
        {
            var tabpage =
                tabsource.Root.Elements("tabpage").Attributes().Where(m => m.Value == tabKey).FirstOrDefault();

            if (tabpage == null)
            {
                throw new IndexOutOfRangeException("bad key");
            }
            return tabpage.Parent;
        }

        internal static NullReferenceException ThrowBadXML()
        {
            throw new NullReferenceException("bad format of tab xml");
        }
    }
}