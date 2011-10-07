using System.Collections.Generic;
using System.Data;
using System.Text;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Builders;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.Checkout
{
    [TestFixture]
    public class OrderStatusViewModelBuilderTests:CheckoutTestsBaseClass
    {
        /// <summary>
        /// Prepares mock repository
        /// </summary>
       

        [Test]
        public void Build_ShouldCallTheGetOrderByOrderNumberMethod()
        {
            //Arrange

            var order = fixture.CreateAnonymous<Order>();

            var orderRepository = MockRepository.GenerateMock<IOrderRepository>();

            orderRepository.Expect(x => x.GetOrderByOrderNumber(Arg<int>.Is.Anything)).Repeat.Once().Return(order);

            var builder = new OrderStatusViewModelBuilder(mapper,orderRepository);
            //Act
            var viewModel = builder.Build(Tests.FAKE_ORDERNUMBER);
            //Assert
            orderRepository.VerifyAllExpectations();
        }

        [Test]
        public void Build_ShouldUseTheRightOrderNumberToGetTheOrderFromTheDatabase()
        {
            //Arrange

            var order = fixture.Build<Order>().With(x=> x.OrderNumber,Tests.FAKE_ORDERNUMBER).CreateAnonymous();

            var orderRepository = MockRepository.GenerateMock<IOrderRepository>();

            orderRepository.Expect(x => x.GetOrderByOrderNumber(Arg<int>.Is.Equal(order.OrderNumber))).Repeat.Once().Return(order);

            var builder = new OrderStatusViewModelBuilder(mapper, orderRepository);
            //Act
            var viewModel = builder.Build(order.OrderNumber);
            //Assert
            orderRepository.VerifyAllExpectations();
        }

        [Test]
        public void Build_ShouldMapTheBillingAndShippingAddressCorrectly()
        {
            //Arrange

            var order = fixture.Build<Order>().With(x => x.OrderNumber, Tests.FAKE_ORDERNUMBER).CreateAnonymous();

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            orderRepository.Stub(x => x.GetOrderByOrderNumber(Arg<int>.Is.Equal(order.OrderNumber))).Repeat.Once().Return(order);

            var builder = new OrderStatusViewModelBuilder(mapper, orderRepository);
            //Act
            var viewModel = builder.Build(order.OrderNumber);
            //Assert
            viewModel.BillingAddress.Address1.Should().Be(order.BillingAddress.Address1);
            viewModel.BillingAddress.City.Should().Be(order.BillingAddress.City);
            viewModel.BillingAddress.Country.Should().Be(order.BillingAddress.Country);
            viewModel.BillingAddress.CountryID.Should().Be(order.BillingAddress.CountryID);
            viewModel.BillingAddress.FirstName.Should().Be(order.BillingAddress.FirstName);
            viewModel.BillingAddress.LastName.Should().Be(order.BillingAddress.LastName);
            viewModel.BillingAddress.Phone.Should().Be(order.BillingAddress.Phone);
            viewModel.BillingAddress.State.Should().Be(order.BillingAddress.State);
            viewModel.BillingAddress.StateID.Should().Be(order.BillingAddress.StateID);
            viewModel.BillingAddress.ZipCode.Should().Be(order.BillingAddress.ZipCode);

            viewModel.ShippingAddress.Address1.Should().Be(order.ShippingAddress.Address1);
            viewModel.ShippingAddress.City.Should().Be(order.ShippingAddress.City);
            viewModel.ShippingAddress.Country.Should().Be(order.ShippingAddress.Country);
            viewModel.ShippingAddress.CountryID.Should().Be(order.ShippingAddress.CountryID);
            viewModel.ShippingAddress.FirstName.Should().Be(order.ShippingAddress.FirstName);
            viewModel.ShippingAddress.LastName.Should().Be(order.ShippingAddress.LastName);
            viewModel.ShippingAddress.Phone.Should().Be(order.ShippingAddress.Phone);
            viewModel.ShippingAddress.State.Should().Be(order.ShippingAddress.State);
            viewModel.ShippingAddress.StateID.Should().Be(order.ShippingAddress.StateID);
            viewModel.ShippingAddress.ZipCode.Should().Be(order.ShippingAddress.ZipCode);
        }

        [Test]
        public void Build_ShouldMapOtherFieldsCorrectly()
        {
            //Arrange

            var order = fixture.Build<Order>().With(x => x.OrderNumber, Tests.FAKE_ORDERNUMBER).With(x=> x.PaymentID,1).CreateAnonymous();

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            orderRepository.Stub(x => x.GetOrderByOrderNumber(Arg<int>.Is.Equal(order.OrderNumber))).Repeat.Once().Return(order);

            var builder = new OrderStatusViewModelBuilder(mapper, orderRepository);
            //Act
            var viewModel = builder.Build(order.OrderNumber);
            //Assert
            viewModel.OrderNumber.Should().Be(order.OrderNumber.ToString());
            viewModel.TotalPrice.Should().Be(Tests.AsMoney(order.TotalPrice));
            viewModel.SpecialInstructions.Should().Be(order.Comment);
            viewModel.Status.Should().Be(CustomAttributes.GetDescription(order.Status));

        }

        [Test]
        public void Build_ShouldMapThePaymentMethodCorrectlyWhenSetToCreditCard()
        {
            //Arrange

            var order = fixture.Build<Order>().With(x => x.OrderNumber, Tests.FAKE_ORDERNUMBER).With(x => x.PaymentID, 1).CreateAnonymous();

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            orderRepository.Stub(x => x.GetOrderByOrderNumber(Arg<int>.Is.Equal(order.OrderNumber))).Repeat.Once().Return(order);

            var builder = new OrderStatusViewModelBuilder(mapper, orderRepository);
            //Act
            var viewModel = builder.Build(order.OrderNumber);
            //Assert
            viewModel.PaymentMethod.Should().Be("Credit card");

        }

        [Test]
        public void Build_ShouldMapTheCardItems()
        {
            //Arrange

            var order =
                fixture.Build<Order>()
                    .With(x => x.OrderNumber, Tests.FAKE_ORDERNUMBER)
                    .With(x => x.PaymentID, 1)
                    .With(x => x.Items, new List<ICartItem>()
                                            {
                                                FakeJewelCartItem(Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,Tests.SAMPLE_JEWEL_SIZE_725,JewelMediaType.WhiteGold, 8000),
                                                FakeDiamondCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,5000),
                                                FakeCustomJewelCartItem(Tests.FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID,Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,Tests.SAMPLE_JEWEL_SIZE_725, JewelMediaType.WhiteGold,10000)
                                            })
                    .CreateAnonymous();

            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            orderRepository.Stub(x => x.GetOrderByOrderNumber(Arg<int>.Is.Equal(order.OrderNumber))).Repeat.Once().
                Return(
                    order);

            var builder = new OrderStatusViewModelBuilder(mapper, orderRepository);
            //Act
            var viewModel = builder.Build(order.OrderNumber);
            //Assert
            viewModel.Items.Should().HaveCount(3);

        }
    }
}