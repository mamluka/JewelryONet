using System;
using AutoMapper;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class DiamondMediaResolver : ValueResolver<Diamond,string>
    {
        private readonly DiamondMediaType mediaType;
        private readonly ISettingManager settingManager;

        public DiamondMediaResolver(DiamondMediaType mediaType, ISettingManager settingManager)
        {
            this.mediaType = mediaType;
            this.settingManager = settingManager;
        }

        protected override string ResolveCore(Diamond source)
        {
            //TODO add the alt to the pictures
            //TODO remove duplication for both resolvers to have the switch
            switch (mediaType)
            {
                case DiamondMediaType.Picture:
                    return settingManager.GetDiamondBaseWebPath() + source.Shape.ToLower() + ".png";
                case DiamondMediaType.HighResolutionPicture:
                    return settingManager.GetDiamondBaseWebPath() + source.Shape.ToLower() + "-hires.png";
                case DiamondMediaType.Icon:
                    return settingManager.GetDiamondBaseWebPath() + source.Shape.ToLower() + "-icon.png";
                default:
                    throw new ArgumentOutOfRangeException();
            }
            

        }
    }
}