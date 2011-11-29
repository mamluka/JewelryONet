using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Web.Mvc;
using AutoMapper;
using JONMVC.Website.Extensions;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;
using NMoneys;
using System.Linq;

namespace JONMVC.Website.ViewModels.Builders
{
    public class JewelryItemViewModelBuilder
    {
        private readonly int jewelryID;
        private readonly IJewelRepository jewelRepository;
        private readonly ITestimonialRepository testimonialRepository;
        private readonly IFileSystem fileSystem;
        private readonly IMappingEngine mapper;

        public JewelryItemViewModelBuilder(int jewelryID, IJewelRepository jewelRepository,ITestimonialRepository testimonialRepository,IFileSystem fileSystem,IMappingEngine mapper)
        {
            this.jewelryID = jewelryID;
            this.jewelRepository = jewelRepository;
            this.testimonialRepository = testimonialRepository;
            this.fileSystem = fileSystem;
            this.mapper = mapper;
        }



        public TOut Build<TOut>() where TOut : PageViewModelBase,IJewelryItemViewModel, new()
        {
            return BuildProcess<TOut>();
        }

        public JewelryItemViewModel Build()
        {
            return BuildProcess<JewelryItemViewModel>();
        }

        private TOut BuildProcess<TOut>() where TOut : PageViewModelBase,IJewelryItemViewModel, new()
        {
            var viewModel = new TOut();

            var jewel = jewelRepository.GetJewelByID(jewelryID);

            if (jewel == null)
            {
                throw new ArgumentNullException();
            }

            viewModel.Title = jewel.Title;

            viewModel.ID = jewel.ID.ToString();

            var prettyMediaFactory = new prettyPhotoMediaFactory();
            var formatter = new JONFormatter();


            viewModel.MainJewelPicture = prettyMediaFactory.SinglePhoto(jewel.Media.PictureURLForWebDisplay,
                                                                        jewel.Media.HiResURLForWebDisplay, jewel.Title);

            viewModel.HiResJewelPicture = prettyMediaFactory.SinglePhotoUseLargeForBoth(jewel.Media.HiResURLForWebDisplay,
                                                                                        jewel.Title);


            

            var extraMedia = new List<prettyPhotoMedia>();

            if (fileSystem.File.Exists(jewel.Media.HiResDiskPathForWebDisplay))
            {
                extraMedia.Add(prettyMediaFactory.SinglePhotoUseLargeForBoth(jewel.Media.HiResURLForWebDisplay, jewel.Title));
            }

            if (fileSystem.File.Exists(jewel.Media.HandDiskPathForWebDisplay))
            {
                extraMedia.Add(prettyMediaFactory.SinglePhotoUseLargeForBoth(jewel.Media.HandURLForWebDisplay, jewel.Title));
            }

            if (fileSystem.File.Exists(jewel.Media.ReportDiskPathForWebDisplay))
            {
                extraMedia.Add(prettyMediaFactory.SinglePhotoUseLargeForBoth(jewel.Media.ReportURLForWebDisplay, "Report"));
            }

            viewModel.ExtraImages = extraMedia;

            if (fileSystem.File.Exists(jewel.Media.MovieDiskPathForWebDisplay))
            {
                viewModel.HasMovie = true;
                viewModel.Movie = jewel.Media.MovieURLForWebDisplay;
            }

            var price = new Money(jewel.Price, Currency.Usd).Format("{1}{0:#,0}");

            viewModel.Price = price;

            viewModel.RegularPrice = new Money(jewel.RegularPrice, Currency.Usd).Format("{1}{0:#,0}");

            viewModel.YouSave = String.Format("{0:0.##}%", Math.Round(100 - (jewel.Price / jewel.RegularPrice) * 100));

            viewModel.isSpecial = jewel.IsSpecial;

            viewModel.ItemNumber = jewel.ItemNumber;

            viewModel.Metal = jewel.MetalFullName();

            viewModel.Weight = jewel.Weight > 0 ? formatter.ToGramWeight(jewel.Weight) : "N/A";

            viewModel.Width = jewel.Width > 0 ? formatter.ToMilimeter(jewel.Width) : "N/A";

            viewModel.IsBestOffer = jewel.IsBestOffer;

            var specs = new List<JewelComponentInfoPart>();

            specs.Add(new JewelComponentInfoPart("Stone", jewel.JewelryExtra.CS.Type, 1));
            specs.Add(new JewelComponentInfoPart("Minimum carat total weight:",
                                                 formatter.ToCaratWeight(jewel.JewelryExtra.CS.Weight), 1));
            specs.Add(new JewelComponentInfoPart("# of Stones", jewel.JewelryExtra.CS.Count.ToString(), 1));

            var colors = new List<string>()
                             {
                                 "D",
                                 "E",
                                 "F",
                                 "G",
                                 "H",
                                 "I",
                                 "J",
                                 "K",
                                 "L",
                                 "M",
                                 "N",
                             };

            var clarities = new List<string>()
                                {
                                    "FL",
                                    "IF",
                                    "VVS1",
                                    "VVS2",
                                    "VS1",
                                    "VS2",
                                    "SI1",
                                    "SI2",
                                    "I1",
                                    "I2",
                                    "I3",
                                };
            colors.Reverse();
            clarities.Reverse();
            var wordsToSayHowTheQualityIs = "Minimum";
            if (jewel.JewelryExtra.CS.Count > 1)
            {
                wordsToSayHowTheQualityIs = "Average";
                specs.Add(new JewelComponentInfoPart(wordsToSayHowTheQualityIs + " Color", CreateRangeStringFrom(colors, jewel.JewelryExtra.CS.Color, 1), 1));
                specs.Add(new JewelComponentInfoPart(wordsToSayHowTheQualityIs + " Clarity", CreateRangeStringFrom(clarities, jewel.JewelryExtra.CS.Clarity, 1), 1));
            }
            else
            {
                specs.Add(new JewelComponentInfoPart(wordsToSayHowTheQualityIs + " Color", jewel.JewelryExtra.CS.Color, 1));
                specs.Add(new JewelComponentInfoPart(wordsToSayHowTheQualityIs + " Clarity", jewel.JewelryExtra.CS.Clarity, 1));
            }
            
            

            if (jewel.JewelryExtra.HasSideStones)
            {
                specs.Add(new JewelComponentInfoPart("Stone", jewel.JewelryExtra.SS.Type, 2));
                specs.Add(new JewelComponentInfoPart("Minimum carat total weight:",
                                                     formatter.ToCaratWeight(jewel.JewelryExtra.SS.Weight), 2));
                specs.Add(new JewelComponentInfoPart("# of Stones", jewel.JewelryExtra.SS.Count.ToString(), 2));
                specs.Add(new JewelComponentInfoPart("Average Color", CreateRangeStringFrom(colors, jewel.JewelryExtra.SS.Color, 1), 2));
                specs.Add(new JewelComponentInfoPart("Average Clarity", CreateRangeStringFrom(clarities, jewel.JewelryExtra.SS.Clarity, 1), 2));

                viewModel.HasSideStones = true;
            }

            viewModel.SpecsPool = specs;

            var testimonailsFromDB = testimonialRepository.GetRandomTestimonails(3);

            viewModel.Testimonials = mapper.Map<List<Testimonial>, List<TestimonialViewModel>>(testimonailsFromDB);

            viewModel.PageTitle = viewModel.Title + " - " + viewModel.Price;

            viewModel.JewelType = jewel.Type;

            return viewModel;
        }

        private string CreateRangeStringFrom(List<string> list,string current,int skip)
        {
            
            if (list.Last() == current)
            {
                return current;
            }
            if (!list.Contains(current))
            {
                return current;
            }

            return list.SkipWhile(x => x != current).Skip(skip).FirstOrDefault() + "-" + current;
        }

       
    }
}