namespace JONMVC.Website.Models.Checkout
{
    public class CreditCard
    {
        public string CreditCardsNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CCV { get; set; }
        public int CreditCardID { get; set; }
    }
}