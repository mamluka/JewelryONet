using System;
using System.Collections.Generic;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Checkout;
using NUnit.Framework;
using Rhino.Mocks;
using JONMVC.Website.Tests.Unit;
using FluentAssertions;


namespace JONMVC.Website.Tests.Integration.Checkout
{
    [TestFixture]
    public class OrdersRepositoryTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Save_ShouldSaveTheOrderToTheDataBase()
        {
            //Arrange
            var order = new Order()
                            {
                                BillingAddress = new Address()
                                                     {
                                                         Address1 = "billingaddr1",
                                                         City = "billingcity",
                                                         CountryID = 1,
                                                         FirstName = "billingfirstname",
                                                         LastName = "billinglastname",
                                                         Phone = "billingphone",
                                                         StateID = 1,
                                                         ZipCode = "billingzipcode"
                                                     },
                                ShippingAddress = new Address()
                                                      {
                                                          Address1 = "shippingaddr1",
                                                          City = "shippingcity",
                                                          CountryID = 1,
                                                          FirstName = "shippingfirstname",
                                                          LastName = "shippinglastname",
                                                          Phone = "shippingphone",
                                                          StateID = 1,
                                                          ZipCode = "shippingzipcode"
                                                      },
                                Comment = "comment",
                                Email = "email8",
                                FirstName = "firstname",
                                LastName = "lastname",
                                PaymentID = 1,
                                Phone = "phone",
                                TotalPrice = 1000,
                                
                                CreditCard = new CreditCard()
                                                 {
                                                     CCV = "000",
                                                     CreditCardID = 1,
                                                     CreditCardsNumber = "12345",
                                                     Month = 1,
                                                     Year = 2011


                                                 },

                                Items = new List<ICartItem>()
                                            {

                                                FakeJewelCartItem(
                                                    Unit.Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT, Unit.Tests.SAMPLE_JEWEL_SIZE_725,
                                                    JewelMediaType.WhiteGold, 1000),
                                                FakeDiamondCartItem(
                                                    Unit.Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT, 2000),
                                                FakeCustomJewelCartItem(
                                                    Unit.Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT,
                                                    Unit.Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT, Unit.Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold, 3000)
                                            }


                            };

            var orderRepository = new OrderRepository(mapper);
            //Act
            orderRepository.Save(order);
            //Assert

        }

    }
}