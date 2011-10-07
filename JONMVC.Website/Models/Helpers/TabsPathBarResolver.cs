using System;
using System.Collections.Generic;
using System.Web.Routing;
using JONMVC.Website.ViewModels.Views;
using System.Linq;
namespace JONMVC.Website.Models.Helpers
{
    public class TabsPathBarResolver:PathBarResolver<ITabsViewModel>
    {
    

        public override List<KeyValuePair<string, string>> GeneratePathBarDictionary(ITabsViewModel model)
        {

            try
            {
                var list = new List<KeyValuePair<string, string>>();

                if (model.IsShowTabs)
                {


                    var mainTabLink = new KeyValuePair<string, string>(model.ShortTitle,
                                                                       webHelpers.RouteUrl("Tabs",
                                                                                           new RouteValueDictionary()
                                                                                               {
                                                                                                   {
                                                                                                       "TabKey",
                                                                                                       model.TabKey
                                                                                                       }
                                                                                               }));
                    list.Add(mainTabLink);


                    var currentTabNonLink =
                        new KeyValuePair<string, string>(
                            model.Tabs.Where(x => x.Id == model.TabId).SingleOrDefault().Caption, "");
                    list.Add(currentTabNonLink);
                }
                else
                {
                    var mainTabLink = new KeyValuePair<string, string>(model.Title,"");
                    list.Add(mainTabLink);
                }


               

                return list;
            }
            catch (Exception ex)
            {

                throw new Exception("When try to create the path bar links for tabkey:" + model.TabKey + " and tabid:" +model.TabId + " an error occured\r\n" + ex.Message);
            }

        }
    }
}