using System;
using System.Linq;
using AutoMapper;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.ViewModels.Builders
{
    public class SpecialOffersBannervViewModelBuilder
    {
        private readonly IJewelRepository jewelRepository;
        private readonly IMappingEngine mapper;

        public SpecialOffersBannervViewModelBuilder(IJewelRepository jewelRepository, IMappingEngine mapper)
        {
            this.jewelRepository = jewelRepository;
            this.mapper = mapper;
        }

        public SpecialOffersBannervViewModel Build()
        {
            var jewel = jewelRepository.GetJewelsByDynamicSQL(new DynamicSQLWhereObject("onspecial = true")).OrderBy(x => Guid.NewGuid()).Take(1).ToList().FirstOrDefault(); 
            return mapper.Map<Jewel, SpecialOffersBannervViewModel>(jewel);
        }
    }
}