using System.Collections.Generic;
using System.Web.Routing;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using System.Linq;
using NMoneys;

namespace JONMVC.Website.Models.JewelDesign
{
    public class TabsForJewelDesignNavigationBuilder
    {
        private readonly CustomJewelPersistenceBase customJewelPersistenceBase;
        private readonly IDiamondRepository diamondRepository;
        private readonly IJewelRepository jewelRepository;
        private readonly IWebHelpers webHelpers;
        private Diamond diamond;
        private Jewel setting;
        private NagivationTabType nagivationTabType;

        public TabsForJewelDesignNavigationBuilder(CustomJewelPersistenceBase customJewelPersistenceBase, IDiamondRepository diamondRepository, IJewelRepository jewelRepository, IWebHelpers webHelpers)
        {
            this.customJewelPersistenceBase = customJewelPersistenceBase;
            this.diamondRepository = diamondRepository;
            this.jewelRepository = jewelRepository;
            this.webHelpers = webHelpers;
            this.nagivationTabType = NagivationTabType.YourDiamond;
        }

        public List<NavigationTab> Build()
        {
            var list = new List<NavigationTab>();

            if (customJewelPersistenceBase.DiamondID > 0 )
            {
                diamond = diamondRepository.GetDiamondByID(customJewelPersistenceBase.DiamondID);
            }

            if (customJewelPersistenceBase.SettingID > 0)
            {
                setting = jewelRepository.GetJewelByID(customJewelPersistenceBase.SettingID);
            }

           

            var diamondTab = BuildDiamondTab();
            list.Add(diamondTab);

            var settingTab = BuildSettingTab();
            list.Add(settingTab);

            var finalTab = BuildFinalTab();

            list.Add(finalTab);


            list = list.Select(ForEachTabEvaluateTheHighlightCSSClass).ToList();


            return list;
        }

        private NavigationTab ForEachTabEvaluateTheHighlightCSSClass(NavigationTab x)
        {
            if (x.Type == this.nagivationTabType)
            {
                x.HighlightState = "on";
            }

            return x;
        }


        private NavigationTab BuildSettingTab()
        {
            var settingTab = new NavigationTab();
            
            settingTab.Type = NagivationTabType.ChooseSetting;

            if (customJewelPersistenceBase.SettingID > 0)
            {
                settingTab.Title = "Your Setting";
                settingTab.Amount = new Money((decimal)setting.Price, Currency.Usd).Format("{1}{0:#,0}");
                settingTab.ViewRoute = webHelpers.RouteUrl("Setting",
                                                           CreatePersistenceRouteValuesDic());
                settingTab.ModifyRoute = webHelpers.RouteUrl("ChooseSetting",
                                                           CreatePersistenceRouteValuesDic());
                settingTab.HighlightState = "off";
                settingTab.CssClass = "setting";
                settingTab.HasEditAndViewLinks = true;

                return settingTab;
            } 
            
            settingTab.Title = "Choose Setting";
            settingTab.Amount = new Money(0, Currency.Usd).Format("{1}{0:#,0}"); ;
            settingTab.HighlightState = "off";
            settingTab.CssClass = "setting";
            
            return settingTab;
        }

      
        private NavigationTab BuildFinalTab()
        {
            var finalTab = new NavigationTab();

            finalTab.Type = NagivationTabType.YourOrder;

            decimal price = 0;

            if (customJewelPersistenceBase.DiamondID>0)
            {
                price += diamond.Price;
            }
            if (customJewelPersistenceBase.SettingID>0)
            {
                price += (decimal)setting.Price;
            }
            finalTab.Title = "Your Order";
            finalTab.Amount = new Money(price, Currency.Usd).Format("{1}{0:#,0}");
            finalTab.HighlightState = "off";
            finalTab.CssClass = "end";
          
            return finalTab;
        }

        private NavigationTab BuildDiamondTab()
        {
            var diamondTab = new NavigationTab();

            diamondTab.Type = NagivationTabType.YourDiamond;

            if (customJewelPersistenceBase.DiamondID > 0)
            {
                
                diamondTab.Title = "Your Diamond";
                diamondTab.Amount = new Money(diamond.Price, Currency.Usd).Format("{1}{0:#,0}");
                diamondTab.HighlightState = "off";
                diamondTab.HasEditAndViewLinks = true;
                diamondTab.ViewRoute = webHelpers.RouteUrl("Diamond", CreatePersistenceRouteValuesDic());
                diamondTab.ModifyRoute = webHelpers.RouteUrl("DiamondSearch",CreatePersistenceRouteValuesDic());
                diamondTab.CssClass = "diamond";
                return diamondTab;
            }
           
            diamondTab.Title = "Choose Diamond";
            diamondTab.Amount = new Money(0, Currency.Usd).Format("{1}{0:#,0}");
            diamondTab.HighlightState = "off";
            diamondTab.CssClass = "diamond";
        
            
            return diamondTab;
        }

        private RouteValueDictionary CreatePersistenceRouteValuesDic()
        {
            return new RouteValueDictionary()
                       {
                           {"SettingID", customJewelPersistenceBase.SettingID},
                           {"DiamondID", customJewelPersistenceBase.DiamondID},
                           {"Size",customJewelPersistenceBase.Size},
                           {"MediaType",customJewelPersistenceBase.MediaType}
                       };
        }


        public void WhichTabToHighLight(NagivationTabType nagivationTabType)
        {
            this.nagivationTabType = nagivationTabType;
        }
    }
}