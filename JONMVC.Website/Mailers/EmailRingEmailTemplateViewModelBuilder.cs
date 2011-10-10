using System;
using AutoMapper;
using JONMVC.Website.Models.Jewelry;
using NMoneys;

namespace JONMVC.Website.Mailers
{
    public class EmailRingEmailTemplateViewModelBuilder
    {
        private readonly EmailRingModel model;
        private readonly IJewelRepository jewelRepository;
        private readonly IMappingEngine mapper;

        public EmailRingEmailTemplateViewModelBuilder(EmailRingModel model, IJewelRepository jewelRepository,
                                                      IMappingEngine mapper)
        {
            this.model = model;
            this.jewelRepository = jewelRepository;
            this.mapper = mapper;
        }

        public EmailRingEmailTemplateViewModel Build()
        {
            try
            {
                var viewModel = mapper.Map<EmailRingModel, EmailRingEmailTemplateViewModel>(model);
                var jewel = jewelRepository.GetJewelByID(Convert.ToInt32(model.ID));
                if (jewel == null)
                {
                    throw new Exception("When asked to get an diamond with id:" + model.ID + " an error occured\r\n" +
                                        "Jewel was not found");
                }
                viewModel.Description = jewel.Title;
                viewModel.Price = new Money(jewel.Price, Currency.Usd).Format("{1}{0:#,0}");
                viewModel.ItemNumber = jewel.ItemNumber;
                viewModel.MediaSet = jewel.Media.MediaSet;
                viewModel.Icon = jewel.Media.IconURLForWebDisplay;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("when asked for a EmailRing Template an error occured\r\n" + ex.Message);
            }

        }
    }
}