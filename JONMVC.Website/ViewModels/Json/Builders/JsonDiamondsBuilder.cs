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

            var dbsearchParameters = mapper.Map<DiamondSearchParametersGivenByJson, DiamondSearchParameters>(searchParameters);

            var diamonds = diamondRepository.DiamondsBySearchParameters(dbsearchParameters);

            var gridrows = new List<DiamondGridRow>();

            foreach (var diamond in diamonds)
            {
                var row = new DiamondGridRow();
                row.id = diamond.DiamondID;

                var cells = new string[14];
                row.cell = cells;

//                row.cell[0] = diamond.DiamondID.ToString();
//                row.cell[1] = diamond.Shape;
//                row.cell[2] = formatter.ToCaratWeight(diamond.Weight) ;
//                row.cell[3] = diamond.Color;
//                row.cell[4] = diamond.Clarity;
//                row.cell[5] = diamond.Cut;
//                row.cell[6] = formatter.FormatTwoDecimalPoints(diamond.Depth);
//                row.cell[7] = formatter.FormatTwoDecimalPoints(diamond.Table);
//                row.cell[8] = diamond.Polish + "/" + diamond.Symmetry;
//                row.cell[9] = diamond.Fluorescence;
//                row.cell[10] = diamond.Report;
//                //TODO Images in diamond search
//                row.cell[11] = "Image";
//
//                row.cell[12] = new Money(diamond.Price, Currency.Usd).Format("{1}{0:#,0}");
//
//                //TODO to this route depended and not hardcoded
//
//                row.cell[13] = "<a href=\"" + webbHelpers.RouteUrl("Diamond", new RouteValueDictionary()
//                                                                                 {
//                                                                                     {"DiamondID",diamond.DiamondID},
//                                                                                     {"SettingID",searchParameters.SettingID},
//                                                                                     {"MediaType",searchParameters.MediaType},
//                                                                                     {"Size",searchParameters.Size}
//                                                                                 }) + "\" >View</a>";

                                
                                row.cell[0] = diamond.Shape;
                                row.cell[1] = formatter.ToCaratWeight(diamond.Weight) ;
                                row.cell[2] = diamond.Color;
                                row.cell[3] = diamond.Clarity;
                                row.cell[4] = diamond.Cut;
                                row.cell[5] = diamond.Report;
                                row.cell[6] = new Money(diamond.Price, Currency.Usd).Format("{1}{0:#,0}");

                                 row.cell[7] = "<a href=\"" + webbHelpers.RouteUrl("Diamond", new RouteValueDictionary()
                                                                                                                 {
                                                                                                                     {"DiamondID",diamond.DiamondID},
                                                                                                                     {"SettingID",searchParameters.SettingID},
                                                                                                                     {"MediaType",searchParameters.MediaType},
                                                                                                                     {"Size",searchParameters.Size}
                                                                                                                 }) + "\" >View</a>";


                gridrows.Add(row);
            }

            jsonModel.rows = gridrows;
            jsonModel.page = searchParameters.page;
            jsonModel.records = diamondRepository.TotalRecords;
            jsonModel.total = diamondRepository.LastOporationTotalPages;
            return jsonModel;
        }
    }
}