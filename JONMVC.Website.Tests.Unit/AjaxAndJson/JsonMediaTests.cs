using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Json.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.AjaxAndJson
{
    [TestFixture]
    public class JsonMediaTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void MetalSetFullName_ShouldReturnMediaSetFullNameWhiteGold()
        {
            //Arrange
            var media1 = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                MediaSet = JewelMediaType.WhiteGold
                
            };
            //Act
            var jsonMedia = new JsonMedia(media1);
            //Assert
            jsonMedia.MediaSetFullName.Should().Be("White Gold 18 Karat");
        }

        [Test]
        public void MetalSetFullName_ShouldReturnMediaSetFullNameYellowGold()
        {
            //Arrange
            var media1 = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                MediaSet = JewelMediaType.YellowGold

            };
            //Act
            var jsonMedia = new JsonMedia(media1);
            //Assert
            jsonMedia.MediaSetFullName.Should().Be("Yellow Gold 18 Karat");
        }

        
        

    }
}