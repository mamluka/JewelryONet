using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using AutoMapper;
using AutoMapper.Mappers;
using System.Linq;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using SortDirection = JONMVC.Website.Models.Utils.SortDirection;


namespace JONMVC.Website.Tests.Unit.Diamonds
{
    [TestFixture]
    public class DiamondRepositoryTests
    {
        private IMappingEngine mapper;
        private const int THE_TOTAL_NUMBER_OF_ITEMS = 6;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        /// 
        [TestFixtureSetUp]
        public void InitializeFixture()
        {
            //Bootstrapper.Excluding.Assembly("JONMVC.Core.Configurations").With.AutoMapper().Start();
            MapsContainer.CreateAutomapperMaps();

            mapper = Mapper.Engine;

        }

        [SetUp]
        public void Initialize()
        {

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldReturnAllTheItemsInTheRepostoryWhenNoFiltersAreAskedForAndWeWantTheFirstPage()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(THE_TOTAL_NUMBER_OF_ITEMS);
        }

        [Test]
        public void DiamondsBySearchParameters_ShouldReturnOnly3ItemsAsRequestedBySearchParams()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 3;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);
        }

        [Test]
        public void DiamondsBySearchParameters_ShouldReturnOnly3ItemsOnPage2AsRequestedBySearchParams()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 2;
            searchParameters.ItemsPerPage = 3;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);
        }

        [Test]
        public void DiamondsBySearchParameters_ShouldReturnZeroItemsIfThePageRequestedDoesntExists()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 3;
            searchParameters.ItemsPerPage = 3;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(0);
        }

        [Test]
        public void DiamondsBySearchParameters_ShouldReturn1ItemOnPage2WhenPageSizeIs5()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 2;
            searchParameters.ItemsPerPage = 5;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(1);
        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByColorWhenOneColorSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Color = new List<string>() {"H"};

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(2);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByColorButMoreThenOneColorSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Color = new List<string>() { "H","G" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(4);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByClarityWhenOneClaritySelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Clarity = new List<string>() { "VS1" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(2);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByClarityButMoreThenOneClaritySelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Clarity = new List<string>() { "VS1", "VVS1" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByShapeWhenOneShapeSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Shape = new List<string>() { "Round" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(2);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByShapeButMoreThenOneShapeSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Shape = new List<string>() { "Round", "Princess" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(5);

        }


        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByReportWhenOneReportSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Report = new List<string>() { "GIA" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(5);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByReportButMoreThenOneReportSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Report = new List<string>() { "IGI", "GIA" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(6);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByCutWhenOneCutSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Cut = new List<string>() { "G" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(2);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByCutButMoreThenOneCutSelected()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.Cut = new List<string>() { "G", "VG" };

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(4);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByPriceMarginsWhenOnlyFromPriceIsSpecified()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.PriceFrom = 25000;
            searchParameters.PriceTo = 0;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(5);

        }
        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByPriceMarginsWhenOnlyToPriceIsSpecified()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.PriceFrom = 0;
            searchParameters.PriceTo = 26000;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByPriceMarginsWhenBothAreSpecified()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.PriceFrom = 26000;
            searchParameters.PriceTo = 28000;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(1);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByPriceMarginsWhenBothAreSpecifiedEvenIfTheFromFieldIBiggerThenTheToField()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.PriceFrom = 28000;
            searchParameters.PriceTo = 26000;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(1);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByWeightMarginsWhenOnlyFromWeightIsSpecified()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.WeightFrom = (decimal) 1.1;
            searchParameters.WeightTo = 0;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(6);

        }
        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByWeightMarginsWhenOnlyToWeightIsSpecified()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.WeightFrom = 0;
            searchParameters.WeightTo = (decimal) 1.35;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByWeightMarginsWhenBothAreSpecified()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.WeightFrom = 1;
            searchParameters.WeightTo = (decimal) 1.35;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);

        }

        [Test]
        public void DiamondsBySearchParameters_ShouldFilterResultsByWeightMarginsWhenBothAreSpecifiedEvenIfTheFromFieldIBiggerThenTheToField()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 10;
            searchParameters.WeightFrom = (decimal) 1.35;
            searchParameters.WeightTo = 1;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            //Assert
            diamonds.Should().HaveCount(3);

        }


        [Test]
        public void TotalPagesNumber_ShouldReturnTheTotalNumberOfPagesCorrectly()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;
            searchParameters.ItemsPerPage = 2;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            diamondRepository.DiamondsBySearchParameters(searchParameters);
            var totalPages = diamondRepository.LastOporationTotalPages;
            //Assert
            totalPages.Should().Be(3);

        }

        [Test]
        public void TotalPagesNumber_ShouldSortByWeightInAscOrderWhenCalledAsPartOFTheSearchParams()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.OrderBy = new DynamicOrderBy("totalprice","asc");

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);
            
            //Assert
            var unsortedCollectio = GetDiamondCollectionUnSorted();
            diamonds.Should().ContainInOrder(unsortedCollectio.OrderBy(x => x.Price).ToList());

        }

        [Test]
        public void TotalPagesNumber_ShouldSortByWeightInDescOrderWhenCalledAsPartOFTheSearchParams()
        {
            //Arrange
            var searchParameters = new DiamondSearchParameters();
            searchParameters.OrderBy = new DynamicOrderBy("totalprice", "desc");

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            var diamonds = diamondRepository.DiamondsBySearchParameters(searchParameters);

            //Assert
            var unsortedCollectio = GetDiamondCollectionUnSorted();
            diamonds.Should().ContainInOrder(unsortedCollectio.OrderByDescending(x => x.Price).ToList());

        }



        private List<Diamond> GetDiamondCollectionUnSorted()
        {
            var searchParameters = new DiamondSearchParameters();
            searchParameters.Page = 1;

            var diamondRepository = new FakeDiamondRepository(mapper);
            //Act
            return diamondRepository.DiamondsBySearchParameters(searchParameters);
        }
    }
}