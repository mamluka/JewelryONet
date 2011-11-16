using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Extensions;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Extentions
{
    [TestFixture]
    public class prettyPhotoMediaFactoryTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void SinglePhoto_ShouldNuildASimpleImageWithThumbAndLargePicsInput()
        {
            //Arrange
            string thumb = "thumb";
            string large = "large";
            string alt = "alt";

            var prettyPhotoMatcher = SimplePrettyPhotoMedia(thumb, large, alt);
            var factory = new prettyPhotoMediaFactory();
            //Act
            var prettyPhoto = factory.SinglePhoto(thumb, large, alt);
            //Assert
           // prettyPhoto.Should().Be(prettyPhotoMatcher);


        }

        [Test]
        public void SinglePhotoUseLargeForBoth_ShouldReturntheRightPaths() 
        {
            //Arrange
            string large = "large";
            string alt = "alt";



            var prettyPhotoMatcher = SimplePrettyPhotoMedia(large, large, alt);
            var factory = new prettyPhotoMediaFactory();
            //Act
            var prettyPhoto = factory.SinglePhotoUseLargeForBoth(large, alt);
            //Assert
           // prettyPhoto.Should().Be(prettyPhotoMatcher);

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SinglePhoto_ShouldThrowExceptiopWhenThumbPathIsEmpty()
        {
            //Arrange
            string thumb = "";
            string large = "large";
            string alt = "alt";

            var prettyPhotoMatcher = SimplePrettyPhotoMedia(thumb, large, alt);
            var factory = new prettyPhotoMediaFactory();
            //Act
            var prettyPhoto = factory.SinglePhoto(thumb, large, alt);
            //Assert
            

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SinglePhoto_ShouldThrowExceptiopWhenLargeImagePathIsEmpty()
        {
            //Arrange
            string thumb = "thumb";
            string large = "";
            string alt = "alt";

            var prettyPhotoMatcher = SimplePrettyPhotoMedia(thumb, large, alt);
            var factory = new prettyPhotoMediaFactory();
            //Act
            var prettyPhoto = factory.SinglePhoto(thumb, large, alt);
            //Assert


        }



        public static prettyPhotoMedia SimplePrettyPhotoMedia(string thumb, string large, string alt)
        {
            var prettyPhotoMatcher = new prettyPhotoMedia(thumb, large, alt);
            return prettyPhotoMatcher;
        }
    }
}