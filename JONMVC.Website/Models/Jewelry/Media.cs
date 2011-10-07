using System;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Jewelry
{
    public class Media
    {
        public string IconURLForWebDisplay { get; set; }

        public string PictureURLForWebDisplay { get; set; }

        public string HiResURLForWebDisplay { get; set; }

        public string HandURLForWebDisplay { get; set; }

        public string MovieURLForWebDisplay { get; set; }

        public string PictureDiskPathForWebDisplay { get; set; }

        public string IconDiskPathForWebDisplay { get; set; }

        public string HiResDiskPathForWebDisplay { get; set; }

        public string HandDiskPathForWebDisplay { get; set; }

        public string MovieDiskPathForWebDisplay { get; set; }

        public JewelMediaType MediaSet { get; set; }
    }
}