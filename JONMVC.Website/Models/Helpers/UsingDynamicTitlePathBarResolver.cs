using System;
using System.Collections.Generic;

namespace JONMVC.Website.Models.Helpers
{
    public class UsingDynamicTitlePathBarResolver : PathBarResolver<dynamic>
    {
        public override List<KeyValuePair<string, string>> GeneratePathBarDictionary(dynamic model)
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

                throw new Exception("When try to create the pathbar for the page:"+model.Title + " an error occured\r\n" + ex.Message);
            }
        }
    }
}