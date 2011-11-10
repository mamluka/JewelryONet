using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using FluentAssertions;
namespace JONMVC.Website.Tests.Unit.Jewelry
{
    [TestFixture]
    public class JewelRepositoryTests
    {
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {

        }

        [Test]
        public void GetItemByID_ShouldReturnTheRightID()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            jewel.ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);
        }

        [Test]
        public void GetItemByID_ShouldSetTheRightSpecialPrice()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            jewel.SpecialPrice.Should().Be(8000);

        }

        [Test]
        public void GetItemByID_ShouldSetTheJewelPriceLikeARegularPriceBecauseItemIsNotSpecialAndUserIsNotADealer()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            jewel.Price.Should().Be(jewel.RegularPrice);

        }

        [Test]
        public void GetItemByID_ShouldSetTheJewelPriceLikeTheSpecialPriceBecauseItemIsOnSpecial()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);

            //Assert
            jewel.Price.Should().Be(jewel.SpecialPrice);

        }

        [Test]
        public void GetItemByID_ShouldSetTheRightDealerPrice()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            jewel.DealerPrice.Should().Be(7000);

        }

        [Test]
        public void GetItemByID_ShouldReturnFalseForInSpecial()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID);

            //Assert
            jewel.IsSpecial.Should().BeFalse();

        }

        [Test]
        public void GetItemByID_ShouldReturnTrueForInSpecialForTheItemThatContainsAllNonDefaults()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            //Act
            var jewel = jewelRepository.GetJewelByID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);

            //Assert
            jewel.IsSpecial.Should().BeTrue();

        }

       

      

    }
}