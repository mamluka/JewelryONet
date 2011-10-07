using System;
using System.Collections.Generic;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Helpers
{
    public class UsingTitlePathBarResolver : PathBarResolver<PageViewModelBase>
    {
        public override List<KeyValuePair<string, string>> GeneratePathBarDictionary(PageViewModelBase model)
        {
            try
            {
                var list = new List<KeyValuePair<string, string>>();



                var currentPage = new KeyValuePair<string, string>(model.PageTitle, "");

                list.Add(currentPage);

                return list;
            }
            catch (Exception ex)
            {

                throw new Exception("When try to create the pathbar for the page:" + model.PageTitle + " an error occured\r\n" + ex.Message);
            }
        }
    }
}