using AutoMapper;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.JewelDesign;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class EndViewModelBuilder
    {
        private readonly CustomJewelPersistenceInEndPage customJewelPersistenceInEndPage;
        private readonly TabsForJewelDesignNavigationBuilder tabsForJewelDesignBuilder;
        private readonly IDiamondRepository diamondRepository;
        private readonly IJewelRepository jewelRepository;
        private readonly IMappingEngine mapper;

        public EndViewModelBuilder(CustomJewelPersistenceInEndPage customJewelPersistenceInEndPage, TabsForJewelDesignNavigationBuilder tabsForJewelDesignBuilder, IDiamondRepository diamondRepository, IJewelRepository jewelRepository, IMappingEngine mapper)
        {
            this.customJewelPersistenceInEndPage = customJewelPersistenceInEndPage;
            this.tabsForJewelDesignBuilder = tabsForJewelDesignBuilder;
            this.diamondRepository = diamondRepository;
            this.jewelRepository = jewelRepository;
            this.mapper = mapper;
        }

        public EndViewModel Build()
        {
           


            var mapperHelp = new MergeDiamondAndJewel();

            mapperHelp.First = diamondRepository.GetDiamondByID(customJewelPersistenceInEndPage.DiamondID);

            jewelRepository.FilterMediaByMetal(customJewelPersistenceInEndPage.MediaType);
            mapperHelp.Second = jewelRepository.GetJewelByID(customJewelPersistenceInEndPage.SettingID);

            var viewModel = mapper.Map<MergeDiamondAndJewel,EndViewModel >(mapperHelp);

            viewModel.TabsForJewelDesignNavigation = tabsForJewelDesignBuilder.Build();

            viewModel.JewelPersistence = new CustomJewelPersistenceBase()
                                             {
                                                 DiamondID = customJewelPersistenceInEndPage.DiamondID,
                                                 SettingID = customJewelPersistenceInEndPage.SettingID,
                                                 Size = customJewelPersistenceInEndPage.Size,
                                                 MediaType = customJewelPersistenceInEndPage.MediaType
                                             };


            return viewModel;
        }
    }
}