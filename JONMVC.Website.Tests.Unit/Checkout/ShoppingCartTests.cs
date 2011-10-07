using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Tests.Unit.Fakes;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class ShoppingCartTests:CheckoutTestsBaseClass
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]


        //TODO mock the session to get the cart
        public void FromSession_ShouldGetTheCartFromSession()
        {
            //Arrange
            var sessionItems = new SessionStateItemCollection();
            sessionItems.Dirty = false;
            sessionItems["cart"] = new ShoppingCart();

            var fakeHttpContext = FakeFactory.FakeHttpContextWithSession(sessionItems);

            var shoppingCartWrapper = new ShoppingCartWrapper(fakeHttpContext);
            
            //Act
            var shoppingCart = shoppingCartWrapper.Get();
            //Assert
            shoppingCart.Should().NotBeNull();

        }

        [Test]
        public void AddItem_ShouldAddAnItemToTheCartItemsList()
        {
            //Arrange
            var shoppingCart = new ShoppingCart();
            //Act
            shoppingCart.AddItem(StubCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,1234,CartItemType.Jewelry));
            //Assert
            shoppingCart.Items.Should().HaveCount(1);

        }

        [Test]
        public void AddItem_ShouldReturnTotalPriceOfItemsInTheCart()
        {
            //Arrange
            var shoppingCart = new ShoppingCart();
            //Act
            shoppingCart.AddItem(StubCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,1000,CartItemType.Jewelry));
            shoppingCart.AddItem(StubCartItem(1112,3000,CartItemType.Jewelry));
            //Assert
            shoppingCart.TotalPrice.Should().Be(1000 + 3000);

        }

        [Test]
        public void Remove_ShouldRemoveAnItemByCartID()
        {
            //Arrange
            var shoppingCart = new ShoppingCart();
            //Act
            shoppingCart.AddItem(StubCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, 1000, CartItemType.Jewelry));
            shoppingCart.AddItem(StubCartItem(1112, 3000, CartItemType.Jewelry));

            shoppingCart.Remove(1);
                
            //Assert
            shoppingCart.Items.Should().HaveCount(1);

        }


        [Test]
        public void Remove_ShouldRemoveAnItemByCartIDAndRecalcualtePrice()
        {
            //Arrange
            var shoppingCart = new ShoppingCart();
            //Act
            shoppingCart.AddItem(StubCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, 1000, CartItemType.Jewelry));
            shoppingCart.AddItem(StubCartItem(1112, 3000, CartItemType.Jewelry));

            shoppingCart.Remove(1);

            //Assert
            shoppingCart.TotalPrice.Should().Be(4000-3000);

        }

        [Test]
        public void Count_ShouldReturnTheRightItemCount()
        {
            //Arrange
            var shoppingCart = new ShoppingCart();
            //Act
            shoppingCart.AddItem(StubCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID, 1000, CartItemType.Jewelry));
            shoppingCart.AddItem(StubCartItem(1112, 3000, CartItemType.Jewelry));

            var totalItems = shoppingCart.Count;

            //Assert
            totalItems.Should().Be(2);

        }

        [Test]
        public void Update_ShouldUpdateAnItem()
        {
            //Arrange
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(FakeJewelCartItem(Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT, Tests.SAMPLE_JEWEL_SIZE_725,
                                                   Tests.MEDIA_TYPE_IS_NOT_IMPORANT,
                                                   Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT));


            var updatedCartItem = FakeJewelCartItem(Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT, "11.25",
                                                    Tests.MEDIA_TYPE_IS_NOT_IMPORANT,
                                                    Tests.NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT);
            //Act

            shoppingCart.Update(0, updatedCartItem);


            //Assert
            var finalItem = shoppingCart.Items[0] as JewelCartItem;
            finalItem.Size.Should().Be("11.25");

        }

        //TODO add error handling

        




    }
}