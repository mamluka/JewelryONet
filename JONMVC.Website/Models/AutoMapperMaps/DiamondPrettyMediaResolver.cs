using System;
using AutoMapper;
using JONMVC.Website.Extensions;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DiamondPrettyMediaResolver : ValueResolver<Diamond, prettyPhotoMedia>
    {
        private readonly DiamondMediaType highResolutionPicture;
        private readonly ISettingManager settingManager;

        public DiamondPrettyMediaResolver(DiamondMediaType highResolutionPicture, ISettingManager settingManager)
        {
            this.highResolutionPicture = highResolutionPicture;
            this.settingManager = settingManager;
        }

        protected override prettyPhotoMedia ResolveCore(Diamond source)
        {
            var image = settingManager.GetDiamondBaseWebPath() + source.Shape.ToLower() + ".png";
            var largeimage = settingManager.GetDiamondBaseWebPath() + source.Shape.ToLower() + "-hires.png";
            switch (highResolutionPicture)
            {
                case DiamondMediaType.Picture:
                    return new prettyPhotoMedia(image,largeimage,"");
                case DiamondMediaType.HighResolutionPicture:
                    return new prettyPhotoMedia(largeimage, largeimage, "");
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }
    }
}