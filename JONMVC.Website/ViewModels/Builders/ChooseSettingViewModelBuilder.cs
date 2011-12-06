using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class ChooseSettingViewModelBuilder
    {
        private readonly ChooseSettingViewModel chooseSettingViewModel;
        private readonly TabsViewModelBuilder tabsViewModelBuilder;
        private readonly TabsForJewelDesignNavigationBuilder tabsForJewelDesignBuilder;

        public ChooseSettingViewModelBuilder(ChooseSettingViewModel chooseSettingViewModel, TabsViewModelBuilder tabsViewModelBuilder, TabsForJewelDesignNavigationBuilder tabsForJewelDesignBuilder)
        {
            this.chooseSettingViewModel = chooseSettingViewModel;
            this.tabsViewModelBuilder = tabsViewModelBuilder;
            this.tabsForJewelDesignBuilder = tabsForJewelDesignBuilder;
        }


        public ChooseSettingViewModel Build()
        {
            var viewModel = tabsViewModelBuilder.Build<ChooseSettingViewModel>();
            viewModel.Size = chooseSettingViewModel.Size;
            viewModel.TabsForJewelDesignNavigation = tabsForJewelDesignBuilder.Build();
            viewModel.DiamondID = chooseSettingViewModel.DiamondID;
            viewModel.SettingID = chooseSettingViewModel.SettingID;
            return viewModel;
        }
    }
}