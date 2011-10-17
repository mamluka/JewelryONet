using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JONMVC.Website.Extensions;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.ViewModels.Views
{
    public interface IJewelryItemViewModel
    {
        string ID { get; set; }
        String Title { get; set; }
        prettyPhotoMedia MainJewelPicture { get; set; }
        List<prettyPhotoMedia> ExtraImages { get; set; }
        prettyPhotoMedia HiResJewelPicture { get; set; }
        string Price { get; set; }
        bool HasMovie { get; set; }
        string Movie { get; set; }
        string ItemNumber { get; set; }
        string Metal { get; set; }
        string Weight { get; set; }
        string Width { get; set; }
        List<JewelComponentInfoPart> SpecsPool { get; set; }
        bool HasSideStones { get; set; }
        bool IsBestOffer { get; set; }
        string RegularPrice { get; set; }
        bool isSpecial { get; set; }
        string YouSave { get; set; }
        List<TestimonialViewModel> Testimonials { get; set; }
        JewelType JewelType { get; set; }
    }
}