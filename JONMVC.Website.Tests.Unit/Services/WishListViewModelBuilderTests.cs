using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Services
{
    [TestFixture]
    public class WishListViewModelBuilderTests:CheckoutTestsBase
    {
       
        [Test]
        public void Build_ShouldHaveOneItemInTheListWhenWishListPersistenceIsGivenWithOneItem()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence,jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items.Should().HaveCount(1);

        }

        [Test]
        public void Build_ShouldMapPriceCorrectly()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence, jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items[0].Price.Should().Be("$10,000");

        }

        [Test]
        public void Build_ShouldMapDescriptionCorrectly()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence, jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items[0].Description.Should().Be("title");

        }


        [Test]
        public void Build_ShouldMapItemNumberCorrectly()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence, jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items[0].ItemNumber.Should().Be(Tests.FAKE_JEWEL_ITEMNUMBER);

        }

        [Test]
        public void Build_ShouldMapIconCorrectly()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence, jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items[0].Icon.Should().Be("/jon-images/jewel/0101-15421-icon-wg.jpg");

        }

        [Test]
        public void Build_ShouldMapIDCorrectly()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence, jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items[0].ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID.ToString());

        }

        [Test]
        public void Build_ShoulSkiJewelIdsThatAreNotValid()
        {
            //Arrange
            var jewelRepository = new FakeJewelRepository(new FakeSettingManager());

            var wishListPersistence = MockRepository.GenerateStub<IWishListPersistence>();
            wishListPersistence.Stub(x => x.GetItemsOnWishList()).Return(new List<int>()
                                                                        {
                                                                            Tests.BAD_FAKE_JEWELRY_ID,
                                                                            Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID
                                                                        });

            var builder = new WishListViewModelBuilder(wishListPersistence, jewelRepository, mapper);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Items.Should().HaveCount(1);

        }


    }
}