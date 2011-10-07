using AutoMapper;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class DiamondViewModelBuilder
    {
        private readonly CustomJewelPersistenceInDiamond customJewelPersistence;
        private readonly IDiamondRepository diamondRepository;
        private readonly DiamondHelpBuilder diamondHelpBuilder;
        private readonly TabsForJewelDesignNavigationBuilder tabsForJewelDesignNavigationBuilder;
        private readonly IMappingEngine mapper;

        public DiamondViewModelBuilder(CustomJewelPersistenceInDiamond customJewelPersistence, TabsForJewelDesignNavigationBuilder tabsForJewelDesignNavigationBuilder, IDiamondRepository diamondRepository, DiamondHelpBuilder diamondHelpBuilder, IMappingEngine mapper)
        {
            this.customJewelPersistence = customJewelPersistence;
            this.diamondRepository = diamondRepository;
            this.diamondHelpBuilder = diamondHelpBuilder;
            this.tabsForJewelDesignNavigationBuilder = tabsForJewelDesignNavigationBuilder;
            this.mapper = mapper;
        }

        public DiamondViewModel Build()
        {
            var diamond = diamondRepository.GetDiamondByID(customJewelPersistence.DiamondID);
            //stage one we map
            var viewModel = mapper.Map<Diamond, DiamondViewModel>(diamond);
            //stage 2 we add things that we don't want to map
            viewModel.TabsForJewelDesignNavigation = tabsForJewelDesignNavigationBuilder.Build();

            viewModel.JewelPersistence = customJewelPersistence;

            viewModel.DiamondHelp = diamondHelpBuilder.Build(diamond);

            return viewModel;
        }
    }
}