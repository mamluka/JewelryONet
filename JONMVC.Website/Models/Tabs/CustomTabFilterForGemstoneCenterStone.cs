using JONMVC.Website.Models.Helpers;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Tabs
{
    public class CustomTabFilterForGemstoneCenterStone : ICustomTabFilter
    {
        private readonly CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues> reader;
        public CustomTabFilterViewModel ViewModel { get; private set; }

        private readonly GemstoneCenterStoneFilterValues values;

        private readonly string key = "gemstone-center-stone";

        private readonly string label = "Center Stone:";

        public CustomTabFilterForGemstoneCenterStone(CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues> reader)
        {
            this.reader = reader;

            ViewModel = new CustomTabFilterViewModel()
                            {
                                Name = key,
                                Value = 0,
                                Values = reader.Values(),
                                Label = label
                            };
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