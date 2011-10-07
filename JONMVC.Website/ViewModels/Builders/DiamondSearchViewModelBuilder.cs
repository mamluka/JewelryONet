using System.Collections.Generic;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class DiamondSearchViewModelBuilder
    {
        private readonly CustomJewelPersistenceForDiamondSearch customJewelPersistenceForDiamondSearch;
        private readonly TabsForJewelDesignNavigationBuilder tabsForJewelDesignNavigationBuilder;

        public DiamondSearchViewModelBuilder(CustomJewelPersistenceForDiamondSearch customJewelPersistenceForDiamondSearch, TabsForJewelDesignNavigationBuilder tabsForJewelDesignNavigationBuilder)
        {
            this.customJewelPersistenceForDiamondSearch = customJewelPersistenceForDiamondSearch;
            this.tabsForJewelDesignNavigationBuilder = tabsForJewelDesignNavigationBuilder;
        }

        public DiamondSearchViewModel Build()
        {
            var viewModel = new DiamondSearchViewModel();
            viewModel.TabsForJewelDesignNavigation = tabsForJewelDesignNavigationBuilder.Build();

            viewModel.JSONClientScriptInitializer = new Dictionary<string, object>()
                                                        {
                                                            {"SettingID",customJewelPersistenceForDiamondSearch.SettingID.ToString()},
                                                            {"Shape",customJewelPersistenceForDiamondSearch.Shape},
                                                            {"Report",customJewelPersistenceForDiamondSearch.Report},
                                                            {"Size",customJewelPersistenceForDiamondSearch.Size},
                                                            {"MediaType",customJewelPersistenceForDiamondSearch.MediaType},
                                                        };

            return viewModel;

        }
    }
}