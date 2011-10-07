using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.ViewModels.Views;
using System.Linq;
namespace JONMVC.Website.ViewModels.Builders
{
    public class SettingViewModelBuilder
    {
        private readonly CustomJewelPersistenceForSetting customJewelForSetting;
        private readonly TabsForJewelDesignNavigationBuilder tabsForJewelDesignBuilder;
        private readonly JewelryItemViewModelBuilder jewelryItemViewModelBuilder;

        public SettingViewModelBuilder(CustomJewelPersistenceForSetting customJewelForSetting, TabsForJewelDesignNavigationBuilder tabsForJewelDesignBuilder, JewelryItemViewModelBuilder jewelryItemViewModelBuilder)
        {
            this.customJewelForSetting = customJewelForSetting;
            this.tabsForJewelDesignBuilder = tabsForJewelDesignBuilder;
            this.jewelryItemViewModelBuilder = jewelryItemViewModelBuilder;
        }

        public SettingViewModel Build()
        {
            var viewModel = jewelryItemViewModelBuilder.Build<SettingViewModel>();

            const int theInfoPartIDForTheCenterStoneThatMustBeHidden = 1;
            viewModel.SpecsPool.RemoveAll(x => x.JewelComponentID == theInfoPartIDForTheCenterStoneThatMustBeHidden);

            viewModel.TabsForJewelDesignNavigation = tabsForJewelDesignBuilder.Build();
            viewModel.JewelPersistence = new CustomJewelPersistenceBase()
                                             {
                                                 DiamondID = customJewelForSetting.DiamondID,
                                                 SettingID = customJewelForSetting.SettingID,
                                                 Size = customJewelForSetting.Size,
                                                 MediaType = customJewelForSetting.MediaType
                                             };
            return viewModel;
        }
    }
}