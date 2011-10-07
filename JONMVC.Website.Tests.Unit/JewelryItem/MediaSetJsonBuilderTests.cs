using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    [TestFixture]
    public class MediaSetJsonBuilderTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void MediaSetList_ShouldReturn2SetsBecauseTheFileExist()
        {
            //Arrange
            const string itemNumber = "0101-15001";
            const JewelMediaType jewelMediaSets = JewelMediaType.All;

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber();
            var settingManager = new FakeSettingManager();
            
            var mediaSetBuilder = new MediaSetBuilder(settingManager, fakeFileSystem);
            //Act
            var mediaSetList = mediaSetBuilder.Build(itemNumber,jewelMediaSets);
            //Assert
            mediaSetList.Should().HaveCount(2);

        }

        [Test]
        public void MediaSetList_ShouldReturn1SetsBecauseIconDoesntExistOnOneOfTheSets()
        {
            //Arrange
            const string itemNumber = "0101-15001";
            const JewelMediaType jewelMediaSets = JewelMediaType.All;

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-yg.flv",itemNumber),new MockFileData("")}
                                                        
                                                    });
            var settingManager = new FakeSettingManager();

            var mediaSetBuilder = new MediaSetBuilder( settingManager, fakeFileSystem);
            //Act
            var mediaSetList = mediaSetBuilder.Build(itemNumber, jewelMediaSets);
            //Assert
            mediaSetList.Should().HaveCount(1);

        }

        [Test]
        public void MediaSetList_ShouldReturnEmptyListIfNoIconFilesArePresentAndNoPicFilesArePresent()
        {
            //Arrange
            const string itemNumber = "0101-15001";
            const JewelMediaType jewelMediaSets = JewelMediaType.All;

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-yg.flv",itemNumber),new MockFileData("")}
                                                        
                                                    });
            var settingManager = new FakeSettingManager();

            var mediaSetBuilder = new MediaSetBuilder(settingManager, fakeFileSystem);
            //Act
            var mediaSetList = mediaSetBuilder.Build(itemNumber, jewelMediaSets);
            //Assert
            mediaSetList.Should().HaveCount(0);

        }

    }
}