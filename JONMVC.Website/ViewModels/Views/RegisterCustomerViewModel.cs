using System.ComponentModel.DataAnnotations;
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
        
        public int CountryID { get; set; }
        [Required]
        public int StateID { get; set; }

        public bool HasError { get; set; }
        public CustomerCreationError CreateStatus { get; set; }

        [Required]
        public string Phone { get; set; }
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