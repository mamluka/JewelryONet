using System;
using System.Linq;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Extentions;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using NMoneys;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    [TestFixture]
    public class JewelryItemViewModelBuilderTests:JewelryItemTestsBase
    {

        [Test]
        public void Build_ShouldReturnTheRightTitle()
        {
            //Arrange

            
            var builder = JewelryItemViewModelBuilderFactoryMethod();


            //Act

            var viewModel = builder.Build();

            //Assert

            viewModel.Title.Should().Be("title");

        }

        [Test]
        public void Build_ShouldReturnTheRightID()
        {
            //Arrange


            var builder = JewelryItemViewModelBuilderFactoryMethod();


            //Act

            var viewModel = builder.Build();

            //Assert

            viewModel.ID.Should().Be(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID.ToString());

        }

        
        [Test]
        public void Build_ShouldReturnThePicturePrettyPhoto()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();

            var prettyPhotoMatcher =
                prettyPhotoMediaFactoryTests.SimplePrettyPhotoMedia("/jon-images/jewel/0101-15421-pic-wg.jpg",
                                                                     "/jon-images/jewel/0101-15421-hires-wg.jpg", "title");
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.MainJewelPicture.Should().Be(prettyPhotoMatcher);
        }

        [Test]
        public void Build_ShouldReturnTheHiresPrettyPhoto()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();

            var prettyPhotoMatcher =
                prettyPhotoMediaFactoryTests.SimplePrettyPhotoMedia("/jon-images/jewel/0101-15421-hires-wg.jpg",
                                                                     "/jon-images/jewel/0101-15421-hires-wg.jpg", "title");
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.HiResJewelPicture.Should().Be(prettyPhotoMatcher);
        }

        [Test]
        public void Build_ShouldReturnAListOfExtraImagesWithTheCorrectCount()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.ExtraImages.Should().HaveCount(3);

        }

        [Test]
        public void Build_ShouldReturnOnlyOneImageBecauseHandDoesntExists()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod(new Dictionary<string, MockFileData>()
                                                    {
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-pic-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-icon-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hires-wg.jpg",new MockFileData("")},
//                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hand-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-mov-wg.flv",new MockFileData("")}
                                                    });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.ExtraImages.Should().HaveCount(1);

        }

        [Test]
        public void Build_ShouldReturnHandAndHiRes2Images()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod(new Dictionary<string, MockFileData>()
                                                    {
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-pic-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-icon-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hires-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hand-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hires2-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-mov-wg.flv",new MockFileData("")}
                                                    });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.ExtraImages.Should().HaveCount(3);

        }

        [Test]
        public void Build_ShouldSetHasMoviePathToBeCorrect()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Movie.Should().Be("/jon-images/jewel/0101-15421-mov-wg.flv");

        }

        [Test]
        public void Build_ShouldSetHasMovieToTrueIfMovieExists()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.HasMovie.Should().BeTrue();

        }

        [Test]
        public void Build_ShouldSetHasMovieToFalseIfMovieDoesntExists()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod(new Dictionary<string, MockFileData>()
                                                    {
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-pic-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-icon-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hires-wg.jpg",new MockFileData("")},
                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-hand-wg.jpg",new MockFileData("")},
//                                                        {@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\0101-15421-mov-wg.flv",new MockFileData("")}
                                                    });
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.HasMovie.Should().BeFalse();

        }

        [Test]
        public void Price_ShouldReturntheRightPrice()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();

            var price = Tests.AsMoney((decimal) 9999.99);

            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Price.Should().Be(price);

        }

        [Test]
        public void Price_ShouldReturnIsSpecialAsFalseBecauseThisIsStandatdItem()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();

            var price = Tests.AsMoney((decimal)9999.99);

            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.isSpecial.Should().BeFalse();

        }

        [Test]
        public void Price_ShouldReturnTheOriginalPriceEqualToTheCurrentPriceBecauseItemIsStandard()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();

            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.RegularPrice.Should().Be(viewModel.Price);

        }

        [Test]
        public void Price_ShouldReturnThePriceEqualToSpecialPriceBecauseItemIsOnSpecial()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);

            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Price.Should().Be(Tests.AsMoney(8000));

        }


        [Test]
        public void Price_ShouldReturnTheYouSaveRatioForTheOnSpecialItem()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);
            var precent = Tests.AsDecimalPrecentRounded((decimal) Math.Round(100 - (8000 / 9999.99) * 100));
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.YouSave.Should().Be(precent);

        }

        [Test]
        public void Price_ShouldReturnIsSpecialAsTrueBecauseItemIsOnSpecial()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);

            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.isSpecial.Should().BeTrue();

        }


        [Test]
        public void Build_ShouldReturnTheRightItemNumber()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.ItemNumber.Should().Be("0101-15421");

        }

        [Test]
        public void Build_ShouldReturnTheRightMetal()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Metal.Should().Be("White Gold 18 Karat");

        }

        [Test]
        public void Build_ShouldReturnTheRightWeightFormatted()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Weight.Should().Be("10.50 gr.");

        }

        [Test]
        public void Build_ShouldReturnTheRighWidthFormatted()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Width.Should().Be("5.00 mm.");

        }

        [Test]
        public void Build_ShouldReturnTheRightAmountOfSpecsForCS()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            var specs = viewModel.SpecsPool.Where(x=> x.JewelComponentID==1);

            specs.Should().HaveCount(5);

        }

        [Test]
        public void Build_ShouldReturnTheRightAmountOfSpecsForSS()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            var specs = viewModel.SpecsPool.Where(x => x.JewelComponentID == 2);

            specs.Should().HaveCount(5);

        }

        [Test]
        public void Build_ShouldReturnTheRightAmountOfSpecs()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            var specs = viewModel.SpecsPool;

            specs.Should().HaveCount(10);

        }

       

        [Test]
        public void Build_ShouldReturnAColorSpecForSideStonesWithRangeOfColorWithOneStepApartUsingTheCurrentColorAsTheMinimum()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            var color = viewModel.SpecsPool.Where(x => x.Title.IndexOf("Color") > -1 && x.JewelComponentID == 2).SingleOrDefault();
            color.Property.Should().Be("G-H");
        }

        [Test]
        public void Build_ShouldReturnAClaritySpecForSideWithStonesRangeOfClarityWithTwoClarityJumps()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            var color = viewModel.SpecsPool.Where(x => x.Title.IndexOf("Clarity") > -1 && x.JewelComponentID == 2).SingleOrDefault();
            color.Property.Should().Be("IF-VVS1");
        }

        [Test]
        public void Build_ShouldReturnMinimumWhenCenterStoneCountIsOne()
        {
            //Arrange
            var fakeCS = fixture.Build<JewelryExtra.JewelComponentProperty>().With(x => x.Count, 1).CreateAnonymous();
            var fakeJewelExtra = fixture.Build<JewelryExtra>().With(x => x.CS, fakeCS).CreateAnonymous();
            var jewel = fixture.Build<Jewel>().With(x => x.JewelryExtra,fakeJewelExtra).CreateAnonymous();

            var builder = CreateJewelItemViewModelBuilderWithJewel(jewel);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.SpecsPool.Where(x => x.Title.Contains("Color") && x.JewelComponentID == 1).SingleOrDefault().Title.Should().Be(
                "Minimum Color");

            viewModel.SpecsPool.Where(x => x.Title.Contains("Clarity") && x.JewelComponentID == 1).SingleOrDefault().Title.Should().Be(
                "Minimum Clarity");
        }


        [Test]
        public void Build_ShouldReturnAvargeWhenNumberOfCenterStonesIsMoreThenOne()
        {
            //Arrange
            var fakeCS = fixture.Build<JewelryExtra.JewelComponentProperty>().With(x => x.Count, 2).CreateAnonymous();
            var fakeJewelExtra = fixture.Build<JewelryExtra>().With(x => x.CS, fakeCS).CreateAnonymous();
            var jewel = fixture.Build<Jewel>().With(x => x.JewelryExtra, fakeJewelExtra).CreateAnonymous();

            var builder = CreateJewelItemViewModelBuilderWithJewel(jewel);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.SpecsPool.Where(x => x.Title.Contains("Color") && x.JewelComponentID ==1).SingleOrDefault().Title.Should().Be(
                "Average Color");

            viewModel.SpecsPool.Where(x => x.Title.Contains("Clarity") && x.JewelComponentID == 1).SingleOrDefault().Title.Should().Be(
                "Average Clarity");
        }

        [Test]
        public void Build_ShouldReturnAColorSpecForCenterStoneWithOutRangeWhenOnlyOneStoneGiven()
        {
            //Arrange
            var fakeCS = fixture.Build<JewelryExtra.JewelComponentProperty>().With(x => x.Count, 1).With(x => x.Color, "H").CreateAnonymous();
            var fakeJewelExtra = fixture.Build<JewelryExtra>().With(x => x.CS, fakeCS).CreateAnonymous();
            var jewel = fixture.Build<Jewel>().With(x => x.JewelryExtra, fakeJewelExtra).CreateAnonymous();

            var builder = CreateJewelItemViewModelBuilderWithJewel(jewel);
            //Act
            var viewModel = builder.Build();
            //Assert
            var color = viewModel.SpecsPool.Where(x => x.Title.IndexOf("Color") > -1 && x.JewelComponentID == 1).SingleOrDefault();
            color.Property.Should().Be("H");
        }

        [Test]
        public void Build_ShouldReturnAClaritySpecForCenterStoneWithNoRangeWhenOneStoneInGiven()
        {
            //Arrange
            var fakeCS = fixture.Build<JewelryExtra.JewelComponentProperty>().With(x => x.Count, 1).With(x => x.Clarity, "VVS1").CreateAnonymous();
            var fakeJewelExtra = fixture.Build<JewelryExtra>().With(x => x.CS, fakeCS).CreateAnonymous();
            var jewel = fixture.Build<Jewel>().With(x => x.JewelryExtra, fakeJewelExtra).CreateAnonymous();

            var builder = CreateJewelItemViewModelBuilderWithJewel(jewel);
            //Act
            var viewModel = builder.Build();
            //Assert
            var color = viewModel.SpecsPool.Where(x => x.Title.IndexOf("Clarity") > -1 && x.JewelComponentID == 1).SingleOrDefault();
            color.Property.Should().Be("VVS1");
        }

        [Test]
        public void Build_ShouldReturnAColorSpecForCenterStoneWithRangeOfColorWithOneStepApartUsingTheCurrentColorAsTheMinimum()
        {
            //Arrange
            var fakeCS = fixture.Build<JewelryExtra.JewelComponentProperty>().With(x => x.Count, 2).With(x=> x.Color,"H").CreateAnonymous();
            var fakeJewelExtra = fixture.Build<JewelryExtra>().With(x => x.CS, fakeCS).CreateAnonymous();
            var jewel = fixture.Build<Jewel>().With(x => x.JewelryExtra, fakeJewelExtra).CreateAnonymous();

            var builder = CreateJewelItemViewModelBuilderWithJewel(jewel);
            //Act
            var viewModel = builder.Build();
            //Assert
            var color = viewModel.SpecsPool.Where(x => x.Title.IndexOf("Color") > -1 && x.JewelComponentID == 1).SingleOrDefault();
            color.Property.Should().Be("G-H");
        }

        [Test]
        public void Build_ShouldReturnAClaritySpecForCenterStoneWithStonesRangeOfClarityWithTwoClarityJumps()
        {
            //Arrange
            var fakeCS = fixture.Build<JewelryExtra.JewelComponentProperty>().With(x => x.Count, 2).With(x => x.Clarity, "VVS1").CreateAnonymous();
            var fakeJewelExtra = fixture.Build<JewelryExtra>().With(x => x.CS, fakeCS).CreateAnonymous();
            var jewel = fixture.Build<Jewel>().With(x => x.JewelryExtra, fakeJewelExtra).CreateAnonymous();

            var builder = CreateJewelItemViewModelBuilderWithJewel(jewel);
            //Act
            var viewModel = builder.Build();
            //Assert
            var color = viewModel.SpecsPool.Where(x => x.Title.IndexOf("Clarity") > -1 && x.JewelComponentID == 1).SingleOrDefault();
            color.Property.Should().Be("IF-VVS1");
        }

        [Test]
        public void Build_ShouldReturnTrueIfHasSideStones()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethod();
            //Act
            var viewModel = builder.Build();
            //Assert
            var size = viewModel.HasSideStones;

            size.Should().BeTrue();

        }

        [Test]
        public void Build_ShouldReturnBestOfferAsTrueIfPresent()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);
            //Act
            var viewModel = builder.Build();
            //Assert
            var size = viewModel.IsBestOffer;

            size.Should().BeTrue();

        }

        [Test]
        public void Build_ShouldReturnThreeTestimonails()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);
            //Act
            var viewModel = builder.Build();
            //Assert
            viewModel.Testimonials.Should().HaveCount(3);
        }

        [Test]
        public void Build_ShouldReturnPageTitleCostructedFromTheJewelProprties()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);
            //Act
            var viewModel = builder.Build();
            //Assert
            string pageTitle = viewModel.Title + " - " + viewModel.Price;
            viewModel.PageTitle.Should().Be(pageTitle);
        }

        [Test]
        public void Build_ShouldReturnTheJewelType()
        {
            //Arrange
            var builder = JewelryItemViewModelBuilderFactoryMethodWithJewelID(Tests.FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER);
            //Act
            var viewModel = builder.Build();
            //Assert

            viewModel.JewelType.Should().Be(JewelType.Ring);
        }



        //TODO add two tests for width and weight NA testing


        






        #region Helpers

         private JewelryItemViewModelBuilder JewelryItemViewModelBuilderFactoryMethod()
        {
            var settingManager = new FakeSettingManager();
            var fakeRepository = new FakeJewelRepository(settingManager);
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);
            var fileSystem =  FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");

            var builder = new JewelryItemViewModelBuilder(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, fakeRepository,fakeTestimonailRepository,fileSystem,mapper);
            return builder;
        }

        private JewelryItemViewModelBuilder CreateJewelItemViewModelBuilderWithJewel(Jewel jewel)
         {
      
            var fakeRepository = MockRepository.GenerateStub<IJewelRepository>();
            fakeRepository.Stub(x => x.GetJewelByID(Arg<int>.Is.Anything)).Return(jewel);

             var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);
             var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber(jewel.ItemNumber);

             var builder = new JewelryItemViewModelBuilder(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, fakeRepository, fakeTestimonailRepository, fileSystem, mapper);
             return builder;
         }


         private JewelryItemViewModelBuilder JewelryItemViewModelBuilderFactoryMethodWithJewelID(int jewelD)
         {
             var settingManager = new FakeSettingManager();
             var fakeRepository = new FakeJewelRepository(settingManager);

             var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber("0101-15421");
             var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);

             var builder = new JewelryItemViewModelBuilder(jewelD, fakeRepository, fakeTestimonailRepository,fileSystem,mapper);
             return builder;
         }

        private JewelryItemViewModelBuilder JewelryItemViewModelBuilderFactoryMethod( Dictionary<string, MockFileData> files )
        {
            var settingManager = new FakeSettingManager();
            var fakeRepository = new FakeJewelRepository(settingManager);

            var fileSystem = FakeFileSystem.MediaFileSystemForItemNumber(files);
            var fakeTestimonailRepository = new FakeTestimonialRepository(mapper);

            var builder = new JewelryItemViewModelBuilder(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, fakeRepository,fakeTestimonailRepository, fileSystem,mapper);
            return builder;
        }

        #endregion
       


    }


}