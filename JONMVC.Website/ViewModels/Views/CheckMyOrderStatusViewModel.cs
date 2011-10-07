using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace JONMVC.Website.ViewModels.Views
{
    public class CheckMyOrderStatusViewModel
    {
        [Email(ErrorMessage = "*")]
        [Required]
        public string Email { get; set; }
        [Required]
        [Integer(ErrorMessage = "*")]
        public string OrderNumber { get; set; }
        public bool HasError { get; set; }
    }
}