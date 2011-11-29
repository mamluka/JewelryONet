using System;
using System.Collections.Generic;
using System.Linq;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Tabs
{
    public class CustomTabFilterForSubCategoryUsingDataBase : ICustomTabFilter
    {
        private readonly string[] filterParams;

        private readonly string key = "subcategory";

        private readonly string label = "Jewel Type:";

        public CustomTabFilterForSubCategoryUsingDataBase(string[] filterParams)
        {
            this.filterParams = filterParams;
        }

        public CustomTabFilterViewModel ViewModel(int currentValue)
        {
            var viewModel = new CustomTabFilterViewModel()
            {
                Name = key,
                Value = currentValue,
                Values = JewelSubCategoryValuesFromJewelCategory(filterParams),
                Label = label
            };

            return viewModel;
        }

        private List<KeyValuePair<string, int>> JewelSubCategoryValuesFromJewelCategory(string[] filterParams)
        {
           
            using (var db = new JONEntities())
            {
                var jewelType = Convert.ToInt32(filterParams[0]);

                var subcategories = db.inv_JEWELSUBTYPE_JEWEL.Where(x => x.JEWELTYPE_ID == jewelType).ToList();

                var list = subcategories.Select(subcategory => new KeyValuePair<string, int>(subcategory.LANG1_LONGDESCR, subcategory.ID)).ToList();
                list.Insert(0, new KeyValuePair<string, int>("-",0));

                return list;
            }
        }

        public DynamicSQLWhereObject DynamicSQLByFilterValue(int filterValue)
        {
            if (filterValue == 0)
            {
                var dynamic = new DynamicSQLWhereObject();
                dynamic.DoNothing();
                return dynamic;
            }
            return new DynamicSQLWhereObject("JEWELSUBTYPE_ID = @0", filterValue);
        }

        public string Key
        {
            get { return key; }
        }

      



    }
}