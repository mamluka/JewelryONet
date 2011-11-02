using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using Foolproof;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class ShoppingCartViewModel
    {
        public string TotalPrice { get; set; }

        [RequiredIfEmpty("LoginEmail")]
        public string FirstName { get; set; }

        [RequiredIfEmpty("LoginEmail")]
        public string LastName { get; set; }

        [RequiredIfEmpty("LoginEmail")]
        [Email(ErrorMessage = "*")]
        [Foolproof.EqualTo("ConfirmEmail")]
        public string Email { get; set; }

        [RequiredIfEmpty("LoginEmail")]
        [Email(ErrorMessage = "*")]
        public string ConfirmEmail { get; set; }

        [RequiredIfEmpty("LoginEmail")]
        [Digits(ErrorMessage = "*")]
        public string Phone { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public List<ICartItemViewModel> CartItems { get; set; }

        public bool IsSignedIn { get; set; }

        public string ErrorMessage { get; set; }

        [RequiredIfEmpty("Email")]
        [Email(ErrorMessage = "*")]
        public string LoginEmail { get; set; }

        [RequiredIfNotEmpty("LoginEmail")]
        public string LoginPassword { get; set; }
    }
}