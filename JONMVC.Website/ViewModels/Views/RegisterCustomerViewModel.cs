using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;

namespace JONMVC.Website.ViewModels.Views
{
    public class RegisterCustomerViewModel
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public bool HasError { get; set; }
        public CustomerCreationError CreateStatus { get; set; }

        [Required]
        public string Phone { get; set; }

        public bool SignupForNewsletter { get; set; }
    }

    public class CustomerCreationError
    {
        private readonly MembershipCreateStatus status;

        public CustomerCreationError(MembershipCreateStatus status)
        {
            this.status = status;
        }


        public MembershipCreateStatus Status
        {
            get { return status; }
        }
    }
}