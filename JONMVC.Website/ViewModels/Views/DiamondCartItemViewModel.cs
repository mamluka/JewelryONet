using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class DiamondCartItemViewModel : ICartItemViewModel
    {
        private string partialName;
        private string emailTemplatePartialName;

        public string PartialName
        {
            get { return partialName; }
        }

        public string Icon { get; set; }
        public string ID { get; set; }
        public int CartID { get; set; }
        public string Desciption { get; set; }
        public string Price { get; set; }

        public bool NoActionLinkDispalyOnly { get; set; }

        public string EmailTemplatePartialName
        {
            get {
                return emailTemplatePartialName;
            }
        }

        public DiamondCartItemViewModel()
        {
            partialName = "DiamondCartItem";
        }
    }
}