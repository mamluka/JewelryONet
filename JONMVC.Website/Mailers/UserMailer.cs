using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Models;
using JONMVC.Website.Models.Checkout;
using Mvc.Mailer;
using System.Net.Mail;

namespace JONMVC.Website.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer     
	{
		public UserMailer():
			base()
		{
			MasterName="_Layout";
		}


        public virtual MailMessage BestOfferAdmin(string mailTo, BestOfferEmailTemplateViewModel model)
        {
            return BestOfferCustomer(mailTo, model);
        }


        public virtual MailMessage BestOfferCustomer(string mailTo, BestOfferEmailTemplateViewModel model)
		{
            var mailMessage = new MailMessage { Subject = "Best offer confirmation from JewelryONet.com" };

            mailMessage.To.Add(mailTo);

            ViewData = new ViewDataDictionary<BestOfferEmailTemplateViewModel>(model);

            PopulateBody(mailMessage, "BestOfferCustomer");

            return mailMessage;
		}

        public MailMessage AskQuestion(string mailTo, AskQuestionEmailTemplateViewModel model)
        {
            var mailMessage = new MailMessage
                                  {
                                      Subject = "Question from " + model.Name,
                                      From = new MailAddress(model.Email)
                                  };


            mailMessage.To.Add(mailTo);
            

            ViewData = new ViewDataDictionary<AskQuestionEmailTemplateViewModel>(model);

            PopulateBody(mailMessage, "AskQuestion");

            return mailMessage;
        }

        public MailMessage OrderConfirmation(string mailTo, OrderConfirmationEmailTemplateViewModel model)
        {
            var mailMessage = new MailMessage { Subject = "Confirm order number:" + model.OrderNumber };

            mailMessage.To.Add(mailTo);

            ViewData = new ViewDataDictionary<OrderConfirmationEmailTemplateViewModel>(model);

            PopulateBody(mailMessage, "OrderConfirmation");

            return mailMessage;
        }

        public string TodayDate()
        {
            return DateTime.Now.ToString();
        }

        public MailMessage RecoverPassword(string mailTo,string lostPassword)
        {
            var mailMessage = new MailMessage { Subject = "Your account password for JewelryONet.com" };

            mailMessage.To.Add(mailTo);

            ViewBag.LostPassword = lostPassword;
            ViewBag.Email = mailTo;

            PopulateBody(mailMessage, "RecoverPassword");

            return mailMessage;
        }

        public MailMessage NewCustomer(Customer customer)
        {
            var mailMessage = new MailMessage { Subject = "Thank you for registering to JewelryONet" };

            mailMessage.To.Add(customer.Email);

            ViewBag.Password = customer.Password;
            ViewBag.Name = customer.FirstName + " " + customer.LastName;
            ViewBag.Email = customer.Email;
           

            PopulateBody(mailMessage, "NewCustomer");

            return mailMessage;
        }

        public MailMessage EmailRing(string mailTo, EmailRingEmailTemplateViewModel model)
        {
            var mailMessage = new MailMessage { Subject = model.YourName +  " wants you to checkout this jewel on JewelryONet.com" };

            mailMessage.To.Add(mailTo);

            ViewData = new ViewDataDictionary<EmailRingEmailTemplateViewModel>(model);

            PopulateBody(mailMessage, "EmailRing");

            return mailMessage;
        }
	}
}