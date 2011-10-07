using System.Collections.Generic;

namespace JONMVC.Website.Models.Jewelry
{
    public class Metal : IMetal
    {
        private readonly string metalName;

        private static Dictionary<string, string> convertionDicShortToLong = new Dictionary<string, string>
                                                          {
                                                              {"plt", "Platinum"},
                                                              {"wg", "White Gold 18 Karat"},
                                                              {"yg", "Yellow Gold 18 Karat"}
                                                          };

        private static readonly Dictionary<string, string> convertionDicLongToShort = new Dictionary<string, string>
                                                          {
                                                              {"Platinum","plt"},
                                                              {"White Gold 18 Karat","wg" },
                                                              {"Yellow Gold 18 Karat","yg"}
                                                          };

        private static readonly Dictionary<JewelMediaType, string> convertionDicMediaEnumToFullName = new Dictionary<JewelMediaType, string>
                                                          {
                                                              {JewelMediaType.WhiteGold,"White Gold 18 Karat"},
                                                              {JewelMediaType.YellowGold,"Yellow Gold 18 Karat" },
                                                              {JewelMediaType.All,"White Gold 18 Karat" }
                                                          };

        public Metal(string metalName)
        {
            this.metalName = metalName;
        }

        public Metal(JewelMediaType requestedJewelMediaType, JewelMediaType currentJewelMediaType)
        {
            if (requestedJewelMediaType == JewelMediaType.All)
            {
                metalName = convertionDicMediaEnumToFullName[currentJewelMediaType];
            }
            else
            {
                metalName = convertionDicMediaEnumToFullName[requestedJewelMediaType];
            }
            
        }

        public string GetShortCode()
        {
            return convertionDicLongToShort[metalName];
        }

        #region Helpers
        
        #endregion

        public string GetFullName()
        {
            return metalName;
        }

        public static string GetFullName(JewelMediaType mediaType)
        {
            return convertionDicMediaEnumToFullName[mediaType];
        }
    }
}