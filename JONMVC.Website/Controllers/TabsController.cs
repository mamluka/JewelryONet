using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using AutoMapper;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Controllers
{
    
	public class TabsController : Controller
	{
		private readonly ITabsRepository tabsRepository;
		private readonly IJewelRepository jewelRepository;
		private readonly IFileSystem fileSystem;
		private readonly IXmlSourceFactory xmlSourceFactory;
		private readonly IPathBarGenerator pathBarGenerator;
        private readonly IMappingEngine mapper;
        //
		// GET: /Tabs/

		public TabsController(ITabsRepository tabsRepository, IJewelRepository jewelRepository, IFileSystem fileSystem, IXmlSourceFactory xmlSourceFactory, IPathBarGenerator pathBarGenerator, IMappingEngine mapper)
		{
			this.tabsRepository = tabsRepository;
			this.jewelRepository = jewelRepository;
			this.fileSystem = fileSystem;
			this.xmlSourceFactory = xmlSourceFactory;
			this.pathBarGenerator = pathBarGenerator;
		    this.mapper = mapper;
		}
        [ExitHttpsIfNotRequiredAttribute]
		public ViewResult SearchTabs(TabsViewModel viewModel)
		{
			XDocument tabsource = xmlSourceFactory.TabSource();

			TabsViewModelBuilder modelBuilder = new TabsViewModelBuilder(viewModel, tabsource, tabsRepository, jewelRepository, fileSystem);

			viewModel = modelBuilder.Build();
			viewModel.PathBarItems = pathBarGenerator.GenerateUsing<TabsPathBarResolver, ITabsViewModel>(viewModel);

			return View("PresentTabs",viewModel);
		}

        [ExitHttpsIfNotRequiredAttribute]
		[HttpPost]
		public ActionResult SearchTabsPost(TabsViewModel viewModel)
		{


            return RedirectToRoute("Tabs", new RouteValueDictionary()
																	 {
																		 {"tabkey", viewModel.TabKey},
																		 {"tabid", viewModel.TabId},
																		 {"page", viewModel.Page},
																		 {"MetalFilter", viewModel.MetalFilter},
																		 {"OrderByPrice", viewModel.OrderByPrice},
																		 {"itemsperpage", viewModel.ItemsPerPage}

						
																	 });
		}

        public ActionResult SpecialOffersBanner()
        {
            var builder = new SpecialOffersBannervViewModelBuilder(jewelRepository, mapper);
            var viewModel = builder.Build();

            return View(viewModel);
        }
	    
	}
}
