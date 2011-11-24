using System;
using JONMVC.Website.Mailers;

namespace JONMVC.Website.Models.JewelryItem
{
    public interface IBestOffer
    {
        void EmailToAdmin(BestOfferViewModel bestOfferViewModel);
        void EmailToCustomer(BestOfferViewModel bestOfferViewModel);
        BestOfferEmailTemplateViewModel CreateEmailModel(BestOfferViewModel bestOfferViewModel);
        void SetTodayString(DateTime dateTime);
    }
}