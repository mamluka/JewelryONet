using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Tabs
{
    public class TabsRepository:TabBase, ITabsRepository
    {

        public TabsRepository(IXmlSourceFactory sourceFactory)
        {
            var xmldoc = sourceFactory.TabSource();
            if (xmldoc == null)
            {
                throw new ArgumentNullException("The xmldoc was null");
            }

            base.tabsource = xmldoc;

           // LoadTabsFromXML();
        }
        #region Public
         public List<Tab> GetTabsCollectionByKey(string tabKey)
         {

             var list = new List<Tab>();

             //Get the collection from xml

             try
             {

                 var tabpage = GetRawTabPageByKey(tabKey);


                 var tabs =
                     from tab in tabpage.Elements("tab")
                     select new
                                {
                                    Caption = tab.Element("caption").Value,
                                    ID = tab.Attribute("id").Value,

                                    //whereString = tab.Element("where").Element("string").Value,
                                    //whereValues = from cond in tab.Element("where").Element("values").Elements("value") select new
                                    //                                                                              {
                                    //                                                                                  Value = cond.Value
                                    //                                                                              }



                                };



                 var tabIndex = 1;
                 foreach (var tab in tabs)
                 {
                     var tabobj = new Tab(tab.Caption, tab.ID,tabIndex);
                     tabIndex++;

                     list.Add(tabobj); ;
                 }
             }
             catch (IndexOutOfRangeException ex)
             {
                 throw new IndexOutOfRangeException(ex.Message);
             }
             catch (NullReferenceException)
             {
                 ThrowBadXML();
             }

            
                



            return list;
        }

        


        #endregion

      
        
    }
}