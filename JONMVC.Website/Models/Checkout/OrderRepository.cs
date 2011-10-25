using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AutoMapper;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.Models.Checkout
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMappingEngine mapper;

        public OrderRepository(IMappingEngine mapper)
        {
            this.mapper = mapper;
        }

        public int Save(Order orderdto)
        {

            using (var db = new JONEntities())
            {

                var totalPrice = orderdto.TotalPrice;

                var customer = mapper.Map<Order, usr_CUSTOMERS>(orderdto);


                if (CustomerExists(customer.email))
                {
                    
                    var customerExists = CustomerFromEmail(orderdto.Email);
                    customer.password = customerExists.password;
                    customer.id = customerExists.id;
                    db.usr_CUSTOMERS.Attach(customer);
                    db.ObjectStateManager.ChangeObjectState(customer, EntityState.Modified);
                }
                else
                {
                    customer.password = Guid.NewGuid().ToString().Substring(0, 8);
                    db.usr_CUSTOMERS.AddObject(customer);    
                }

                
             

           


                // ObjectSet<acc_ORDERS> orderset = new ObjectSet<acc_ORDERS>();

                var nextOrderNumber = db.acc_ORDERS.Count() + 1;

                var order = new acc_ORDERS
                                       {
                                           user_id = customer.id,
                                           OrderNumber = nextOrderNumber+10000,
                                           orderdate = DateTime.Now,
                                           campaign = "notset",
                                           affiliate = "notset",
                                           referrer = "notset",
                                           remote_ip = "notset",
                                           order_transacted = true,
                                           InvoiceNumber = 1,
                                           InvoiceCopy = true,
                                           InvoiceDate = DateTime.Now,
                                           JewelrySize = "",
                                           ClubOrder = false,
                                           packaging_id = 1,
                                           payment_id = orderdto.PaymentID,
                                           shipping_id = 1,
                                           shipping_tracking_no = "",
                                           amnt_discount = 0,
                                           amnt_extracharges = 0,
                                           amnt_grandtotal = totalPrice,
                                           amnt_items = totalPrice,
                                           amnt_labor = 0,
                                           amnt_shipping = 0,
                                           amnt_subtotal = totalPrice,
                                           amnt_vat = 0,
                                           amnt_wrapping = 0,
                                           adrs_billing_firstname = 
                                               orderdto.BillingAddress.FirstName,
                                           adrs_billing_lastname =   orderdto.BillingAddress.LastName,
                                           adrs_billing_city = orderdto.BillingAddress.City,
                                           adrs_billing_phone = orderdto.BillingAddress.Phone,
                                           adrs_billing_state_id = orderdto.BillingAddress.StateID,
                                           adrs_billing_street =
                                               orderdto.BillingAddress.Address1,
                                           adrs_billing_zip = orderdto.BillingAddress.ZipCode,
                                           adrs_delivery_country_id = orderdto.ShippingAddress.CountryID,
                                           adrs_delivery_firstname = 
                                               orderdto.ShippingAddress.FirstName, 
                                           adrs_delivery_lastname=    orderdto.ShippingAddress.LastName,
                                           adrs_delivery_city = orderdto.ShippingAddress.City,
                                           adrs_delivery_phone = orderdto.ShippingAddress.Phone,
                                           adrs_delivery_state_id = orderdto.ShippingAddress.StateID,
                                           adrs_delivery_street =
                                               orderdto.ShippingAddress.Address1,
                                           adrs_delivery_zip = orderdto.ShippingAddress.ZipCode,
                                           adrs_billing_country_id = orderdto.BillingAddress.CountryID,
                                           cannot_be_edited = false,
                                           OrderDeleted = false,
                                           Customer_Notes = orderdto.Comment ?? String.Empty,
                                           sts1_new_order_received = true,
                                           sts1_new_order_received_date = DateTime.Now,
                                           sts1_new_order_received_viewed = false,
                                           sts2_waiting_for_authorization = false,
                                           sts2_waiting_for_authorization_date = DateTime.Now,
                                           sts2_waiting_for_authorization_note = "",
                                           sts2_waiting_for_authorization_viewed = false,
                                           sts3_waiting_for_payment = false,
                                           sts3_waiting_for_payment_date = DateTime.Now,
                                           sts3_waiting_for_payment_note = "",
                                           sts3_waiting_for_payment_viewed = false,
                                           sts4_order_confirmed = false,
                                           sts4_order_confirmed_date = DateTime.Now,
                                           sts4_order_confirmed_note = "",
                                           sts4_order_confirmed_viewed = false,
                                           sts5_partial_order_confirmed = false,
                                           sts5_partial_order_confirmed_date = DateTime.Now,
                                           sts5_partial_order_confirmed_note = "",
                                           sts5_partial_order_confirmed_viewed = false,
                                           sts6_order_failed = false,
                                           sts6_order_failed_date = DateTime.Now,
                                           sts6_order_failed_note = "",
                                           sts6_order_failed_viewed = false,
                                           sts7_order_waiting_to_be_send = false,
                                           sts7_order_waiting_to_be_send_date = DateTime.Now,
                                           sts7_order_waiting_to_be_send_note = "",
                                           sts7_order_waiting_to_be_send_viewed = false,
                                           sts8_order_send = false,
                                           sts8_order_send_date = DateTime.Now,
                                           sts8_order_send_note = "",
                                           sts8_order_send_viewed = false,
                                           sts9_partial_order_send = false,
                                           sts9_partial_order_send_date = DateTime.Now,
                                           sts9_partial_order_send_note = "",
                                           sts9_partial_order_send_viewed = false,
                                           sts10_order_received_by_customer = false,
                                           sts10_order_received_by_customer_date = DateTime.Now,
                                           sts10_order_received_by_customer_note = "",
                                           sts10_order_received_by_customer_viewed = false,
                                           sts11_partial_order_received_by_customer = false,
                                           sts11_partial_order_received_by_customer_date = DateTime.Now,
                                           sts11_partial_order_received_by_customer_note = "",
                                           sts11_partial_order_received_by_customer_viewed = false,
                                           sts12_customer_returning_order = false,
                                           sts12_customer_returning_order_date = DateTime.Now,
                                           sts12_customer_returning_order_note = "",
                                           sts12_customer_returning_order_viewed = false,
                                           sts13_customer_returning_part_order = false,
                                           sts13_customer_returning_part_order_date = DateTime.Now,
                                           sts13_customer_returning_part_order_note = "",
                                           sts13_customer_returning_part_order_viewed = false,
                                           sts14_customer_refunded = false,
                                           sts14_customer_refunded_date = DateTime.Now,
                                           sts14_customer_refunded_note = "",
                                           sts14_customer_refunded_viewed = false,
                                           sts15_customer_partly_refunded = false,
                                           sts15_customer_partly_refunded_date = DateTime.Now,
                                           sts15_customer_partly_refunded_note = "",
                                           sts15_customer_partly_refunded_viewed = false,
                                           sts16_order_closed = false,
                                           sts16_order_closed_date = DateTime.Now,
                                           sts16_order_closed_note = "",
                                           sts16_order_closed_viewed = false,
                                           sts17_order_cancelled = false,
                                           sts17_order_cancelled_date = DateTime.Now,
                                           sts17_order_cancelled_note = "",
                                           sts17_order_cancelled_viewed = false,
                                           sts_curr_stat = "",
                                           sts_curr_date = DateTime.Now,
                                           Interest_start_date = DateTime.Now,
                                           Interest_percent = 0,
                                           Merchant_Notes = "",
                                           LastModify_Date = DateTime.Now,
                                           LastModify_User= "",
                                           LastModify_User_Id = customer.id,
                                           order_currency = "USD",
                                           order_currency_rate = 1,
                                           include_receipt = false,
                                           hear_fromus = ""
                                           

                                       };


                
                db.acc_ORDERS.AddObject(order);
       

                foreach (var item in orderdto.Items)
                {

                    switch (item.Type)
                    {
                        case CartItemType.Jewelry:
                            {
                                var cartItem = item as JewelCartItem;
                                var orderItem = new acc_JEWELRY_ORDER_ITEMS()
                                                    {
                                                        Item_id = cartItem.ID,
                                                        Item_quantity = 1,
                                                        description = "",
                                                        jewelsize = cartItem.Size,
                                                        metal = (int) cartItem.MediaType,
                                                        Item_no = "",
                                                        OrderNumber = order.OrderNumber

                                                    };

                                db.acc_JEWELRY_ORDER_ITEMS.AddObject(orderItem);
                            }
                            break;
                        case CartItemType.Diamond:
                            {
                                var cartItem = item as DiamondCartItem;
                                var orderItem = new acc_DIAMOND_ORDER_ITEMS()
                                {
                                    Item_id = cartItem.ID,
                                    Item_quantity = 1,
                                    description = "",
                                    OrderNumber = order.OrderNumber

                                };

                                db.acc_DIAMOND_ORDER_ITEMS.AddObject(orderItem);
                            }
                            break;
                        case CartItemType.CustomJewel:
                            {
                                var cartItem = item as CustomJewelCartItem;
                                var orderItem = new acc_CUSTOMJEWEL_ORDER_ITEMS()
                                {
                                    Setting_id = cartItem.SettingID,
                                    Diamond_id = cartItem.DiamondID,
                                    Item_quantity = 1,
                                    size = cartItem.Size,
                                    metal = (int)cartItem.MediaType,
                                    diamond_description = "",
                                    setting_description = "",
                                    OrderNumber = order.OrderNumber
                                };

                                db.acc_CUSTOMJEWEL_ORDER_ITEMS.AddObject(orderItem);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                switch (orderdto.PaymentID)
                {
                    case 1:
                        {
                            var payment = new acc_CASHFLOW()
                                              {
                                                  user_id = customer.id,
                                                  payment_type = 1,
                                                  LastModify_Date = DateTime.Now,
                                                  LastModify_User = customer.email,
                                                  LastModify_User_Id = customer.id,
                                                  amount_interest = 0,
                                                  amount_actual = totalPrice,
                                                  amount_costs = 0,
                                                  amount_total = totalPrice,
                                                  approved = false,
                                                  approved_date = DateTime.Now,
                                                  cc_batch = "",
                                                  cc_cleared = false,
                                                  cc_clubmember = false,
                                                  cc_confirmation = "",
                                                  cc_cvv = orderdto.CreditCard.CCV,
                                                  cc_exp_month = orderdto.CreditCard.Month.ToString(),
                                                  cc_exp_year = orderdto.CreditCard.Year.ToString(),
                                                  cc_name = orderdto.FirstName + " " + orderdto.LastName,
                                                  cc_number = orderdto.CreditCard.CreditCardsNumber,
                                                  cc_type_id = orderdto.CreditCard.CreditCardID,
                                                  cc_user_ssn = "",
                                                  cq_account = "",
                                                  cq_bank = "",
                                                  cq_date = DateTime.Now,
                                                  cq_name = "",
                                                  master = false,
                                                  mt_account = "",
                                                  mt_bank = "",
                                                  mt_code = "",
                                                  mt_name = "",
                                                  notes = "",
                                                  order_id = order.id,
                                                  paypal = false


                                              };

                            db.acc_CASHFLOW.AddObject(payment);
                        }
                        break;
                    case 2:
                        {
                            var payment = new acc_CASHFLOW()
                            {
                                payment_type = 2,
                                LastModify_Date = DateTime.Now,
                                LastModify_User = customer.email,
                                LastModify_User_Id = customer.id,
                                amount_interest = 0,
                                amount_actual = totalPrice,
                                amount_costs = 0,
                                amount_total = totalPrice,
                                approved = false,
                                approved_date = DateTime.Now,
                                cc_batch = "",
                                cc_cleared = false,
                                cc_clubmember = false,
                                cc_confirmation = "",
                                cc_user_ssn = "",
                                cq_account = "",
                                cq_bank = "",
                                cq_date = DateTime.Now,
                                cq_name = "",
                                master = false,
                                mt_account = "",
                                mt_bank = "",
                                mt_code = "",
                                mt_name = "",
                                notes = "",
                                order_id = order.id,
                                paypal = false,
                                user_id = customer.id

                            };

                            db.acc_CASHFLOW.AddObject(payment);
                        }
                        break;
                    case 3:
                        {
                            var payment = new acc_CASHFLOW()
                            {
                                payment_type = 3,
                                LastModify_Date = DateTime.Now,
                                LastModify_User = customer.email,
                                LastModify_User_Id = customer.id,
                                amount_interest = 0,
                                amount_actual = totalPrice,
                                amount_costs = 0,
                                amount_total = totalPrice,
                                approved = false,
                                approved_date = DateTime.Now,
                                cc_batch = "",
                                cc_cleared = false,
                                cc_clubmember = false,
                                cc_confirmation = "",
                                cc_user_ssn = "",
                                cq_account = "",
                                cq_bank = "",
                                cq_date = DateTime.Now,
                                cq_name = "",
                                master = false,
                                mt_account = "",
                                mt_bank = "",
                                mt_code = "",
                                mt_name = "",
                                notes = "",
                                order_id = order.id,
                                paypal = true,
                                user_id = customer.id


                            };

                            db.acc_CASHFLOW.AddObject(payment);
                        }
                        break;

                }

                db.SaveChanges();
                

                return order.OrderNumber;

            }
           
        }

        private usr_CUSTOMERS CustomerFromEmail(string email)
        {
            try
            {
                using (var db = new JONEntities())
                {

                    var customer = db.usr_CUSTOMERS.Where(x => x.email == email).SingleOrDefault();
                    if (customer != null)
                    {
                        return customer;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("When asked to get the id of a customer:" + email  + " an error occured:\r\n" + ex.Message);
            }
           
        }

        private bool CustomerExists(string email)
        {
            using (var db = new JONEntities())
            {
                var count = db.usr_CUSTOMERS.Where(x => x.email == email).Count();
                if (count>0)
                {
                    return true;
                }
                return false;
            }
        }

        public Order GetOrderByOrderNumber(int orderNumber)
        {
            try
            {
                using (var db = new JONEntities())
                {
                    var orderdto = db.acc_ORDERS.Where(x => x.OrderNumber == orderNumber).SingleOrDefault();
                    return mapper.Map<acc_ORDERS, Order>(orderdto);
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("When asked to get the orderid="+ orderNumber + " an error occured:" + ex.Message);
            }
            
        }

        public List<Order> GetOrdersByCustomerEmail(string email)
        {
            using (var db = new JONEntities())
            {
                var orders = db.acc_ORDERS.Where(x => x.usr_CUSTOMERS.email == email).ToList();
                return mapper.Map<List<acc_ORDERS>, List<Order>>(orders);
            }
        }
    }
}