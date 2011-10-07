using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace JONMVC.Website.ViewModels.Views
{
    public class CreditCardViewModel
    {
        public string CreditCart { get; set; }

        [CreditCard(ErrorMessage = "*")]
        [Required(ErrorMessage = "*")]
        public string CreditCardsNumber { get; set; }


        public int Month { get; set; }


        public int Year { get; set; }

        [Required(ErrorMessage = "*")]
        [Max(999,ErrorMessage = "*")]
        [Digits(ErrorMessage = "*")]
        public string CCV { get; set; }

        [Min(2)]
        public int CreditCardID { get; set; }
    }
}