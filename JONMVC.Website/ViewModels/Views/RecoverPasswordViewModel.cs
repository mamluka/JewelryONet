namespace JONMVC.Website.ViewModels.Views
{
    public class RecoverPasswordViewModel
    {
        public bool HasError { get; set; }
        

        public string Email { get; set; }

        public string ErrorMessage { get; set; }
    }
}