using System;
using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using Mvc.Mailer;
using NMoneys;

namespace JONMVC.Website.Models.JewelryItem
{
    public class BestOffer : IBestOffer
    {
        private readonly IUserMailer userMailer;
        private readonly ISettingManager settingManager;
        private readonly IJewelRepository jewelRepository;
        private readonly IMappingEngine mapper;
        private DateTime dateTime;

        public BestOffer(IUserMailer userMailer, ISettingManager settingManager, IJewelRepository jewelRepository, IMappingEngine mapper)
        {
            this.userMailer = userMailer;
            this.settingManager = settingManager;
            this.jewelRepository = jewelRepository;
            this.mapper = mapper;
        }

        public void EmailToAdmin(BestOfferViewModel bestOfferViewModel)
        {
            try
            {
                var adminEmail = settingManager.AdminEmail();
                var model = CreateEmailModel(bestOfferViewModel);
                userMailer.BestOfferAdmin(adminEmail, model).Send();
            }
            catch (Exception ex)
            {

                throw new Exception("when asked to email the best offer email to the admin an error occured:\r\n" +ex.Message);
            }

        }

        public BestOfferEmailTemplateViewModel CreateEmailModel(BestOfferViewModel bestOfferViewModel)
        {
            try
            {
                var jewel = jewelRepository.GetJewelByID(bestOfferViewModel.JewelID);

                var templateModel = mapper.Map<Jewel, BestOfferEmailTemplateViewModel>(jewel);
                templateModel.OfferPrice = new Money(bestOfferViewModel.OfferPrice, Currency.Usd).Format("{1}{0:#,0}");
                templateModel.Email = bestOfferViewModel.OfferEmail;
                templateModel.OfferNumber = jewel.ID.ToString();
                templateModel.OfferDate = dateTime.ToString();
                return templateModel;
            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to map the viewmodel from request to a viewmodel for the email an error occured: \r\n" + ex.Message);
            }
            

        }

        public void SetTodayString(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }
    }
}