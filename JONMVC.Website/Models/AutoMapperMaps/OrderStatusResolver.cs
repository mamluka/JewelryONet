using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.AutoMapperMaps
{
    public class OrderStatusResolver : ValueResolver<acc_ORDERS, OrderStatus>
    {
        protected override OrderStatus ResolveCore(acc_ORDERS source)
        {
            var status = OrderStatus.OrderRecieved;

            if (source.sts1_new_order_received )
            {
                status = OrderStatus.OrderRecieved;
            }

            if (source.sts3_waiting_for_payment )
            {
                status = OrderStatus.WaitingForPayment;
            }

            if (source.sts4_order_confirmed )
            {
                status = OrderStatus.OrderConfirmed;
            }

            if (source.sts7_order_waiting_to_be_send )
            {
                status = OrderStatus.WaitingToBeSent;
            }

            if (source.sts8_order_send )
            {
                status = OrderStatus.OrderSent;
            }

            if (source.sts12_customer_returning_order )
            {
                status = OrderStatus.OrderReturned;
            }

            if (source.sts14_customer_refunded )
            {
                status = OrderStatus.OrderRefunded;
            }

            if (source.sts17_order_cancelled )
            {
                status = OrderStatus.OrderCancelled;
            }

            return status;
        }
    }
}