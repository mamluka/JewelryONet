using AutoMapper;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;

namespace JONMVC.Website.Tests.Unit.Checkout
{
    public class CheckoutTestsBaseClass
    {
        protected IMappingEngine mapper;
        protected Fixture fixture;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        /// 
        [TestFixtureSetUp]
        public void InitializeFixture()
        {
           // Bootstrapper.Excluding.Assembly("JONMVC.Core.Configurations").With.AutoMapper().Start();
            MapsContainer.CreateAutomapperMaps();

            mapper = Mapper.Engine;
            fixture = new Fixture();
        }

        public static ICartItem StubCartItem(int id, decimal price, CartItemType type)
        {
            var cartItem = MockRepository.GenerateMock<ICartItem>();
            cartItem.Stub(x => x.ID).Return(id);
            cartItem.Stub(x => x.Price).Return(price);
            cartItem.Stub(x => x.Type).Return(type);
            return cartItem;
        }

        public static JewelCartItem FakeJewelCartItem(int id, string size, JewelMediaType mediaType,decimal price)
        {
            var cartItem = new JewelCartItem(id, mediaType,price);
            cartItem.SetSize(size);
            return cartItem;
        }

        public static DiamondCartItem FakeDiamondCartItem(int id,decimal price)
        {
            var cartItem = new DiamondCartItem(id, price);
           
            return cartItem;
        }

        public static CustomJewelCartItem FakeCustomJewelCartItem(int diamondid, int settingid, string size, JewelMediaType mediaType, decimal price)
        {
            var cartItem = new CustomJewelCartItem(diamondid, settingid, size, mediaType, price);
            return cartItem;
        }

        public static CheckoutDetailsModel DefaultCheckoutDetailsModel()
        {
            var details = new CheckoutDetailsModel()
                              {
                                  BillingAddress = new AddressViewModel()
                                                       {
                                                           Address1 = "billingaddr1",
                                                          
                                                           City = "billingcity",
                                                           CountryID = 10,
                                                           FirstName = "billingfirstname",
                                                           LastName = "billinglastname",
                                                           Phone = "billingphone",
                                                           StateID = 20,
                                                           ZipCode = "billingzipcode",
                                                           Country = "billingcountry",
                                                           State = "billingstate"
                                                           
                                                      
                                                           
                                                           

                                                           
                                                       },
                                  ShippingAddress = new AddressViewModel()
                                                        {
                                                            Address1 = "shippingaddr1",
                                                            
                                                            City = "shippingcity",
                                                            CountryID = 10,
                                                            FirstName = "shippingfirstname",
                                                            LastName = "shippinglastname",
                                                            Phone = "shippingphone",
                                                            StateID = 20,
                                                            ZipCode = "shippingzipcode",
                                                            Country = "shippingcountry",
                                                            State = "shippingstate"
                                                           
                                                        },
                                  Comment = "comment",
                                  Email = "email",
                                  FirstName = "firstname",
                                  LastName = "lastname",
                                  PaymentMethod = PaymentMethod.CraditCard,
                                  SameAddress = false,
                                  Phone = "phone",
                                  CreditCardViewModel = new CreditCardViewModel()
                                                {
                                                    CCV = "000",
                                                    CreditCardsNumber = "12345",
                                                    CreditCardID = 1,
                                                    Month = 1,
                                                    Year = 2011
                                                }
                              };
            return details;
        }
    }
}