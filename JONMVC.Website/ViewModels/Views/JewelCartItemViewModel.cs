using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class JewelCartItemViewModel:ICartItemViewModel
    {
        private readonly string partialName;

        public string PartialName
        {
            get {
                return partialName;
            }
        }





        public string Icon { get; set; }
        public string ID { get; set; }
        public int CartID { get; set; }
        public string Itemnumber { get; set; }
        public string Desciption { get; set; }
        public string Price { get; set; }
        public string JewelSize { get; set; }

        public bool NoActionLinkDispalyOnly { get; set; }

        
        public JewelCartItemViewModel()
        {
            partialName = "JewelCartItem";
        }



    }
}