using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;
using NMoneys;
using Mvc.Mailer;
namespace JONMVC.Website.Controllers
{
    [HandleError]
    [ExitHttpsIfNotRequired]
    public class JewelryItemController : Controller
    {
        private readonly IJewelRepository jewelRepository;
        private readonly IMediaSetBuilder mediaSetBuilder;
        private readonly IWebHelpers webHelpers;
        private readonly IFileSystem fileSystem;
        private readonly IBestOffer bestOffer;
        private readonly IWishListPersistence wishListPersistence;
        private readonly ITestimonialRepository testimonailRepository;
        private readonly IUserMailer mailer;
        private readonly IPathBarGenerator pathBarGenerator;
        private readonly IMappingEngine mapper;
        //
        // GET: /JewelryItem/

        public JewelryItemController(IJewelRepository jewelRepository, IMediaSetBuilder mediaSetBuilder, IWebHelpers webHelpers, IFileSystem fileSystem, IBestOffer bestOffer, IWishListPersistence wishListPersistence, ITestimonialRepository testimonailRepository, IUserMailer mailer, IPathBarGenerator pathBarGenerator, IMappingEngine mapper)
        {
            this.jewelRepository = jewelRepository;
            this.mediaSetBuilder = mediaSetBuilder;
            this.webHelpers = webHelpers;
            this.fileSystem = fileSystem;
            this.bestOffer = bestOffer;
            this.wishListPersistence = wishListPersistence;
            this.testimonailRepository = testimonailRepository;
            this.mailer = mailer;
            this.pathBarGenerator = pathBarGenerator;
            this.mapper = mapper;
        }

        public ActionResult Index(int id, JewelMediaType? nullableMediaSet)
        {

            var mediaSet = nullableMediaSet ?? JewelMediaType.WhiteGold;

            jewelRepository.FilterMediaByMetal(mediaSet);

            var builder = new JewelryItemViewModelBuilder(id, jewelRepository, testimonailRepository,fileSystem,mapper);
            try
            {
                var viewModel = builder.Build();
                viewModel.PathBarItems = pathBarGenerator.GenerateUsing<UsingDynamicTitlePathBarResolver, dynamic>(viewModel);

                return View(viewModel);
            }
            catch (ArgumentNullException)
            {

                throw new ArgumentNullException("Bad Request for an item");
            }



        }

        public ActionResult PostBestOffer(BestOfferViewModel model)
        {
            try
            {
                bestOffer.EmailToAdmin(model);

                return Json(new OporationWithoutReturnValueJsonModel());
            }
            catch (Exception ex)
            {

                return Json(new OporationWithoutReturnValueJsonModel(true, ex.Message));
            }
        }

        public ActionResult AddToWishList(int jewelID)
        {
            try
            {
               wishListPersistence.SaveID(jewelID);

                return Json(new OporationWithoutReturnValueJsonModel());
            }
            catch (Exception ex)
            {

                return Json(new OporationWithoutReturnValueJsonModel(true, ex.Message));
            }
        }

        public ActionResult AddToWishListRedirect(int jewelID)
        {
            try
            {
                wishListPersistence.SaveID(jewelID);

                return RedirectToAction("Wishlist", "Services");
            }
            catch (Exception ex)
            {

                throw new Exception("When trying to add item to the wishlist item id " + jewelID.ToString() + " an error occured:\r\n" + ex.Message );
            }
        }

        public ActionResult RemoveFromWishList(int jewelID)
        {
            wishListPersistence.RemoveID(jewelID);

            return RedirectToAction("Wishlist", "Services");
        }


        public ActionResult FindYourRingSize()
        {
            return View(new EmptyViewModel());
        }

        public ActionResult MediaSets(int jewelID)
        {

            var jewel = jewelRepository.GetJewelByID(jewelID);

            var builder = new MediaSetsJsonModelBuilder(jewel,mediaSetBuilder, webHelpers);

            var viewModel = builder.Build();

            return Json(viewModel);
        }

        public ActionResult EmailRing(EmailRingModel model)
        {

            try
            {
                var builder = new EmailRingEmailTemplateViewModelBuilder(model, jewelRepository, mapper);
                var template = builder.Build();
                mailer.EmailRing(model.FriendEmail, template).Send();
                return Json(new OporationWithoutReturnValueJsonModel());
            }
            catch (Exception ex)
            {
                var viewModel = new OporationWithoutReturnValueJsonModel();
                viewModel.HasError = true;
                viewModel.ErrorMessage = ex.Message;

                return Json(viewModel);
            }
           
        }



       
    }
} 
