using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models;
using JONMVC.Website.Models.Checkout;
using Mvc.Mailer;
using System.Net.Mail;

namespace JONMVC.Website.Mailers
{ 
    public interface IUserMailer
    {
				
		MailMessage BestOfferAdmin(string mailTo, BestOfferEmailTemplateViewModel bestOfferViewModel);


        MailMessage BestOfferCustomer(string mailTo, BestOfferEmailTemplateViewModel bestOfferViewModel);

        MailMessage AskQuestionAdminVersion(string mailTo, AskQuestionEmailTemplateViewModel askQuestionEmailTemplateViewModel);

        MailMessage AskQuestionCustomerVersion(string mailTo, AskQuestionEmailTemplateViewModel askQuestionEmailTemplateViewModel);

        MailMessage OrderConfirmation(string mailTo, OrderConfirmationEmailTemplateViewModel model);

        MailMessage RecoverPassword(string name,string mailTo, string lostPassword);

        MailMessage NewCustomer(Customer customer);

        MailMessage EmailRing(string mailTo, EmailRingEmailTemplateViewModel model);

    }
}