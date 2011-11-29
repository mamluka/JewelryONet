using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JONMVC.Website.Tests.Unit.Tabs
{
    public class FakeTabXmlFactory
    {



         private XDocument CreateRoot()
         {
             var fakexml = new XDocument();
             fakexml.Add(
                 new XElement("tabs"));

             return fakexml;
         }

         public XDocument Regular3Tabs(string tabKey)
         {
             var fakexml = CreateRoot();

             fakexml.Root.Add(
               new XElement("tabpage",
                   new XAttribute("key",tabKey),
                   new XElement("sprite", "diamond-jewelry"),
                   new XElement("title", "Diamond Jewelry"),
                   new XElement("shorttitle", "Short Diamond Jewelry"),
                   new XElement("extratext", "extratext"),
                   new XElement("tab",
                       new XAttribute("id","engagement-rings"),
                       new XElement("caption","Rings"),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1"),
                           new XElement("values",
                               new XElement("value",new XAttribute("type","int"),"2"),
                               new XElement("value", new XAttribute("type", "int"), "7")
                               )

                           )
                       ),
                        new XElement("tab",
                       new XAttribute("id","diamod-studs"),
                       new XElement("caption","Studs"),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1"),
                           new XElement("values",
                               new XElement("value",new XAttribute("type","int"),"11"),
                               new XElement("value", new XAttribute("type", "int"), "7")
                               )

                           )
                       ),
                        new XElement("tab",
                       new XAttribute("id","diamod-pendants"),
                       new XElement("caption","Pendants"),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1"),
                           new XElement("values",
                               new XElement("value",new XAttribute("type","int"),"6"),
                               new XElement("value", new XAttribute("type", "int"), "7")
                               )

                           )
                   
                   )

               ));

             return fakexml;


         }

         public XDocument JewelDesign3Tabs(string tabKey)
         {
             var fakexml = CreateRoot();

             fakexml.Root.Add(
               new XElement("tabpage",
                   new XAttribute("key", tabKey),
                   new XElement("sprite", "jewel-design-settings"),
                   new XElement("title", "Models to choose from"),
                   new XElement("tab",
                       new XAttribute("id", "solitaire-settings"),
                       new XElement("caption", "Solitaire"),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1  and jewelsubtype_id=@2"),
                           new XElement("values",
                               new XElement("value", new XAttribute("type", "int"), "10"),
                               new XElement("value", new XAttribute("type", "int"), "7"),
                               new XElement("value", new XAttribute("type", "int"), "40")
                               )

                           )
                       ),
                        new XElement("tab",
                        new XAttribute("id", "sidestones-settings"),
                       new XElement("caption", "Sidestones"),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1  and jewelsubtype_id=@2"),
                           new XElement("values",
                               new XElement("value", new XAttribute("type", "int"), "10"),
                               new XElement("value", new XAttribute("type", "int"), "7"),
                               new XElement("value", new XAttribute("type", "int"), "43")
                               )

                           )
                       ),
                       new XElement("tab",
                        new XAttribute("id", "threestone-settings"),
                       new XElement("caption", "Sidestones"),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1  and jewelsubtype_id=@2"),
                           new XElement("values",
                               new XElement("value", new XAttribute("type", "int"), "10"),
                               new XElement("value", new XAttribute("type", "int"), "7"),
                               new XElement("value", new XAttribute("type", "int"), "42")
                               )

                           )
                       )

               ));

             return fakexml;


         }

         public XDocument SpecialTab(string tabKey)
         {
             var fakexml = CreateRoot();

             fakexml.Root.Add(
               new XElement("tabpage",
                   new XAttribute("key", tabKey),
                   new XAttribute("showtabs", "false"),
                   new XElement("title", "Models to choose from"),
                   new XElement("view", "CustomInTabContainer"),
                   new XElement("tab",
                       new XAttribute("id", TabTestsBase.SPECIAL_TABID1),
                       new XElement("where",
                           new XElement("string", "jeweltype_id = @0 and category_id = @1  and jewelsubtype_id=@2"),
                           new XElement("values",
                               new XElement("value", new XAttribute("type", "int"), "10"),
                               new XElement("value", new XAttribute("type", "int"), "7"),
                               new XElement("value", new XAttribute("type", "int"), "40")
                               )

                           )
                       )

               ));

             return fakexml;


         }

         public XDocument ValueNumberMissmatchOneTab(string tabKey)
         {
             var fakexml = CreateRoot();

             fakexml.Root.Add(
              new XElement("tabpage",
                  new XAttribute("key", tabKey),
                  new XElement("sprite", "diamond-jewelry"),
                  new XElement("title", "Diamond Jewelry"),
                  new XElement("tab",
                      new XAttribute("id", "engagement-rings"),
                      new XElement("caption", "Rings"),
                      new XElement("where",
                          new XElement("string", "jeweltype_id = @0 and category_id = @1"),
                          new XElement("values",
                              new XElement("value", new XAttribute("type", "int"), "2")
                              )

                          )
                      )

                  )

              );

             return fakexml;
         }

        public XDocument TabWithCustomGeneralTabFilter(string tabKey)
        {
            var fakexml = CreateRoot();

            fakexml.Root.Add(
             new XElement("tabpage",
                 new XAttribute("key", tabKey),
                 new XElement("sprite", "diamond-jewelry"),
                 new XElement("title", "Diamond Jewelry"),
                 new XElement("customfilters", "gemstone-center-stone"),
                 new XElement("tab",
                     new XAttribute("id", "engagement-rings"),
                     new XElement("caption", "Rings"),
                     new XElement("where",
                         new XElement("string", "jeweltype_id = @0 "),
                         new XElement("values",
                             new XElement("value", new XAttribute("type", "int"), "2")
                             )

                         )
                     )

                 )

             );

            return fakexml;
        }

        public XDocument TabWithCustomInTabFilter(string tabKey)
        {
            var fakexml = CreateRoot();

            fakexml.Root.Add(
             new XElement("tabpage",
                 new XAttribute("key", tabKey),
                 new XElement("sprite", "diamond-jewelry"),
                 new XElement("title", "Diamond Jewelry"),
                 new XElement("tab",
                     new XAttribute("id", "engagement-rings"),
                     new XElement("caption", "Rings"),
                     new XElement("customfilter", "subcategory",
                         new XAttribute("params", "7,2")),
                     new XElement("where",
                         new XElement("string", "jeweltype_id = @0 "),
                         new XElement("values",
                             new XElement("value", new XAttribute("type", "int"), "2")
                             )

                         )
                     )

                 )

             );

            return fakexml;
        }
    }
}
