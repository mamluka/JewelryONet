using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Utils
{
    [TestFixture]
    public class FormatterTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void FormatTwoDecimalPoints_ShouldReturnTheRightFormat()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 5.26;
            //Act
            var result = formatter.FormatTwoDecimalPoints(weight,"");
            //Assert
            result.Should().Be("5.26");
        }

        [Test]
        public void FormatTwoDecimalPoints_ShouldReturnTheRightFormatRoundnumber()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 5.1;
            //Act
            var result = formatter.FormatTwoDecimalPoints(weight,"");
            //Assert
            result.Should().Be("5.10");
        }

        [Test]
        public void FormatTwoDecimalPoints_ShouldReturnTheRightFormatTenthPart()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 5.9;
            //Act
            var result = formatter.FormatTwoDecimalPoints(weight,"");
            //Assert
            result.Should().Be("5.90");
        }

        [Test]
        public void FormatTwoDecimalPoints_ShouldReturnTheRightFormatSmallerThanOne()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 0.3;
            //Act
            var result = formatter.FormatTwoDecimalPoints(weight,"");
            //Assert
            result.Should().Be("0.30");
        }

        [Test]
        public void ToCaratWeight_ShouldReturnTheRightFormatSmallerThanOne()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 0.3;
            //Act
            var result = formatter.ToCaratWeight(weight);
            //Assert
            result.Should().Be("0.30 Ct.");
        }

        [Test]
        public void ToGramWeight_ShouldReturnTheRightFormatSmallerThanOne()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 0.3;
            //Act
            var result = formatter.ToGramWeight(weight);
            //Assert
            result.Should().Be("0.30 gr.");
        }

        [Test]
        public void ToMilimeter_ShouldReturnTheRightFormatSmallerThanOne()
        {
            //Arrange
            var formatter = new JONFormatter();
            var weight = 0.3;
            //Act
            var result = formatter.ToMilimeter(weight);
            //Assert
            result.Should().Be("0.30 mm.");
        }
    }
}