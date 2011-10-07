using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Json.Views;
using NMoneys;

namespace JONMVC.Website.ViewModels.Json.Builders
{
    public class MediaSetsJsonModelBuilder
    {
        private readonly Jewel jewel;
        private readonly IMediaSetBuilder mediaSetBuilder;
        private readonly IWebHelpers webHelpers;

        public MediaSetsJsonModelBuilder(Jewel jewel, IMediaSetBuilder mediaSetBuilder,IWebHelpers webHelpers)
        {
            this.jewel = jewel;
            this.mediaSetBuilder = mediaSetBuilder;
            this.webHelpers = webHelpers;
        }

        public MediaSetsJsonModel Build()
        {
            var viewModel = new MediaSetsJsonModel();

            var mediaSets = mediaSetBuilder.Build(jewel.ItemNumber,jewel.MediaSetsOwnedByJewel);

            viewModel.MediaSets = mediaSets as List<JsonMedia>;

            viewModel.Price = new Money((decimal)jewel.Price, Currency.Usd).Format("{1}{0:#,0}");
            viewModel.ID = jewel.ID;
            viewModel.Title = jewel.Title;
            
            var dic = new Dictionary<string, string>();

            foreach (var jsonMedia in mediaSets)
            {
                dic[Metal.GetFullName(jsonMedia.MediaSet)] = webHelpers.RouteUrl("JewelryItem", new RouteValueDictionary()
                                                                          {
                                                                              {"id",jewel.ID},
                                                                              {"nullableMediaSet",jsonMedia.MediaSet}
                                                                          });
            }

            viewModel.MediaSetRouteLinkDictionary = dic;

            return viewModel;
        }
    }
}