using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JONMVC.Website.Models.Jewelry
{
    public class Metal : IMetal
    {
        private readonly string metalName;

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

        public Metal(JewelMediaType requestedJewelMediaType, JewelMediaType currentJewelMediaType, string metal)
        {
            if (requestedJewelMediaType == JewelMediaType.All)
            {
                metalName = convertionDicMediaEnumToFullName[currentJewelMediaType];
            }
            else
            {
                metalName = convertionDicMediaEnumToFullName[requestedJewelMediaType];
            }

            var matchedMetalKarat = Regex.Match(metal, "\\d{1,2}").Value;
            if (!string.IsNullOrEmpty(matchedMetalKarat))
            {
                metalName = Regex.Replace(metalName, "\\d{1,2}", matchedMetalKarat);
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