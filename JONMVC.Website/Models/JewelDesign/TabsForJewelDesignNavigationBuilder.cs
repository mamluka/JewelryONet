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

            settingTab.ToolTipBody = "Start by choosing your setting following these steps:<br> <ul> <li>1. Choose a setting</li> <li>2. Choose a desired diamond</li> <li>3. Review your purchase in the \"Your Order\" tab before checkout</li> </ul> <br> Note: In any given time you can Modify or View your selected setting or diamond.";
            settingTab.ToolTipTitle = "Choose a Setting";

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
            
            settingTab.Title = "Choose a Setting";
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

            finalTab.ToolTipBody =
                "Review the diamond and the setting you have selected before proceeding to checkout.";
            finalTab.ToolTipTitle = "Your Order";

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

            diamondTab.ToolTipTitle = "Choose a Diamond";
            diamondTab.ToolTipBody =
                "Start by choosing your diamond following these steps:<br /> <ul> <li>1. Choose a diamond</li> <li>2. Choose a desired setting</li> <li>3. Review your purchase in the \"Your Order\" tab before checkout</li> </ul><br /> Note: In any given time you can Modify or View your selected diamond or setting.";
        

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