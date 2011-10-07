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

            if (source.sts1_new_order_received ?? false)
            {
                status = OrderStatus.OrderRecieved;
            }

            if (source.sts3_waiting_for_payment ?? false)
            {
                status = OrderStatus.WaitingForPayment;
            }

            if (source.sts4_order_confirmed ?? false)
            {
                status = OrderStatus.OrderConfirmed;
            }

            if (source.sts7_order_waiting_to_be_send ?? false)
            {
                status = OrderStatus.WaitingToBeSent;
            }

            if (source.sts8_order_send ?? false)
            {
                status = OrderStatus.OrderSent;
            }

            if (source.sts12_customer_returning_order ?? false)
            {
                status = OrderStatus.OrderReturned;
            }

            if (source.sts14_customer_refunded ?? false)
            {
                status = OrderStatus.OrderRefunded;
            }

            if (source.sts17_order_cancelled ?? false)
            {
                status = OrderStatus.OrderCancelled;
            }

            return status;
        }
    }
}