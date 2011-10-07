using JONMVC.Website.Models.Jewelry;
using NUnit.Framework;
using Rhino.Mocks;
using JONMVC.Website.Models.Utils;
using FluentAssertions;

namespace JONMVC.Website.Tests.Unit.Jewelry
{
    /// <summary>
    /// The media object is treated as a value object
    /// </summary>
    [TestFixture]
    public class MediaFactoryTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void IconURLForWebDisplay_ShoulReturnTheRightPathToIcon()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            
            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.IconURLForWebDisplay;
            //Assert
            Assert.That(iconPath,Is.EqualTo("/jon-images/jewel/0101-10001-icon-wg.jpg"));    

        }
        
        [Test]
        public void PictureURLForWebDisplay_ShoulReturnTheRightPathToPicture()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.PictureURLForWebDisplay;
            //Assert
            Assert.That(iconPath, Is.EqualTo("/jon-images/jewel/0101-10001-pic-wg.jpg"));

        }

        [Test]
        public void HiResURLForWebDisplay_ShoulReturnTheRightPathToPicture()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.HiResURLForWebDisplay;
            //Assert
            Assert.That(iconPath, Is.EqualTo("/jon-images/jewel/0101-10001-hires-wg.jpg"));

        }

        [Test]
        public void HandURLForWebDisplay_ShoulReturnTheRightPathToPicture()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.HandURLForWebDisplay;
            //Assert
            Assert.That(iconPath, Is.EqualTo("/jon-images/jewel/0101-10001-hand-wg.jpg"));

        }

        [Test]
        public void MovieURLForWebDisplay_ShoulReturnTheRightPathToPicture()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.MovieURLForWebDisplay;
            //Assert
            Assert.That(iconPath, Is.EqualTo("/jon-images/jewel/0101-10001-mov-wg.flv"));

        }

        [Test]
        public void PictureDiskPathForWebDisplay_ShouldReturnTheRightPath()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.PictureDiskPathForWebDisplay;
            //Assert
            Assert.That(iconPath, Is.EqualTo(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-10001-pic-wg.jpg"));

        }
        [Test]
        public void IconDsikPathForWebDisplay_ShouldReturnTheRightPath()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseDiskPath()).Return(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.IconDiskPathForWebDisplay;
            //Assert
            Assert.That(iconPath, Is.EqualTo(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-10001-icon-wg.jpg"));

        }
        [Test]
        public void IconURLForWebDisplay_ShoulReturnTheRightPrefixForYellowGoldIfYellowGoldPresent()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.YellowGold,JewelMediaType.YellowGold);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.IconURLForWebDisplay;
            //Assert
            iconPath.Should().Contain("yg");

        }
        [Test]
        public void IconURLForWebDisplay_ShoulReturnTheRightPrefixForYellowGoldIfAllPresent()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.YellowGold, JewelMediaType.All);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.IconURLForWebDisplay;
            //Assert
            iconPath.Should().Contain("yg");

        }
        [Test]
        public void IconURLForWebDisplay_ShoulReturnTheRightPrefixForWhiteGoldIfWhiteGoldPresent()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.WhiteGold, JewelMediaType.WhiteGold);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.IconURLForWebDisplay;
            //Assert
            iconPath.Should().Contain("wg");

        }
        [Test]
        public void IconURLForWebDisplay_ShoulReturnTheRightPrefixForWhiteGoldIfAllPresent()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.WhiteGold, JewelMediaType.All);
            //Act
            Media media = mediaFactory.BuildMedia();

            string iconPath = media.IconURLForWebDisplay;
            //Assert
            iconPath.Should().Contain("wg");

        }

        [Test]
        public void IconURLForWebDisplay_ShoulReturnWhiteGoldMediaSetWhenAskedForWhiteGoldAndGivenAll()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.WhiteGold, JewelMediaType.All);
            //Act
            Media media = mediaFactory.BuildMedia();

            
            //Assert
            media.MediaSet.Should().Be(JewelMediaType.WhiteGold);

        }

        [Test]
        public void IconURLForWebDisplay_ShoulReturnYellowGoldMediaSetWhenAskedForYellowGoldAndGivenAll()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.YellowGold, JewelMediaType.All);
            //Act
            Media media = mediaFactory.BuildMedia();


            //Assert
            media.MediaSet.Should().Be(JewelMediaType.YellowGold);

        }


        [Test]
        public void IconURLForWebDisplay_ShoulReturnWhiteGoldMediaSetWhenAskedForAlldAndGivenAll()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.All, JewelMediaType.All);
            //Act
            Media media = mediaFactory.BuildMedia();


            //Assert
            media.MediaSet.Should().Be(JewelMediaType.WhiteGold);

        }

        [Test]
        public void IconURLForWebDisplay_ShoulReturnWhiteGoldMediaSetWhenAskedForWhiteGoldAndGivenWhiteGold()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.WhiteGold, JewelMediaType.WhiteGold);
            //Act
            Media media = mediaFactory.BuildMedia();


            //Assert
            media.MediaSet.Should().Be(JewelMediaType.WhiteGold);

        }

        [Test]
        public void IconURLForWebDisplay_ShoulReturnYellowGoldMediaSetWhenAskedForYellowGoldAndGivenYellowGold()
        {
            //Assign
            ISettingManager manager = MockRepository.GenerateStub<ISettingManager>();

            const string itemNumber = "0101-10001";

            manager.Stub(x => x.GetJewelryBaseWebPath()).Return("/jon-images/jewel/");

            var mediaFactory = new MediaFactory(itemNumber, manager);
            mediaFactory.ChangeMediaSet(JewelMediaType.YellowGold, JewelMediaType.YellowGold);
            //Act
            Media media = mediaFactory.BuildMedia();


            //Assert
            media.MediaSet.Should().Be(JewelMediaType.YellowGold);

        }

        //TODO Add more error checking

    }
}