using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Routing;
using AutoMapper;
using AutoMapper.Mappers;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Views;
using NMoneys;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    [TestFixture]
    public class JsonDiamondsBuilderTests:JewelDesignTestsBase
    {
        /// <summary>
        /// Prepares mock repository
        /// </summary>

        [SetUp]
        public void Initialize()
        {
            base.Initialize();
        }

        [Test]
        public void Build_ShouldReturnAllTheItemsThatTheRepositoryReturnedToTheBuilder()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            var searchParameters = new DiamondSearchParametersGivenByJson();

            var formatter = CreateDefaultJONFormatter;
            var webbHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //Assert
            jsonResult.rows.Should().HaveCount(4);
        }

        [Test]
        public void Build_ShouldReturnThePageThatWasRequestedByTheSearchParameters()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            var searchParameters = new DiamondSearchParametersGivenByJson
                                       {
                                           page = 4
                                       };
         

            var formatter = CreateDefaultJONFormatter;

            var webbHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //Assert
            jsonResult.page.Should().Be(4);
        }

        [Test]
        public void Build_ShouldReturnTheRightNumberOfRecordsForTheCurrentPageThatWasRequestedByTheSearch()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            diamondRepository.Stub(x => x.TotalRecords).Return(4);

            var searchParameters = new DiamondSearchParametersGivenByJson();

            var formatter = CreateDefaultJONFormatter;

            var webbHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //Assert
            jsonResult.records.Should().Be(4);
        }

        [Test]
        public void Build_ShouldReturnTheTotalNumberOfRecordsForTheCurrentSearch()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>());

            diamondRepository.Stub(x => x.LastOporationTotalPages).Return(4);

            var searchParameters = new DiamondSearchParametersGivenByJson();

            var formatter = CreateDefaultJONFormatter;

            var webbHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //Assert
            jsonResult.total.Should().Be(4);
        }

      

        [Test]
        public void Build_ShouldReturnTheRightIDInTheRowOfTheGrid()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            var searchParameters = new DiamondSearchParametersGivenByJson();

            var formatter = CreateDefaultJONFormatter;

            var webbHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //Assert
            jsonResult.rows[0].id = 1;
            jsonResult.rows[1].id = 2;
            jsonResult.rows[2].id = 3;
            jsonResult.rows[3].id = 4;

        }

        [Test]
        public void Build_ShouldReturnTheRightRowCellsInTheRowsOfTheGrid()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            var searchParameters = new DiamondSearchParametersGivenByJson();

            var webbHelpers = MockRepository.GenerateStub<IWebHelpers>();

            var formatter = CreateDefaultJONFormatter;

            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //Assert


            //jsonResult.rows[0].cell[0].Should().Be("1");
            jsonResult.rows[0].cell[0].Should().Be("Round");
            jsonResult.rows[0].cell[1].Should().Be("carat weight");
            jsonResult.rows[0].cell[2].Should().Be("H");
            jsonResult.rows[0].cell[3].Should().Be("VVS1");
            jsonResult.rows[0].cell[4].Should().Be("VG");
            //jsonResult.rows[0].cell[6].Should().Be("decimal points");
            //jsonResult.rows[0].cell[7].Should().Be("decimal points");
            //jsonResult.rows[0].cell[8].Should().Be("VG/VG");
            //jsonResult.rows[0].cell[9].Should().Be("VG");
            jsonResult.rows[0].cell[5].Should().Be("GIA");
           // jsonResult.rows[0].cell[11].Should().Be("58.9");
            jsonResult.rows[0].cell[6].Should().Be(Tests.AsMoney(2586));


        }

        [Test]
        public void Build_ShouldReturnTheRightDiamondViewLinkAndCallTheWebHelpersCorrectly()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            var searchParameters = new DiamondSearchParametersGivenByJson();
            var webbHelpers = MockRepository.GenerateMock<IWebHelpers>();

            webbHelpers.Expect(
                x =>
                x.RouteUrl(Arg<string>.Is.Equal("Diamond"),
                           Arg<RouteValueDictionary>.Matches(d => (int)d["DiamondID"] == 1 && (int)d["SettingID"] == 0))).Repeat.Once()
                           .Return("/JewelDesign/Diamond/1");

            var formatter = CreateDefaultJONFormatter;


            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper,webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //AssertJONMVC.Website.Tests.Unit.JewelDesign.JsonDiamondsBuilderTests.Build_ShouldReturnTheRightDiamondViewLinkAndCallTheWebHelpersCorrectly:
            webbHelpers.VerifyAllExpectations();
            jsonResult.rows[0].cell[7].Should().Be("<a href=\"/JewelDesign/Diamond/1\" >View</a>");

           


        }

        [Test]
        public void Build_ShouldReturnTheRightDiamondViewLinkAndCallTheWebHelpersCorrectlyWhenSettingIDIsGivenTogetherWithMediaTypeAndSize()
        {
            //Arrange
            var diamondRepository = MockRepository.GenerateStub<IDiamondRepository>();
            diamondRepository.Stub(
                x => x.DiamondsBySearchParameters(Arg<DiamondSearchParameters>.Is.Anything)).Return(new List<Diamond>()
                                                                                                        {
                                                                                                            {CreateStubDiamondDiversity("H","VVS1",(decimal) 2.25,2586,1)},
                                                                                                            {CreateStubDiamondDiversity("D","SI1",(decimal) 3.4,2086,2)},
                                                                                                            {CreateStubDiamondDiversity("E","VS1",(decimal) 1.25,12586,3)},
                                                                                                            {CreateStubDiamondDiversity("H","VS2",(decimal) 2.45,21586,4)},
                                                                                                     });

            var searchParameters = new DiamondSearchParametersGivenByJson();

            searchParameters.SettingID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;
            searchParameters.MediaType = JewelMediaType.YellowGold;
            searchParameters.Size = Tests.SAMPLE_JEWEL_SIZE_725;

            var webbHelpers = MockRepository.GenerateMock<IWebHelpers>();

            webbHelpers.Expect(
                x =>
                x.RouteUrl(Arg<string>.Is.Equal("Diamond"),
                           Arg<RouteValueDictionary>.Matches(
                               d =>
                               (int) d["DiamondID"] == 1 &&
                               (int) d["SettingID"] == Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID &&
                               (string) d["Size"] == Tests.SAMPLE_JEWEL_SIZE_725 &&
                               (JewelMediaType) d["MediaType"] == searchParameters.MediaType
                               ))).Repeat.Once()

                .Return("/JewelDesign/Diamond/1/1111/7.25/2");

            var formatter = CreateDefaultJONFormatter;


            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter, mapper, webbHelpers);
            //Act
            var jsonResult = builder.Build();
            //AssertJONMVC.Website.Tests.Unit.JewelDesign.JsonDiamondsBuilderTests.Build_ShouldReturnTheRightDiamondViewLinkAndCallTheWebHelpersCorrectly:
            webbHelpers.VerifyAllExpectations();
            jsonResult.rows[0].cell[7].Should().Be("<a href=\"/JewelDesign/Diamond/1/1111/7.25/2\" >View</a>");




        }

        private static IJONFormatter CreateDefaultJONFormatter
        {
            get
            {
                var formatter = MockRepository.GenerateStub<IJONFormatter>();

                formatter.Stub(x => x.ToCaratWeight(Arg<decimal>.Is.Anything)).Return("carat weight");
                formatter.Stub(x => x.FormatTwoDecimalPoints(Arg<decimal>.Is.Anything)).Return("decimal points");
                return formatter;
            }
        }


        private Diamond CreateStubDiamondDiversity(string color,string clarity,decimal weight,decimal price,int id)
        {
            return new Diamond()
                       {
                           Clarity = clarity,
                           Color = color,
                           Culet = "",
                           Depth = (decimal) 58.9,
                           DiamondID = id,
                           Fluorescence = "VG",
                           Girdle = "",
                           Cut = "VG",
                           Height = (decimal) 2.25,
                           Length = (decimal) 5.36,
                           Polish = "VG",
                           Price = price,
                           Report = "GIA",
                           ReportURL = "",
                           Shape = "Round",
                           Symmetry = "VG",
                           Table = (decimal) 65.2,
                           Weight = weight,
                           Width = (decimal) 3.25
                       };
        }


    }


}