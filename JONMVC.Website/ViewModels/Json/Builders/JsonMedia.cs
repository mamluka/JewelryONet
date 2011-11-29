using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.ViewModels.Json.Builders
{
    public class JsonMedia:Media
    {
        public string MediaSetFullName { get; set; }

        public JsonMedia(Media media)
        {
            HandDiskPathForWebDisplay = media.HandDiskPathForWebDisplay;
            HandURLForWebDisplay = media.HandURLForWebDisplay;
            HiResDiskPathForWebDisplay = media.HiResDiskPathForWebDisplay;
            HiResURLForWebDisplay = media.HiResURLForWebDisplay;
            IconDiskPathForWebDisplay = media.IconDiskPathForWebDisplay;
            IconURLForWebDisplay = media.IconURLForWebDisplay;
            MediaSet = media.MediaSet;
            MovieDiskPathForWebDisplay = media.MovieDiskPathForWebDisplay;
            MovieURLForWebDisplay = media.MovieURLForWebDisplay;
            PictureDiskPathForWebDisplay = media.PictureDiskPathForWebDisplay;
            PictureURLForWebDisplay = media.PictureURLForWebDisplay;

            ReportURLForWebDisplay = media.ReportURLForWebDisplay;
            ReportDiskPathForWebDisplay = media.ReportDiskPathForWebDisplay;

            MediaSetFullName = Metal.GetFullName(MediaSet);
        }
    }
}