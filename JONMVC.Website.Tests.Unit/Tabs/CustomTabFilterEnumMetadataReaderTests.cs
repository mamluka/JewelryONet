using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace JONMVC.Website.Tests.Unit.Tabs
{
    [TestFixture]
    public class CustomTabFilterEnumMetadataReaderTests
    {
        [Test]
        public void Values_ShouldReturnTheValuesWithTheData()
        {
            //Arrange
            var filterReader = new CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues>();

            //Act
           var values =  filterReader.Values();
            //Assert

            values.Should().HaveCount(4);

        }

        [Test]
        public void Values_ShouldReturnTheDynamicSQLCorrespondingToTheEnumIntGiven()
        {
            //Arrange
            var filterReader = new CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues>();

            //Act
            var dynamicSQL = filterReader.ReadDynamicSQLByValue(1);
            //Assert

            dynamicSQL.Pattern.Should().Be("cs_type = @0");
            dynamicSQL.Valuelist.Should().Contain("ruby");

        }

        [Test]
        public void Values_ShouldReturnNullIfNoFieldIsPresentMeaningDontFilter()
        {
            //Arrange
            var filterReader = new CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues>();

            //Act
            var dynamicSQL = filterReader.ReadDynamicSQLByValue(0);
            //Assert

            dynamicSQL.IsDoingNothing.Should().BeTrue();


        }
    }
}