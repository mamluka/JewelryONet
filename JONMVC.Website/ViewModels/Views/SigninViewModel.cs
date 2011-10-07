using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace JONMVC.Website.ViewModels.Views
{
    public class SigninViewModel:PageViewModelBase
    {
        [Required]
        [Email]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string RememberMe { get; set; }
        public bool HasError { get; set; }
    }
}