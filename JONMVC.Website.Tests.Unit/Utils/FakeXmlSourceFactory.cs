using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Tabs;

namespace JONMVC.Website.Tests.Unit.Utils
{
    public class FakeXmlSourceFactory : IXmlSourceFactory
    {
        public XDocument TabSource()
        {
            var fakeXmlTabFactory = new FakeTabXmlFactory();
            return fakeXmlTabFactory.Regular3Tabs(TabTestsBase.TabKey);
        }
        public XDocument DiamondHelpSource()
        {

            var fakexml = new XDocument();
            fakexml.Add(
                new XElement("diamondhelp"));

            fakexml.Root.Add(
                new XElement("helppage",
                             new XAttribute("key", "cut"), new XAttribute("title", "Cut"),
                             new XElement("helppart",
                                          new XAttribute("value", "EX"),
                                          new XElement("body", new XCData("this is the vg"))),

                             new XElement("helppart",
                                          new XAttribute("value", "VVG"),
                                          new XElement("body", new XCData("this is the vg"))),

                             new XElement("helppart",
                                          new XAttribute("value", "VG"),
                                          new XElement("body", new XCData("this is the vg"))),

                             new XElement("helppart",
                                          new XAttribute("value", "FAIR"),
                                          new XElement("body", new XCData("this is the vg")))

                    ),

                new XElement("helppage",
                             new XAttribute("key", "color"), new XAttribute("title", "Color"),
                             new XElement("helppart",
                                          new XAttribute("value", "E"),
                                          new XElement("body", new XCData("this is the vg"))),
                             new XElement("helppart",
                                          new XAttribute("value", "F"),
                                          new XElement("body", new XCData("this is the vg"))),
                             new XElement("helppart",
                                          new XAttribute("value", "G"),
                                          new XElement("body", new XCData("this is the vg"))),
                             new XElement("helppart",
                                          new XAttribute("value", "H"),
                                          new XElement("body", new XCData("help for H")))
                    ),

                new XElement("helppage",
                             new XAttribute("key", "clarity"), new XAttribute("title", "Clarity"),
                             new XElement("helppart",
                                          new XAttribute("value", "VS1"),
                                          new XElement("body", new XCData("this is the vg"))),

                             new XElement("helppart",
                                          new XAttribute("value", "VS2"),
                                          new XElement("body", new XCData("this is the vg"))),

                             new XElement("helppart",
                                          new XAttribute("value", "VVS1"),
                                          new XElement("body", new XCData("this is the vg"))),

                             new XElement("helppart",
                                          new XAttribute("value", "VVS2"),
                                          new XElement("body", new XCData("this is the vg")))

                    )




                );

            return fakexml;
        }
    }
}
