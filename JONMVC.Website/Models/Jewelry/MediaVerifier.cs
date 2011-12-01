using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.Jewelry
{
    public class MediaVerifier
    {
        private readonly IFileSystem fileSystem;

        public MediaVerifier(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Media Verify(Media media)
        {
            if (!fileSystem.File.Exists(media.IconDiskPathForWebDisplay))
            {
                return null;
            }

            if (!fileSystem.File.Exists(media.PictureDiskPathForWebDisplay))
            {
                return null;
            }

            if (!fileSystem.File.Exists(media.HiResDiskPathForWebDisplay))
            {
                media.HiResURLForWebDisplay = null;
            }

            if (!fileSystem.File.Exists(media.HandDiskPathForWebDisplay))
            {
                media.HandURLForWebDisplay = null;
            }

            if (!fileSystem.File.Exists(media.MovieDiskPathForWebDisplay))
            {
                media.MovieURLForWebDisplay = null;
            }

            if (!fileSystem.File.Exists(media.ReportDiskPathForWebDisplay))
            {
                media.ReportURLForWebDisplay = null;
            }

            return media;
        }
    }
}