using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Diamonds
{
    [TestFixture]
    public class SearchParametersAutoMapperTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        [ExpectedException(typeof(AutoMapperMappingException))]
        public void Mapper_ShouldReturnAExceptionIfANonSupportedDatabaseKeyIsGivenByTheJSON()
        {
            //Arrange

            Mapper.CreateMap<DiamondSearchParametersGivenByJson, DiamondSearchParameters>()
               .ForMember(s => s.Shape,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("shape")).FromMember(s=>s.Shape))
               .ForMember(s => s.Color,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("color")).FromMember(s => s.Color))
               .ForMember(s => s.Clarity,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("clarity")).FromMember(s => s.Clarity))
               .ForMember(s => s.Cut,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("grade")).FromMember(s => s.Cut))
               .ForMember(s => s.Report,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("report")).FromMember(s => s.Report))
               .ForMember(s => s.ItemsPerPage, opt => opt.MapFrom(x => x.rows))
               .ForMember(s => s.SortDirection, opt => opt.MapFrom(x => x.sort))


               ;

            var json = new DiamondSearchParametersGivenByJson()
                           {
                               page = 1,
                               Color = new List<string>() {"H"},
                               Clarity = new List<string>() {"VVS3"},
                               Cut = new List<string>() {"VG"},
                               Report = new List<string>() {"GIA"},
                               Shape = new List<string>() {"Round"},
                               PriceFrom = 1000,
                               PriceTo = 2000,
                               WeightFrom = 1,
                               WeightTo = 3,
                               rows = 10,
                               sidx = "id",
                               sort = "desc"


                           };

            var searchParameters = Mapper.Map<DiamondSearchParametersGivenByJson, DiamondSearchParameters>(json);
            //Act

            //Assert

        }


        [Test]
        public void Mapper_ShouldCreateAVaidMapForTheSearchParameterObject()
        {
            //Arrange

            Mapper.CreateMap<DiamondSearchParametersGivenByJson, DiamondSearchParameters>()
               .ForMember(s => s.Shape,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("shape")).FromMember(s => s.Shape))
               .ForMember(s => s.Color,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("color")).FromMember(s => s.Color))
               .ForMember(s => s.Clarity,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("clarity")).FromMember(s => s.Clarity))
               .ForMember(s => s.Cut,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("grade")).FromMember(s => s.Cut))
               .ForMember(s => s.Report,
                          opt =>
                          opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("report")).FromMember(s => s.Report))
               .ForMember(s => s.ItemsPerPage, opt => opt.MapFrom(x => x.rows))
               .ForMember(s => s.SortDirection, opt => opt.MapFrom(x => x.sort))


               ;
            //Act

            //Assert
            Mapper.AssertConfigurationIsValid();

        }
    }
}