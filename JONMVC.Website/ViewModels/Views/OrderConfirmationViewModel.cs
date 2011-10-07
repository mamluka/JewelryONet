namespace JONMVC.Website.ViewModels.Views
{
    public class OrderConfirmationViewModel
    {
        public string OrderNumber { get; set; }
        public string Email { get; set; }
        public bool HasError { get; set; }
    }
}