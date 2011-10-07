using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Json.Builders;

namespace JONMVC.Website.Models.JewelryItem
{
    public class MediaSetBuilder : IMediaSetBuilder
    {
        private readonly ISettingManager settingManager;
        private readonly IFileSystem fileSystem;
        private string itemNumber;
        private JewelMediaType jewelMediaSets;


        public MediaSetBuilder(ISettingManager settingManager, IFileSystem fileSystem)
        {

            this.settingManager = settingManager;
            this.fileSystem = fileSystem;
        }

        public IEnumerable<JsonMedia> Build(string itemNumberForSet, JewelMediaType mediaSetsOwnedByJewel)
        {
            this.jewelMediaSets = mediaSetsOwnedByJewel;
            this.itemNumber = itemNumberForSet;

            var list = new List<JsonMedia>();



            switch (mediaSetsOwnedByJewel)
            {
                case JewelMediaType.All:
                    {
                        var media1 = BuildAndVerifyMediaByMediaSet(JewelMediaType.YellowGold);
                        if (media1 != null)
                        {
                            list.Add(media1);
                        }

                        var media2 = BuildAndVerifyMediaByMediaSet(JewelMediaType.WhiteGold);
                        if (media2 != null)
                        {
                            list.Add(media2);
                        }
                    }
                    break;
                case JewelMediaType.YellowGold:
                    {
                        var media1 = BuildAndVerifyMediaByMediaSet(JewelMediaType.YellowGold);
                        if (media1 != null)
                        {
                            list.Add(media1);
                        }

                    }
                    break;
                case JewelMediaType.WhiteGold:
                    {
                        var media2 = BuildAndVerifyMediaByMediaSet(JewelMediaType.WhiteGold);
                        if (media2 != null)
                        {
                            list.Add(media2);
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            



            return list;
        }

        private JsonMedia BuildAndVerifyMediaByMediaSet(JewelMediaType mediaSet)
        {
            var mediaFactory = new MediaFactory(itemNumber, settingManager);
            var mediaVerifier = new MediaVerifier(fileSystem);
            mediaFactory.ChangeMediaSet(mediaSet, jewelMediaSets);
            var media = mediaVerifier.Verify(mediaFactory.BuildMedia());

            JsonMedia jsonMedia = null;

            if (media != null)
            {
                jsonMedia = new JsonMedia(media);
            }

            return jsonMedia;
        }
    }
}