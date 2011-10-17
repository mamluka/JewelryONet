using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JONMVC.Website.Extensions;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;

namespace JONMVC.Website.ViewModels.Views
{
    public class JewelryItemViewModel : PageViewModelBase, IJewelryItemViewModel, IJewelBeseInfo
    {
        public string ID { get; set; }

        public String Title { get; set; }

        public prettyPhotoMedia MainJewelPicture { get; set; }

        public List<prettyPhotoMedia> ExtraImages { get; set; }

        public prettyPhotoMedia HiResJewelPicture { get; set; }

        public string Price { get; set; }

        public bool HasMovie { get; set; }

        public string Movie { get; set; }

        public string ItemNumber { get; set; }

        public string Metal { get; set; }

        public string Weight { get; set; }

        public string Width { get; set; }

        public List<JewelComponentInfoPart> SpecsPool { get; set; }

        public bool HasSideStones { get; set; }

        public bool IsBestOffer { get; set; }

        public string RegularPrice { get; set; }

        public bool isSpecial { get; set; }

        public string YouSave { get; set; }

        public List<TestimonialViewModel> Testimonials { get; set; }

        public JewelType JewelType { get; set; }
    }
}