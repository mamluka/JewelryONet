using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using System.IO.Abstractions.TestingHelpers;


namespace JONMVC.Website.Tests.Unit.Jewelry
{
    [TestFixture]
    public class MediaVerifierTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Verify_ShouldReturnTheSamePathIfAllFilesArePresent()
        {
            //Arrange


            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber();

           var media = new Media()
                            {
                                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"
                                
                            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.IconURLForWebDisplay.Should().NotBeNull();
            verifiedmedia.PictureURLForWebDisplay.Should().NotBeNull();
            verifiedmedia.HiResURLForWebDisplay.Should().NotBeNull();
            verifiedmedia.HandURLForWebDisplay.Should().NotBeNull();
            verifiedmedia.MovieURLForWebDisplay.Should().NotBeNull();


        }
        [Test]
        public void Verify_ShouldReturnWholeMediaNullIfIConNotPresentOnDisk()
        {
            //Arrange

            const string itemNumber = "0101-15001";

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-report-all.jpg",itemNumber),new MockFileData("")}
                                                        
                                                    });

            var media = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"

            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.Should().BeNull();


        }

        [Test]
        public void Verify_ShouldReturnWholeMediaNullIfPictureNotPresentOnDisk()
        {
            //Arrange

            const string itemNumber = "0101-15001";

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-report-all.jpg",itemNumber),new MockFileData("")}
                                                        
                                                    });

            var media = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"

            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.Should().BeNull();


        }

        [Test]
        public void Verify_ShouldReturnHiResAsNullIfHiResIsNotPresentOnDisk()
        {
            //Arrange

            const string itemNumber = "0101-15001";

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-report-all.jpg",itemNumber),new MockFileData("")}
                                                        
                                                    });

            var media = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"

            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.HiResURLForWebDisplay.Should().BeNull();


        }

        [Test]
        public void Verify_ShouldReturnHandAsNullIfHandIsNotPresentOnDisk()
        {
            //Arrange

            const string itemNumber = "0101-15001";

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-report-all.jpg",itemNumber),new MockFileData("")}
                                                        
                                                    });

            var media = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"

            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.HandURLForWebDisplay.Should().BeNull();


        }

        [Test]
        public void Verify_ShouldReturnHandAsNullIfReportIsNotPresentOnDisk()
        {
            //Arrange

            const string itemNumber = "0101-15001";

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        //{String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-report-all.jpg",itemNumber),new MockFileData("")}
                                                        
                                                    });

            var media = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"

            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.ReportURLForWebDisplay.Should().BeNull();


        }

        [Test]
        public void Verify_ShouldReturnMovieAsNullIfMovieIsNotPresentOnDisk()
        {
            //Arrange

            const string itemNumber = "0101-15001";

            var fakeFileSystem = FakeFileSystem.MediaFileSystemForItemNumber(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
//                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")}
                                                        
                                                    });

            var media = new Media()
            {
                IconDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-icon-wg.jpg",
                PictureDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-pic-wg.jpg",
                HiResDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hires-wg.jpg",
                HandDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-hand-wg.jpg",
                MovieDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-mov-wg.flv",
                ReportDiskPathForWebDisplay = @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15001-report-all.jpg",

                IconURLForWebDisplay = @"/jon-images/jewel/0101-15001-icon-wg.jpg",
                PictureURLForWebDisplay = @"/jon-images/jewel/0101-15001-pic-wg.jpg",
                HiResURLForWebDisplay = @"/jon-images/jewel/0101-15001-hires-wg.jpg",
                HandURLForWebDisplay = @"/jon-images/jewel/0101-15001-hand-wg.jpg",
                MovieURLForWebDisplay = @"/jon-images/jewel/0101-15001-mov-wg.flv",
                ReportURLForWebDisplay = @"/jon-images/jewel/0101-15001-report-all.jpg"

            };

            var mediaVerifier = new MediaVerifier(fakeFileSystem);
            //Act
            var verifiedmedia = mediaVerifier.Verify(media);
            //Assert
            verifiedmedia.MovieURLForWebDisplay.Should().BeNull();


        }



    }
}