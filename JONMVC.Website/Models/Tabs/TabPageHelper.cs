using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using JONMVC.Website.Models.Helpers;

namespace JONMVC.Website.Models.Tabs
{
    public class TabPageHelper : TabBase
    {
        public TabPageHelper(XDocument tabsource)
        {
            base.tabsource = tabsource;
        }

       

 
    }
}