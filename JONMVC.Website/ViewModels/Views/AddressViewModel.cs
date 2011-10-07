using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace JONMVC.Website.ViewModels.Views
{
    public class AddressViewModel
    {
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "*")]
        public string City { get; set; }

        [Required(ErrorMessage = "*")]
        [Min(2)]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Min(2)]
        public int StateID { get; set; }

        public string Country { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "*")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string Phone { get; set; }

    }
}