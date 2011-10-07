using System.Data.Objects.DataClasses;
using System.Text;
using JONMVC.Website.Controllers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;


namespace JONMVC.Website.Tests.Unit.MyAccount
{
    [TestFixture]
    public class DataBaseCustomerAccountServiceTests:MyAccountTestsBase
    {
        private Fixture fixture;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Customize<usr_CUSTOMERS>(
                customer =>
                customer.With(x => x.password, Tests.SAMPLE_PASSWORD).With(x => x.email, Tests.SAMPLE_EMAIL_ADDRESS));
        }

        [Test]
        public void ValidateCustomer_ShouldReturnTrueIfTheCredentialsAreCorrect()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();  
            //Act
            var result = customerAccountService.ValidateCustomer(Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD);
            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void ValidateCustomer_ShouldReturnFalseOnIncorrectPassword()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomer(Tests.SAMPLE_EMAIL_ADDRESS, Tests.SAMPLE_PASSWORD+"BadPassword");
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidateCustomer_ShouldReturnFalseOnMissingEmail()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomer(Tests.SAMPLE_EMAIL_ADDRESS + "BadEmail", Tests.SAMPLE_PASSWORD);
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidateCustomer_ShouldReturnFalseIfWeUserCorrectEmailAndCorrectPasswordButForDifferentUsers()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomer(Tests.SAMPLE_EMAIL_ADDRESS + "2", Tests.SAMPLE_PASSWORD+"3");
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidateCustomer_ShouldReturnFalseIfWeUseEmptyPassWord()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomer(Tests.SAMPLE_EMAIL_ADDRESS , "");
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidateCustomer_ShouldReturnFalseIfEmailIsEmpty()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomer("", Tests.SAMPLE_PASSWORD);
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidateCustomerUsingOrderNumber_ShouldValidateUsingAnEmailAndAnOrderNumber()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomerUsingOrderNumber(Tests.SAMPLE_EMAIL_ADDRESS,Tests.FAKE_ORDERNUMBER.ToString());
            //Assert
            result.Should().BeTrue();

        }

        [Test]
        public void ValidateCustomerUsingOrderNumber_ShouldReturnFalseIfTheOrderNumberDoesntMetchTheEmail()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomerUsingOrderNumber(Tests.SAMPLE_EMAIL_ADDRESS, Tests.FAKE_ORDERNUMBER.ToString() + "123");
            //Assert
            result.Should().BeFalse();

        }

        [Test]
        public void ValidateCustomerUsingOrderNumber_ShouldReturnFalseIfTheEmailDoesntExsists()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomerUsingOrderNumber(Tests.SAMPLE_EMAIL_ADDRESS+"Doesn't Exists", Tests.FAKE_ORDERNUMBER.ToString());
            //Assert
            result.Should().BeFalse();

        }

        [Test]
        public void ValidateCustomerUsingOrderNumber_ShouldReturnFalseIfTheOrderNumberDoesntExists()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var result = customerAccountService.ValidateCustomerUsingOrderNumber(Tests.SAMPLE_EMAIL_ADDRESS, Tests.FAKE_ORDERNUMBER.ToString()+"123");
            //Assert
            result.Should().BeFalse();

        }

        [Test]
        public void GetCustomerByEmail_ShouldReturnAUserWithTheCorrectEmail()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var customer = customerAccountService.GetCustomerByEmail(Tests.SAMPLE_EMAIL_ADDRESS);
            //Assert
            customer.Email.Should().Be(Tests.SAMPLE_EMAIL_ADDRESS);
        }

        [Test]
        public void RecoverPassword_ShouldReturnAPasswordIfEmailIsOK()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var lostPassword = customerAccountService.RecoverPassword(Tests.SAMPLE_EMAIL_ADDRESS);
            //Assert
            lostPassword.Should().Be(Tests.SAMPLE_PASSWORD);
        }

        [Test]
        [ExpectedException]
        public void RecoverPassword_ShouldThrowExceptionWhenEmaisIsNotFound()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            customerAccountService.RecoverPassword(Tests.SAMPLE_EMAIL_ADDRESS+"NotFound");
            //Assert
        }


        [Test]
        [Ignore("Ignored because can't mock reference to state/country table in EF")]
        public void GetExtendedCustomerByEmail_ShouldReturnAnExtendedCustomer()
        {
            //Arrange
            var customerAccountService = CreateDefaultFakeDataBaseCustomerAccountService();
            //Act
            var extendedCustomer =  customerAccountService.GetExtendedCustomerByEmail(Tests.SAMPLE_EMAIL_ADDRESS);
            //Assert
            extendedCustomer.ShippingAddress.Should().NotBeNull();
            extendedCustomer.BillingAddress.Should().NotBeNull();

        }
        //TODO find a way to test this


        private FakeDataBaseCustomerAccountService CreateDefaultFakeDataBaseCustomerAccountService()
        {
            var customerAccountService = new FakeDataBaseCustomerAccountService(mapper);
            return customerAccountService;
        }
    }
}