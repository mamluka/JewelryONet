using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Checkout
{
    public enum PaymentMethod
    {
        [Description("Credit card")]
        CraditCard=1,
        [Description("Wire transfer")]
        WireTransfer=2,
        [Description("PayPal")]
        PayPal=3
    }
}