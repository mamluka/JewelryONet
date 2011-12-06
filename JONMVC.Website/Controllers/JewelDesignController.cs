using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using AutoMapper.Mappers;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Controllers
{
    [ExitHttpsIfNotRequired]
    public class JewelDesignController : Controller
    {
        private readonly IDiamondRepository diamondRepository;
        private readonly IJONFormatter formatter;
        private readonly IMappingEngine mapper;
        private readonly IJewelRepository jewelRepository;
        private readonly IWebHelpers webHelpers;
        private readonly IXmlSourceFactory xmlSourceFactory;
        private readonly IFileSystem fileSystem;
        private readonly DiamondHelpBuilder diamondHelpBuilder;
        private readonly ITabsRepository tabsRepository;
        private readonly IPathBarGenerator pathBarGenerator;
        private readonly ITestimonialRepository testimonialRepository;
        //
        // GET: /JewelDesign/

        public JewelDesignController(IDiamondRepository diamondRepository, IJONFormatter formatter, IMappingEngine mapper, IJewelRepository jewelRepository, IWebHelpers webHelpers, IXmlSourceFactory xmlSourceFactory, IFileSystem fileSystem, DiamondHelpBuilder diamondHelpBuilder, ITabsRepository tabsRepository, IPathBarGenerator pathBarGenerator, ITestimonialRepository testimonialRepository)
        {
            this.diamondRepository = diamondRepository;
            this.formatter = formatter;
            this.mapper = mapper;
            this.jewelRepository = jewelRepository;
            this.webHelpers = webHelpers;
            this.xmlSourceFactory = xmlSourceFactory;
            this.fileSystem = fileSystem;
            this.diamondHelpBuilder = diamondHelpBuilder;
            this.tabsRepository = tabsRepository;
            this.pathBarGenerator = pathBarGenerator;
            this.testimonialRepository = testimonialRepository;
        }

        public ActionResult Index()
        {
            var viewModel = new EmptyViewModel();
            viewModel.PageTitle = "Design your own ring";

            viewModel.PathBarItems = pathBarGenerator.GenerateUsing<UsingDynamicTitlePathBarResolver, dynamic>(viewModel);
            return View(viewModel);
        }
        public ActionResult DiamondSearch(CustomJewelPersistenceForDiamondSearch customJewelPersistenceBaseClass)
        {

            var tabsForJewelDesignBuilder = new TabsForJewelDesignNavigationBuilder(customJewelPersistenceBaseClass, diamondRepository,
                                                                          jewelRepository, webHelpers);

            tabsForJewelDesignBuilder.WhichTabToHighLight(NagivationTabType.YourDiamond);

            var builder = new DiamondSearchViewModelBuilder(customJewelPersistenceBaseClass, tabsForJewelDesignBuilder);

            var viewModel = builder.Build();

            return View(viewModel);
        }
        public ActionResult Diamond(CustomJewelPersistenceInDiamond customJewelPersistenceInDiamond)
        {
            var tabsForJewelNavigation = new TabsForJewelDesignNavigationBuilder(customJewelPersistenceInDiamond, diamondRepository,
                                                                                 jewelRepository, webHelpers);

            tabsForJewelNavigation.WhichTabToHighLight(NagivationTabType.YourDiamond);

            var builder = new DiamondViewModelBuilder(customJewelPersistenceInDiamond,tabsForJewelNavigation,diamondRepository, diamondHelpBuilder,mapper);
            var viewModel = builder.Build();

            viewModel.PathBarItems = pathBarGenerator.GenerateUsing<UsingDynamicTitlePathBarResolver, dynamic>(viewModel);

            return View(viewModel);
        }

        public ActionResult ChooseSetting(ChooseSettingViewModel chooseSettingViewModel )
        {

            var tabsource = xmlSourceFactory.TabSource();

            var tabsViewModelBuilder = new TabsViewModelBuilder(chooseSettingViewModel, tabsource, tabsRepository, jewelRepository, fileSystem);

            var tabsForJewelNavigation = new TabsForJewelDesignNavigationBuilder(chooseSettingViewModel, diamondRepository,
                                                                    jewelRepository, webHelpers);

            tabsForJewelNavigation.WhichTabToHighLight(NagivationTabType.ChooseSetting);

           
            var builder = new ChooseSettingViewModelBuilder(chooseSettingViewModel, tabsViewModelBuilder, tabsForJewelNavigation);

            var viewModel = builder.Build();

            viewModel.PathBarItems = pathBarGenerator.GenerateUsing<TabsPathBarResolver, ITabsViewModel>(viewModel);

            return View(viewModel);
        }

        public ActionResult Setting(CustomJewelPersistenceForSetting customJewelPersistenceForSetting)
        {


            var tabsForJewelNavigation = new TabsForJewelDesignNavigationBuilder(customJewelPersistenceForSetting, diamondRepository,
                                                                     jewelRepository, webHelpers);

            tabsForJewelNavigation.WhichTabToHighLight(NagivationTabType.ChooseSetting);

            jewelRepository.FilterMediaByMetal(customJewelPersistenceForSetting.MediaType == 0 ? JewelMediaType.WhiteGold : customJewelPersistenceForSetting.MediaType);

            var jewelryItemViewModelBuilder = new JewelryItemViewModelBuilder(customJewelPersistenceForSetting.SettingID, jewelRepository,testimonialRepository ,fileSystem,mapper);

            var builder = new SettingViewModelBuilder(customJewelPersistenceForSetting, tabsForJewelNavigation, jewelryItemViewModelBuilder);

            var viewModel = builder.Build();

            viewModel.PathBarItems = pathBarGenerator.GenerateUsing<UsingDynamicTitlePathBarResolver, dynamic>(viewModel);


            return View(viewModel);
        }

        public ActionResult End(CustomJewelPersistenceInEndPage customJewelPersistenceInEndPage)
        {

            if (customJewelPersistenceInEndPage.MediaType == 0 ) 
            {
                customJewelPersistenceInEndPage.MediaType = JewelMediaType.WhiteGold;
            }

            var tabsForJewelNavigation = new TabsForJewelDesignNavigationBuilder(customJewelPersistenceInEndPage, diamondRepository,
                                                                                    jewelRepository, webHelpers);

            tabsForJewelNavigation.WhichTabToHighLight(NagivationTabType.YourOrder);

            var builder = new EndViewModelBuilder(customJewelPersistenceInEndPage, tabsForJewelNavigation, diamondRepository,
                                                 jewelRepository, mapper);

            var viewModel = builder.Build();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Diamonds(DiamondSearchParametersGivenByJson searchParameters)
        {

          //  
            var builder = new JsonDiamondsBuilder(searchParameters, diamondRepository, formatter,mapper, webHelpers);
            var json = builder.Build();
            return Json(json);
        }


        [HttpPost]
        public ActionResult ChooseSettingPost(ChooseSettingViewModel chooseSettingViewModel)
        {


            return RedirectToRoute("ChooseSetting", new RouteValueDictionary()
                                                        {
                                                            {"DiamondID", chooseSettingViewModel.DiamondID},
                                                            {"SettingID", chooseSettingViewModel.SettingID},
                                                            {"controller", "JewelDesign"},
                                                            {"action", "ChooseSetting"},
                                                            {"tabid", chooseSettingViewModel.TabId},
                                                            {"page", 1},
                                                            {"MetalFilter", chooseSettingViewModel.MetalFilter},
                                                            {"OrderByPrice", chooseSettingViewModel.OrderByPrice},
                                                            {"itemsperpage", chooseSettingViewModel.ItemsPerPage}

                                                        });
        }

        public ActionResult RedirectSetting(RedirectSettingModel redirectSettingModel)
        {
            switch (redirectSettingModel.CommandID)
            {
                case 1:

                    return RedirectToRoute("End", new RouteValueDictionary()
                                                           {
                                                               {"SettingID", redirectSettingModel.SettingID},
                                                               {"DiamondID", redirectSettingModel.DiamondID},
                                                               {"Size", redirectSettingModel.Size},
                                                               {"MediaType", redirectSettingModel.MediaType}
                                                           });

                case 2:

                    return RedirectToRoute("DiamondSearch", new RouteValueDictionary()
                                                           {
                                                               {"SettingID", redirectSettingModel.SettingID},
                                                               {"DiamondID", 0},
                                                               {"Size", redirectSettingModel.Size},
                                                               {"MediaType", redirectSettingModel.MediaType},
                                                   
                                                           });
             
                case 3:

                        return RedirectToAction("ShoppingCartAddJewel","Checkout", new RouteValueDictionary()
                                                           {
                                                               {"id", redirectSettingModel.SettingID},
                                                               {"size", redirectSettingModel.Size},
                                                               {"MediaType", redirectSettingModel.MediaType}
                                                           });

                default:
                    throw new Exception("When asked to redirect from jewel design setting page an invalid commandid was given:" + redirectSettingModel.CommandID);
            }
        }
    }
}
