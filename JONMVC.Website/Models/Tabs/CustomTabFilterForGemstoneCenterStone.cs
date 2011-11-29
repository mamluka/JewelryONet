using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Tabs
{
    public class CustomTabFilterForGemstoneCenterStone : ICustomTabFilter
    {
        private readonly CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues> reader;

        private readonly GemstoneCenterStoneFilterValues values;

        private readonly string key = "gemstone-center-stone";

        private readonly string label = "Center Stone:";

        public CustomTabFilterForGemstoneCenterStone(CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues> reader)
        {
            this.reader = reader;

            
        }

        public CustomTabFilterViewModel ViewModel(int currentValue)
        {
            var viewModel = new CustomTabFilterViewModel()
            {
                Name = key,
                Value = currentValue,
                Values = reader.Values(),
                Label = label
            };

            return viewModel;
        }

        public DynamicSQLWhereObject DynamicSQLByFilterValue(int filterValue)
        {
            return reader.ReadDynamicSQLByValue(filterValue);
        }

        public string Key
        {
            get { return key; }
        }

       
    }
}