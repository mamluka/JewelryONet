using System.Xml.Linq;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;

namespace JONMVC.Website.Tests.Unit.Tabs
{
    public class TabTestsBase:MapperAndFixtureBase
    {
        protected const ITabsRepository NOTUSED_TABSREPOSITORY = null;
        protected const IJewelRepository NOTUSED_JEWELRYREPOSITORY = null;
        protected const string TAB_ID2 = "diamod-studs";
        protected const string TAB_ID3 = "diamod-pendants";
        protected FakeTabXmlFactory fakeTabXmlFactory;
        protected XDocument xmldoc_regular3tabs;
        protected XDocument xmldoc_tabswithgeneralfilter;
        protected static string TAB_KEY = "testkey";
        protected static string TAB_ID1 = "engagement-rings";
        protected FakeXmlSourceFactory fakeXmlSourceFactory;
        public static string SPECIAL_TABID1 = "specialtabid";
        protected XDocument xmldoc_specialtab;
        public XDocument xmldoc_tabswithintabfilter;

        public static string TabKey
        {
            get
            {
                return TAB_KEY;
            } 
        }

        public static string TabID1
        {
            get
            {
                return TAB_ID1;
            }
        }
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            fakeTabXmlFactory = new FakeTabXmlFactory();
            xmldoc_regular3tabs = fakeTabXmlFactory.Regular3Tabs(TAB_KEY);
            xmldoc_specialtab = fakeTabXmlFactory.SpecialTab(TAB_KEY);
            xmldoc_tabswithgeneralfilter = fakeTabXmlFactory.TabWithCustomGeneralTabFilter(TabKey);
            xmldoc_tabswithintabfilter = fakeTabXmlFactory.TabWithCustomInTabFilter(TabKey);
            fakeXmlSourceFactory = new FakeXmlSourceFactory();
        }

       
    }
}