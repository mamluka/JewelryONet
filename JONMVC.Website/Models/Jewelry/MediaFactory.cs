using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Jewelry
{
    public class MediaFactory
    {
        private readonly string itemNumber;
        private readonly IMetal metal;
        private readonly ISettingManager settingManager;

        private const string ImageExtension = ".jpg";
        private const string MovieExtension = ".flv";

        private string conventionMetalPrefix;

        private JewelMediaType currentMediaSet;

        public MediaFactory(string itemNumber, ISettingManager settingManager)
        {
            this.itemNumber = itemNumber;
            this.settingManager = settingManager;
            conventionMetalPrefix = "wg";
            currentMediaSet = JewelMediaType.WhiteGold;
        }

        public Media BuildMedia()
        {
            var media = new Media();
            //paths from the setting manager end with "/"

            media.IconURLForWebDisplay = FillParametersIntoPathPattern(GetBaseWebPath(), itemNumber, "icon", conventionMetalPrefix, ImageExtension);
            media.PictureURLForWebDisplay = FillParametersIntoPathPattern(GetBaseWebPath(), itemNumber, "pic", conventionMetalPrefix, ImageExtension);
            media.HiResURLForWebDisplay = FillParametersIntoPathPattern(GetBaseWebPath(), itemNumber, "hires", conventionMetalPrefix, ImageExtension);
            media.HandURLForWebDisplay = FillParametersIntoPathPattern(GetBaseWebPath(), itemNumber, "hand", conventionMetalPrefix, ImageExtension);
            media.MovieURLForWebDisplay = FillParametersIntoPathPattern(GetBaseWebPath(), itemNumber, "mov", conventionMetalPrefix, MovieExtension);

            media.IconDiskPathForWebDisplay = FillParametersIntoPathPattern(GetBaseDiskPath(), itemNumber, "icon", conventionMetalPrefix, ImageExtension);
            media.PictureDiskPathForWebDisplay = FillParametersIntoPathPattern(GetBaseDiskPath(), itemNumber, "pic", conventionMetalPrefix, ImageExtension);
            media.HiResDiskPathForWebDisplay = FillParametersIntoPathPattern(GetBaseDiskPath(), itemNumber, "hires", conventionMetalPrefix, ImageExtension);
            media.HandDiskPathForWebDisplay = FillParametersIntoPathPattern(GetBaseDiskPath(), itemNumber, "hand", conventionMetalPrefix, ImageExtension);
            media.MovieDiskPathForWebDisplay = FillParametersIntoPathPattern(GetBaseDiskPath(), itemNumber, "mov", conventionMetalPrefix, MovieExtension);

            media.MediaSet = currentMediaSet;
            return media;
        }

        public void ChangeMediaSet(JewelMediaType requestedJewelMediaType, JewelMediaType currentJewelMediaType)
        {

            var fromEnumToFileNamingConventionDictionary = new Dictionary<JewelMediaType, string>()
                                                  {
                                                      {JewelMediaType.WhiteGold, "wg"},
                                                      {JewelMediaType.YellowGold, "yg"},
                                                      {JewelMediaType.All, "wg"}
                                                  };

            if (requestedJewelMediaType == JewelMediaType.All)
            {
                conventionMetalPrefix = fromEnumToFileNamingConventionDictionary[currentJewelMediaType];

                currentMediaSet = currentJewelMediaType == JewelMediaType.All ? JewelMediaType.WhiteGold : currentJewelMediaType;
            }
            else
            {
                conventionMetalPrefix =  fromEnumToFileNamingConventionDictionary[requestedJewelMediaType];
                currentMediaSet = requestedJewelMediaType;
            }
        }

       
  


        #region Helpers
        public string FillParametersIntoPathPattern(string path, string itemnumber, string pictype, string metal, string extension)
        {
            const string pattern = "{0}{1}-{2}-{3}{4}";

            return String.Format(pattern, path, itemnumber, pictype, metal, extension);
        }

        private string GetBaseWebPath()
        {
            return settingManager.GetJewelryBaseWebPath();
        }

        private string GetBaseDiskPath()
        {
            return settingManager.GetJewelryBaseDiskPath();
        }
        #endregion
    }
}