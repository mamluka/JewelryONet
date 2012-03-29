using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JONMVC.Website.Models.Jewelry
{
    public class Metal : IMetal
    {
        private readonly string metalName;

        private static readonly Dictionary<string, string> ConvertionDicLongToShort = new Dictionary<string, string>
                                                          {
                                                              {"Platinum","plt"},
                                                              {"White Gold 18 Karat","wg" },
                                                              {"Yellow Gold 18 Karat","yg"}
                                                          };

        private static readonly Dictionary<JewelMediaType, string> ConvertionDicMediaEnumToFullName = new Dictionary<JewelMediaType, string>
                                                          {
                                                              {JewelMediaType.WhiteGold,"White Gold 18 Karat"},
                                                              {JewelMediaType.YellowGold,"Yellow Gold 18 Karat" },
                                                              {JewelMediaType.All,"White Gold 18 Karat" }
                                                          };

        public Metal(string metalName)
        {
            this.metalName = metalName;
        }

        public Metal(JewelMediaType requestedJewelMediaType, JewelMediaType currentJewelMediaType, string metal)
        {
            metalName = requestedJewelMediaType == JewelMediaType.All ? ConvertionDicMediaEnumToFullName[currentJewelMediaType] : ConvertionDicMediaEnumToFullName[requestedJewelMediaType];

            var matchedMetalKarat = Regex.Match(metal, "\\d{1,2}").Value;
            if (!string.IsNullOrEmpty(matchedMetalKarat))
            {
                metalName = Regex.Replace(metalName, "\\d{1,2}", matchedMetalKarat);
            }



        }

        public string GetShortCode()
        {
            return ConvertionDicLongToShort[metalName];
        }

        #region Helpers
        
        #endregion

        public string GetFullName()
        {
            return metalName;
        }

        public static string GetFullName(JewelMediaType mediaType)
        {
            return ConvertionDicMediaEnumToFullName[mediaType];
        }
    }
}