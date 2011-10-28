using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using JONMVC.Website.Models.Checkout;

namespace JONMVC.Website.ViewModels.Views
{
    public class ShoppingCartViewModel
    {
        public string TotalPrice { get; set; }

        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [Email(ErrorMessage = "*")]
        [Compare("ConfirmEmail",ErrorMessage = "*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Email(ErrorMessage = "*")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "*")]
        [Digits(ErrorMessage = "*")]
        public string Phone { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public List<ICartItemViewModel> CartItems { get; set; }

        public bool IsSignedIn { get; set; }

        public string ErrorMessage { get; set; }
    }
}