using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class CustomJewelCartItemViewModel : ICartItemViewModel
    {


        public string Icon { get; set; }
        public string ID { get; set; }
        public int CartID { get; set; }
        public string Itemnumber { get; set; }
        public string DiamondID { get; set; }

        public string SettingDesciption { get; set; }
        public string DiamondDesciption { get; set; }

        public string Price { get; set; }
        public string SettingPrice { get; set; }
        public string DiamondPrice { get; set; }

        public string Size { get; set; }

        private readonly string partialName;

        public string PartialName
        {
            get { return partialName; }
        }

        public bool NoActionLinkDispalyOnly { get; set; }

        public CustomJewelCartItemViewModel()
        {
            this.partialName = "CustomJewelCartItem";
        }
    }
}