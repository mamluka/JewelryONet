using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Checkout
{
    public enum OrderStatus
    {
        [Description("Order Recieved")]
        OrderRecieved=0,

        [Description("Waiting For Payment")]
        WaitingForPayment=1,

        [Description("Order Confirmed")]
        OrderConfirmed=2,

        [Description("Waiting To Be Sent")]
        WaitingToBeSent=3,

        [Description("Order Sent")]
        OrderSent=4,

        [Description("Order Returned")]
        OrderReturned=5,

        [Description("Order Refunded")]
        OrderRefunded=6,

        [Description("Order Cancelled")]
        OrderCancelled=7
    }
}