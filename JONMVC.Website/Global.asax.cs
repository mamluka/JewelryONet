using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bootstrap;
using Bootstrap.AutoMapper;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.ViewModels.Builders;

namespace JONMVC.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Tabs", // Route name
                "Tabs/{tabkey}/{tabid}/{page}/{MetalFilter}/{OrderByPrice}/{itemsperpage}",
                // URL with parameters
                new
                    {
                        controller = "Tabs",
                        action = "SearchTabs",
                        tabkey = "diamond-rings",
                        tabid = "engagement-rings",
                        page = 1,
                        MetalFilter = JewelMediaType.WhiteGold,
                        OrderByPrice = OrderByPrice.LowToHigh,
                        itemsperpage = "20"
                    } // Parameter defaults
                ,
                new
                    {
                        httpMethod = new HttpMethodConstraint("GET") 
                    }
                );


            routes.MapRoute(
                "JewelryItem", // Route name
                "Buy/{id}/{nullableMediaSet}", // URL with parameters
                new { controller = "JewelryItem", action = "Index", id = UrlParameter.Optional, nullableMediaSet = JewelMediaType.WhiteGold }
                // Parameter defaults
                );


    
            routes.MapRoute(
                "ShoppingCartRemoveitem", // Route name
                "Checkout/ShoppingCart/Remove/{cartid}", // URL with parameters
                new { controller = "Checkout", action = "RemoveShoppingCartItem", cartid = 0 }

                // Parameter defaults
                );

            routes.MapRoute(
                "ShoppingCartChangeJewelSize", // Route name
                "Checkout/ShoppingCart/JewelSize/{cartid}/{size}/{MediaType}", // URL with parameters
                new { controller = "Checkout", action = "JewelSize", cartid = 0, size = "7.25", MediaType = JewelMediaType.WhiteGold  }

                // Parameter defaults
                );

            routes.MapRoute(
                "BestOffer", // Route name
                "BestOffer/{tabkey}/{tabid}/{page}/{MetalFilter}/{OrderByPrice}/{itemsperpage}",
                // URL with parameters
                new
                    {
                        controller = "Tabs",
                        action = "SearchTabs",
                        tabkey = "best-offer-items",
                        tabid = "offers",
                        page = 1,
                        MetalFilter = JewelMediaType.WhiteGold,
                        OrderByPrice = OrderByPrice.LowToHigh,
                        itemsperpage = 20
                    } // Parameter defaults
                    ,
                new
                    {
                        httpMethod = new HttpMethodConstraint("GET")
                    }
                );

            routes.MapRoute(
                "SpecialOffers", // Route name
                "SpecialOffers/{tabkey}/{tabid}/{page}/{MetalFilter}/{OrderByPrice}/{itemsperpage}",
                // URL with parameters
                new
                    {
                        controller = "Tabs",
                        action = "SearchTabs",
                        tabkey = "special-offers-items",
                        tabid = "special-offers",
                        page = 1,
                        MetalFilter = JewelMediaType.WhiteGold,
                        OrderByPrice = OrderByPrice.LowToHigh,
                        itemsperpage = 20
                    } // Parameter defaults
                ,
                new
                    {
                        httpMethod = new HttpMethodConstraint("GET")
                    }

                );

            routes.MapRoute(
                "3DGallery", // Route name
                "3DGallery/{tabkey}/{tabid}/{page}/{MetalFilter}/{OrderByPrice}/{itemsperpage}",
                // URL with parameters
                new
                {
                    controller = "Tabs",
                    action = "SearchTabs",
                    tabkey = "3d-gallery",
                    tabid = "gallery",
                    page = 1,
                    MetalFilter = JewelMediaType.WhiteGold,
                    OrderByPrice = OrderByPrice.LowToHigh,
                    itemsperpage = 20
                } // Parameter defaults
                ,
                new
                {
                    httpMethod = new HttpMethodConstraint("GET")
                }

                );

       


            routes.MapRoute(
                "DiamondSearch", // Route name
                "JewelDesign/DiamondSearch/{DiamondID}/{SettingID}/{Size}/{MediaType}/{Shape}/{Report}", // URL with parameters
                new { controller = "JewelDesign", action = "DiamondSearch", DiamondID = 0, SettingID = 0,Shape="Round",Report="All",Size="7",MediaType = JewelMediaType.WhiteGold } // Parameter defaults
                );

            routes.MapRoute(
                "Diamond", // Route name
                "JewelDesign/Diamond/{DiamondID}/{SettingID}/{Size}/{MediaType}", // URL with parameters
                new { controller = "JewelDesign", action = "Diamond", DiamondID = 0, SettingID = 0, Size = "7", MediaType = JewelMediaType.WhiteGold } // Parameter defaults
                );


            routes.MapRoute(
                "ChooseSetting", // Route name
                "JewelDesign/ChooseSetting/{TabId}/{page}/{MetalFilter}/{OrderByPrice}/{itemsperpage}/{DiamondID}/{SettingID}/{Size}/{MediaType}", // URL with parameters
                new
                    {
                        controller = "JewelDesign",
                        action = "ChooseSetting",
                        TabId = "solitaire-settings",
                        page = 1,
                        MetalFilter = "1",
                        OrderByPrice = "1",
                        itemsperpage = "20",
                        DiamondID=0,
                        SettingID = 0,
                        Size = "7",
                        MediaType = JewelMediaType.WhiteGold
                    } // Parameter defaults
                );

            routes.MapRoute(
                "Setting", // Route name
                "JewelDesign/Setting/{DiamondID}/{SettingID}/{Size}/{MediaType}", // URL with parameters
                new { controller = "JewelDesign", action = "Setting", DiamondID = "0", SettingID = "0", Size = "7", MediaType = JewelMediaType.WhiteGold } // Parameter defaults
                );

            routes.MapRoute(
                "End", // Route name
                "JewelDesign/End/{DiamondID}/{SettingID}/{Size}/{MediaType}", // URL with parameters
                new { controller = "JewelDesign", action = "End", DiamondID = "0", SettingID = "0", Size = "7", MediaType = JewelMediaType.WhiteGold } // Parameter defaults
                );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

           // var binpath = AppDomain.CurrentDomain.GetData("PRIVATE_BINPATH").ToString();

            //throw new Exception(binpath + @"\JONMVC.Core.dll");

            Bootstrapper.With.AutoMapper().Start();


        }

    }
}