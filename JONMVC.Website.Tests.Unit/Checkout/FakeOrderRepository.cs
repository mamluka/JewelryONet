using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Checkout
{
    public class FakeOrderRepository:IOrderRepository
    {

        private static Fixture fixture = new Fixture();

        private List<acc_ORDERS> dbmock = new List<acc_ORDERS>()
                                              {
                                                  fixture.Build<acc_ORDERS>().With(x => x.OrderNumber,Tests.FAKE_ORDERNUMBER)
                                                      .Without(x => x.EntityKey)
                                                      .Without(x => x.usr_CUSTOMERSReference)
                                                      .Without(x => x.usr_CUSTOMERS)
                                                      .Without(x => x.sys_COUNTRY1Reference)
                                                      .Without(x => x.sys_COUNTRYReference)
                                                      .Without(x => x.sys_STATE1Reference)
                                                      .Without(x => x.sys_STATEReference)
                                                      .Without(x => x.acc_CUSTOMJEWEL_ORDER_ITEMS)
                                                      .Without(x => x.acc_DIAMOND_ORDER_ITEMS)
                                                      .Without(x => x.acc_JEWELRY_ORDER_ITEMS)
                                                      .Without(x => x.acc_CASHFLOW)
                                                      .Without(x => x.sys_STATE)
                                                      .Without(x => x.sys_STATE1)
                                                      .Without(x => x.sys_COUNTRY)
                                                      .Without(x => x.sys_COUNTRY1)

                                                      .CreateAnonymous()
                                              };

        

        public int Save(Order orderdto)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByOrderNumber(int orderNumber)
        {
            try
            {
                var orderdto = dbmock.Where(x => x.OrderNumber == orderNumber).SingleOrDefault();
                orderdto.sys_COUNTRYReference= new EntityReference<sys_COUNTRY>()
                                                   {
                                                       Value = new sys_COUNTRY()
                                                   };

                var order = Mapper.Map<acc_ORDERS, Order>(orderdto);

                return order;

            }
            catch (Exception ex)
            {
                
                throw new Exception("when asked to get the order with the ordernumber:" + orderNumber + " an error occured\r\n" + ex.Message);
            }
        }

        public List<Order> GetOrdersByCustomerEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}