namespace JONMVC.Website.Models.Checkout
{
    public interface ICartItemViewModel
    {
        string PartialName { get; }
        bool NoActionLinkDispalyOnly { get; set; }
    }
}