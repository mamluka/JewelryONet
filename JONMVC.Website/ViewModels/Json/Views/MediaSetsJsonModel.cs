using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Json.Builders;

namespace JONMVC.Website.ViewModels.Json.Views
{
    public class MediaSetsJsonModel
    {
        public List<JsonMedia> MediaSets { get; set; }
        public string Price { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }

        public Dictionary<string, string> MediaSetRouteLinkDictionary { get; set; }
    }
}