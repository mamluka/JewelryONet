using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class FromJsonToDataBase : ValueResolver<List<string>, List<string>>
    {
        private readonly string propertyName;

        private readonly Dictionary<string, Dictionary<string, string>> convertionDictionary = new Dictionary
            <string, Dictionary<string, string>>()
                                                                                                   {
                                                                                                       {
                                                                                                           "shape",
                                                                                                           new Dictionary
                                                                                                           <string,
                                                                                                           string>()
                                                                                                               {
                                                                                                                   {
                                                                                                                       "Round"
                                                                                                                       ,
                                                                                                                       "Round"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Cushion"
                                                                                                                       ,
                                                                                                                       "Cushion"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Emerald"
                                                                                                                       ,
                                                                                                                       "Emerald"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Marquise"
                                                                                                                       ,
                                                                                                                       "Marquise"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Oval"
                                                                                                                       ,
                                                                                                                       "Oval"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Pear"
                                                                                                                       ,
                                                                                                                       "Pear"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Princess"
                                                                                                                       ,
                                                                                                                       "Princess"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Radiant"
                                                                                                                       ,
                                                                                                                       "Radiant"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Asscher"
                                                                                                                       ,
                                                                                                                       "Asscher"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Heart"
                                                                                                                       ,
                                                                                                                       "Heart"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Triangular"
                                                                                                                       ,
                                                                                                                       "Triangular"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Baguette"
                                                                                                                       ,
                                                                                                                       "Baguette"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "Shield"
                                                                                                                       ,
                                                                                                                       "Shield"
                                                                                                                       },
                                                                                                               }
                                                                                                           },
                                                                                                       {
                                                                                                           "clarity",
                                                                                                           new Dictionary
                                                                                                           <string,
                                                                                                           string>()
                                                                                                               {
                                                                                                                   {
                                                                                                                       "I1"
                                                                                                                       ,
                                                                                                                       "I1"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "I2"
                                                                                                                       ,
                                                                                                                       "I2"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "IF"
                                                                                                                       ,
                                                                                                                       "IF"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "SI1"
                                                                                                                       ,
                                                                                                                       "SI1"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "SI2"
                                                                                                                       ,
                                                                                                                       "SI2"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "SI3"
                                                                                                                       ,
                                                                                                                       "SI3"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "VS1"
                                                                                                                       ,
                                                                                                                       "VS1"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "VS2"
                                                                                                                       ,
                                                                                                                       "VS2"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "VVS1"
                                                                                                                       ,
                                                                                                                       "VVS1"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "VVS2"
                                                                                                                       ,
                                                                                                                       "VVS2"
                                                                                                                       },
                                                                                                               }
                                                                                                           },
                                                                                                       {
                                                                                                           "color",
                                                                                                           new Dictionary
                                                                                                           <string,
                                                                                                           string>()
                                                                                                               {
                                                                                                                   {
                                                                                                                       "D"
                                                                                                                       ,
                                                                                                                       "D"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "E"
                                                                                                                       ,
                                                                                                                       "E"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "F"
                                                                                                                       ,
                                                                                                                       "F"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "G"
                                                                                                                       ,
                                                                                                                       "G"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "H"
                                                                                                                       ,
                                                                                                                       "H"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "I"
                                                                                                                       ,
                                                                                                                       "I"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "J"
                                                                                                                       ,
                                                                                                                       "J"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "K"
                                                                                                                       ,
                                                                                                                       "K"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "L"
                                                                                                                       ,
                                                                                                                       "L"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "M"
                                                                                                                       ,
                                                                                                                       "M"
                                                                                                                       },
                                                                                                               }
                                                                                                           },
                                                                                                       {
                                                                                                           "cut",
                                                                                                           new Dictionary
                                                                                                           <string,
                                                                                                           string>()
                                                                                                               {
                                                                                                                   {
                                                                                                                       "EX"
                                                                                                                       ,
                                                                                                                       "Excellent"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "VG"
                                                                                                                       ,
                                                                                                                       "Very Good"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "GVG"
                                                                                                                       ,
                                                                                                                       "Good to Very Good"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "G"
                                                                                                                       ,
                                                                                                                       "Good"
                                                                                                                       },
                                                                                                               }
                                                                                                           },
                                                                                                       {
                                                                                                           "report",
                                                                                                           new Dictionary
                                                                                                           <string,
                                                                                                           string>()
                                                                                                               {
                                                                                                                   {
                                                                                                                       "GIA"
                                                                                                                       ,
                                                                                                                       "GIA"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "EGL"
                                                                                                                       ,
                                                                                                                       "EGL"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "HRD"
                                                                                                                       ,
                                                                                                                       "HRD"
                                                                                                                       },
                                                                                                                   {
                                                                                                                       "IGI"
                                                                                                                       ,
                                                                                                                       "IGI"
                                                                                                                       },
                                                                                                               }
                                                                                                           },
                                                                                                   };



        public FromJsonToDataBase(string propertyName)
        {
            this.propertyName = propertyName;
        }

        protected override List<string> ResolveCore(List<string> source)
        {
            var jsonListFromWebRequest = source;

            var databaseStrickValues = new List<string>();

            if (jsonListFromWebRequest != null)
                databaseStrickValues.AddRange(jsonListFromWebRequest.Select(GivesAConvertionValue));

            return databaseStrickValues;

        }

        private string GivesAConvertionValue(string key)
        {
            if (convertionDictionary[propertyName].ContainsKey(key))
            {
                return convertionDictionary[propertyName][key];
            }
            throw new ArgumentException("no such value in the mapping dictionary");
        }
    }
}