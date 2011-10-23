using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Routing;
using AutoMapper;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using NMoneys;

namespace JONMVC.Website.ViewModels.Json.Builders
{
    public class JsonDiamondsBuilder
    {
        private readonly DiamondSearchParametersGivenByJson searchParameters;
        private readonly IDiamondRepository diamondRepository;
        private readonly IJONFormatter formatter;
        private readonly IMappingEngine mapper;
        private readonly IWebHelpers webbHelpers;

        public JsonDiamondsBuilder(DiamondSearchParametersGivenByJson searchParameters, IDiamondRepository diamondRepository, IJONFormatter formatter, IMappingEngine mapper, IWebHelpers webbHelpers)
        {
            this.searchParameters = searchParameters;
            this.diamondRepository = diamondRepository;
            this.formatter = formatter;
            this.mapper = mapper;
            this.webbHelpers = webbHelpers;
        }

        public DiamondsJsonModel Build()
        {
            var jsonModel = new DiamondsJsonModel();

            var dbsearchParameters =
                mapper.Map<DiamondSearchParametersGivenByJson, DiamondSearchParameters>(searchParameters);

            var diamonds = diamondRepository.DiamondsBySearchParameters(dbsearchParameters);

            var gridrows = new List<DiamondGridRow>();

            var userData = new Dictionary<string, DiamondJsonUserData>();

            foreach (var diamond in diamonds)
            {
                var row = new DiamondGridRow();
                row.id = diamond.DiamondID;

                var cells = new string[14];
                row.cell = cells;
                //TODO Images in diamond search


                //TODO to this route depended and not hardcoded

                row.cell[0] = diamond.Shape;
                row.cell[1] = formatter.ToCaratWeight(diamond.Weight);
                row.cell[2] = diamond.Color;
                row.cell[3] = diamond.Clarity;
                row.cell[4] = diamond.Cut;
                row.cell[5] = diamond.Report;
                row.cell[6] = new Money(diamond.Price, Currency.Usd).Format("{1}{0:#,0}");

                var viewUrl = webbHelpers.RouteUrl("Diamond", new RouteValueDictionary()
                                                                  {
                                                                      {"DiamondID", diamond.DiamondID},
                                                                      {
                                                                          "SettingID",
                                                                          searchParameters.SettingID
                                                                          },
                                                                      {
                                                                          "MediaType",
                                                                          searchParameters.MediaType
                                                                          },
                                                                      {"Size", searchParameters.Size}
                                                                  });

                var addUrl = webbHelpers.RouteUrl("ChooseSetting", new RouteValueDictionary()
                                                                  {
                                                                      {"DiamondID", diamond.DiamondID},
                                                                      {
                                                                          "SettingID",
                                                                          searchParameters.SettingID
                                                                          },
                                                                      {
                                                                          "MediaType",
                                                                          searchParameters.MediaType
                                                                          },
                                                                      {"Size", searchParameters.Size}
                                                                  });

                var finishUrl = webbHelpers.RouteUrl("End", new RouteValueDictionary()
                                                                  {
                                                                      {"DiamondID", diamond.DiamondID},
                                                                      {
                                                                          "SettingID",
                                                                          searchParameters.SettingID
                                                                          },
                                                                      {
                                                                          "MediaType",
                                                                          searchParameters.MediaType
                                                                          },
                                                                      {"Size", searchParameters.Size}
                                                                  });

                var viewLink = "<a href=\"" + viewUrl  + "\" >View</a>";

                row.cell[7] = viewLink;

                var addToRingLink = "<a href=\"" + webbHelpers.RouteUrl("Diamond", new RouteValueDictionary()
                                                                                 {
                                                                                     {"DiamondID", diamond.DiamondID},
                                                                                     {
                                                                                         "SettingID",
                                                                                         searchParameters.SettingID
                                                                                         },
                                                                                     {
                                                                                         "MediaType",
                                                                                         searchParameters.MediaType
                                                                                         },
                                                                                     {"Size", searchParameters.Size}
                                                                                 }) + "\" >View</a>";


                gridrows.Add(row);

                var currentUserData = mapper.Map<Diamond, DiamondJsonUserData>(diamond);
                currentUserData.ViewURL = viewUrl;
                currentUserData.AddURL = addUrl;
                currentUserData.FinishURL = finishUrl;

                userData[diamond.DiamondID.ToString()] = currentUserData;
            }

            jsonModel.rows = gridrows;
            jsonModel.page = searchParameters.page;
            jsonModel.records = diamondRepository.TotalRecords;
            jsonModel.total = diamondRepository.LastOporationTotalPages;
            jsonModel.userdata = userData;
            
            

            return jsonModel;
        }
    }
}