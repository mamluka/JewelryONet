using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using JONMVC.Website.Controllers;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using JONMVC.Website.ViewModels.Views;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using FluentAssertions;
using MvcContrib.TestHelper;


namespace JONMVC.Website.Tests.Unit.MyAccount
{
    [TestFixture]
    public class MyAccountControllerTests : MyAccountTestsBase
    {
        [SetUp]
        public void Initialize()
        {
            
        }

        [Test]
        public void CheckMyOrderStatus_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var controller = CreateDefaultMyAccountController();
            //Act
            var result = controller.CheckMyOrderStatus();
            //Assert
            result.AssertViewRendered().WithViewData<CheckMyOrderStatusViewModel>();
        }

       



        [Test]
        public void CheckMyOrderPost_ShouldTryToValidateTheUserUsingTheService()
        {
            //Arrange
            var customerAccountService = MockRepository.GenerateStrictMock<ICustomerAccountService>();
            customerAccountService.Expect(
                x => x.ValidateCustomerUsingOrderNumber(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Repeat.Once().Return(false);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);

            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();
            //Act
            controller.CheckMyOrderStatus(email,orderID);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void CheckMyOrderPost_ShouldTryToValidateTheCustomerWithTheEmailAndOrderNumberGivenByPost()
        {
            //Arrange

            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = MockRepository.GenerateStrictMock<ICustomerAccountService>();


            customerAccountService.Expect(
                x => x.ValidateCustomerUsingOrderNumber(Arg<string>.Is.Equal(email), Arg<string>.Is.Equal(orderID))).Repeat.Once().Return(false);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);

           

            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void CheckMyOrderPost_ShouldLoginTheCustomerIfTheLoginIsValid()
        {
            //Arrange

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(true);

            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Anything, Arg<Customer>.Is.Anything));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);

            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();
            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void CheckMyOrderPost_ShouldLoginTheCustomerWithTheEmailParameter()
        {
            //Arrange
            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(true);

            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Equal(email), Arg<Customer>.Is.Anything));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);

            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void CheckMyOrderPost_ShouldGetTheCustomerDetailsFromTheServiceWhenLoggedInWithCorrectEmail()
        {
            //Arrange
            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(true);

            customerAccountService.Expect(x=> x.GetCustomerByEmail(Arg<string>.Is.Equal(email)));

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);

            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void CheckMyOrderPost_ShouldPassTheCustomerDetailsToTheAuthenticationService()
        {
            //Arrange
            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(true);

            var customer = fixture.CreateAnonymous<Customer>();

            customerAccountService.Stub(x => x.GetCustomerByEmail(Arg<string>.Is.Equal(email))).Return(customer);

            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Equal(email), Arg<Customer>.Matches(
                m => m.Email == customer.Email &&
                     m.Country == customer.Country &&
                     m.Firstname == customer.Firstname &&
                     m.Lastname == customer.Lastname &&
                     m.State == customer.State
                                                                                 )));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);

            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }


        [Test]
        public void CheckMyOrderPost_ShouldRedirectToServiceOrderStatusActionWithTheOrderNumberIfTheCredentialAreCorrect()
        {
            //Arrange
            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(true);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);


            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
           
            result.AssertActionRedirect().ToController("Checkout").ToAction("OrdersStatus").WithParameter("orderNumber",orderID);
        }

        [Test]
        public void CheckMyOrderPost_ShouldRedirectBackToCheckMyOrderIfTheUserIsNotValidatedByTheCustomerService()
        {
            //Arrange
            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(false);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);


            //Act
            var result = controller.CheckMyOrderStatus(email, orderID);
            //Assert
            result.AssertViewRendered().WithViewData<CheckMyOrderStatusViewModel>();
        }

        [Test]
        public void CheckMyOrderPost_ShouldSetHasErrorToTrueIfTheCredentintials()
        {
            //Arrange
            var email = fixture.CreateAnonymous<string>();
            var orderID = fixture.CreateAnonymous<string>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(false);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);


            //Act
            var result = controller.CheckMyOrderStatus(email, orderID) as ViewResult;
            //Assert
            var viewModel = result.Model as CheckMyOrderStatusViewModel;
            viewModel.HasError.Should().BeTrue();
        }

        [Test]
        public void Register_ShouldRenderTheCorrectViewModel()
        {
            //Arrange
            var controller = CreateDefaultMyAccountController();
            //Act
            var result = controller.Register();
            //Assert
            result.AssertViewRendered().WithViewData<RegisterCustomerViewModel>();
        }

        [Test]
        public void RegisterPost_ShouldCallCreateCustomer()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();
            var status = fixture.CreateAnonymous<MembershipCreateStatus>();

            var customerAccountService = MockRepository.GenerateStrictMock<ICustomerAccountService>();

            customerAccountService.Expect(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(status);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            var result = controller.Register(customerViewModel);
            //Assert
            customerAccountService.VerifyAllExpectations();


        }

        [Test]
        public void RegisterPost_ShouldCreateCustomerWithTheRightFields()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();
            var status = fixture.CreateAnonymous<MembershipCreateStatus>();

            var customerAccountService = MockRepository.GenerateStrictMock<ICustomerAccountService>();

            customerAccountService.Expect(
                x => x.CreateCustomer(Arg<Customer>.Matches(
                    m => m.Email == customerViewModel.Email &&
                         m.CountryID == customerViewModel.CountryID &&
                         m.Firstname == customerViewModel.Firstname &&
                         m.Lastname == customerViewModel.Lastname &&
                         m.StateID == customerViewModel.StateID
                                      ))).Return(status);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            controller.Register(customerViewModel);
            //Assert
            customerAccountService.VerifyAllExpectations();


        }

        [Test]
        public void RegisterPost_ShouldLoginTheCustomerAfterWeCreatedTheUser()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(MembershipCreateStatus.Success);

            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Anything, Arg<Customer>.Is.Anything));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);
            //Act
            var result = controller.Register(customerViewModel);
            //Assert
            authentication.VerifyAllExpectations();


        }

        [Test]
        public void RegisterPost_ShouldLoginTheCustomerAfterWeCreatedTheUserWithTheCorrectEmail()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(MembershipCreateStatus.Success);



            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Equal(customerViewModel.Email), Arg<Customer>.Is.Anything));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);
            //Act
            var result = controller.Register(customerViewModel);
            //Assert
            authentication.VerifyAllExpectations();


        }

        [Test]
        public void RegisterPost_ShouldLoginTheCustomerAfterWeCreatedTheUserWithTheCorrectCustomerDetails()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(MembershipCreateStatus.Success);



            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Anything, Arg<Customer>.Matches(
                    m => m.Email == customerViewModel.Email &&
                         m.CountryID == customerViewModel.CountryID &&
                         m.Firstname == customerViewModel.Firstname &&
                         m.Lastname == customerViewModel.Lastname &&
                         m.StateID == customerViewModel.StateID
                                      )));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);
            //Act
            var result = controller.Register(customerViewModel);
            //Assert
            authentication.VerifyAllExpectations();


        }

        [Test]
        public void RegisterPost_ShouldRedirectToThankYouForJoiningPageIfCreateProcessIsSuccessful()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(MembershipCreateStatus.Success);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            var result = controller.Register(customerViewModel);
            //Assert
            result.AssertActionRedirect().ToAction("ThankYouForJoining");
         


        }

        [Test]
        public void RegisterPost_ShouldSetHasErrorToTrueIfStatusIsNotSuccess()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(MembershipCreateStatus.ProviderError);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            

            var result = controller.Register(customerViewModel) as ViewResult;
            //Assert
            var viewModel = result.Model as RegisterCustomerViewModel;
            viewModel.HasError.Should().BeTrue();

        }

        [Test]
        public void RegisterPost_ShouldSetTheCreateMessageWithTheCorrectMemebershipStatus()
        {
            //Arrange

            var customerViewModel = fixture.CreateAnonymous<RegisterCustomerViewModel>();

            var status = fixture.CreateAnonymous<MembershipCreateStatus>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.CreateCustomer(Arg<Customer>.Is.Anything)).Return(status);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act


            var result = controller.Register(customerViewModel) as ViewResult;
            //Assert
            var viewModel = result.Model as RegisterCustomerViewModel;
            viewModel.CreateStatus.Status.Should().Be(status);

        }


        [Test]
        public void ThankYouForJoining_ShouldReturnTheCorrecViewModel()
        {
            //Arrange
            var controller = CreateDefaultMyAccountController();
            //Act
            var result = controller.ThankYouForJoining();
            //Assert
            result.AssertViewRendered().WithViewData<EmptyViewModel>();
        }

        [Test]
        public void SignIn_ShouldReturnTheRightViewModel()
        {
            //Arrange
            var controller = CreateDefaultMyAccountController();
            //Act
            var result = controller.Signin();
            //Assert
            result.AssertViewRendered().WithViewData<SigninViewModel>();
        }

        [Test]
        public void SigninPost_ShoulValidateTheCustomerLoginUsingEmailAndPassword()
        {
            //Arrange
            var model = fixture.CreateAnonymous<SigninViewModel>();
          

            var customerAccountService = MockRepository.GenerateMock<ICustomerAccountService>();

            customerAccountService.Expect(
                x => x.ValidateCustomer(Arg<string>.Is.Equal(model.Email),Arg<string>.Is.Equal(model.Password))).Return(true);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            controller.Signin(model);
            //Assert
            
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void SigninPost_ShoulSignInTheCustomerToTheSite()
        {
            //Arrange
            var model = fixture.CreateAnonymous<SigninViewModel>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForRegularPasswordBasedValidationReturns(true);

            var authentication = MockRepository.GenerateStrictMock<IAuthentication>();
            authentication.Expect(x => x.Signin(Arg<string>.Is.Equal(model.Email), Arg<Customer>.Is.Anything));

            var controller = CreateDefaultMyAccountControllerWithCustomAuthAndAccount(authentication, customerAccountService);
            //Act
            controller.Signin(model);
            //Assert

            authentication.VerifyAllExpectations();
        }

        [Test]
        public void SigninPost_ShouldGetTheUserDetailsAfterTheUserWasCheckedandIsValid()
        {
            //Arrange
            var model = fixture.CreateAnonymous<SigninViewModel>();

            var customer = fixture.CreateAnonymous<Customer>();

            var customerAccountService =
                CustomerAccountServiceThatWhenAskedForRegularPasswordBasedValidationReturns(true);

            customerAccountService.Expect(
                x => x.GetCustomerByEmail(Arg<string>.Is.Equal(model.Email))).Return(customer);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            controller.Signin(model);
            //Assert

            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void SigninPost_ShouldReturnHasErrorAsTrueIfThereIsNoValidationSoItsIsAnError()
        {
            //Arrange
            var model = fixture.CreateAnonymous<SigninViewModel>();

            var customerAccountService =
                CustomerAccountServiceThatWhenAskedForRegularPasswordBasedValidationReturns(false);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            var result = controller.Signin(model) as ViewResult;
            //Assert
            var resultmodel = result.Model as SigninViewModel;

            resultmodel.HasError.Should().BeTrue();

        }

        [Test]
        public void RecoverPassword_ShouldRenderWithTheRightViewModel()
        {
            //Arrange
            var controller = CreateDefaultMyAccountController();
            //Act
            var viewModel = controller.RecoverPassword();
            //Assert
            viewModel.AssertViewRendered().WithViewData<RecoverPasswordViewModel>();
        }

        [Test]
        public void RecoverPasswordPost_ShouldRecoverThePasswordUsingtTheCustomerService()
        {
            //Arrange
            var model = fixture.CreateAnonymous<RecoverPasswordViewModel>();

            var customerAccountService = MockRepository.GenerateMock<ICustomerAccountService>();
            customerAccountService.Expect(x => x.RecoverPassword(Arg<string>.Is.Equal(model.Email))).Repeat.Once().Return(Tests.SAMPLE_PASSWORD);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            controller.RecoverPassword(model);
            //Assert
            customerAccountService.VerifyAllExpectations();
        }

        [Test]
        public void RecoverPasswordPost_ShouldSendTheEmailUsingTheMailer()
        {
            //Arrange
            var model = fixture.CreateAnonymous<RecoverPasswordViewModel>();

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            customerAccountService.Stub(x => x.RecoverPassword(Arg<string>.Is.Anything)).Repeat.Once().Return(Tests.SAMPLE_PASSWORD);

            var mailer = MockRepository.GenerateMock<IUserMailer>();
            mailer.Expect(
                x => x.RecoverPassword(Arg<string>.Is.Equal(model.Email), Arg<string>.Is.Equal(Tests.SAMPLE_PASSWORD))).
                Repeat.Once();
           
            var controller = CreateDefaultMyAccountControllerWithCustomerUserMailerAndCustomCustomerAccountService(mailer,customerAccountService);
            //Act
            controller.RecoverPassword(model);
            //Assert
            mailer.VerifyAllExpectations();
        }

        [Test]
        public void RecoverPasswordPost_ShouldSetTheHasErrorToFalseIfPasswordIsReturned()
        {
            //Arrange
            var model = fixture.CreateAnonymous<RecoverPasswordViewModel>();

            var mailer = MockRepository.GenerateMock<IUserMailer>();
            mailer.Expect(
                x => x.RecoverPassword(Arg<string>.Is.Equal(model.Email), Arg<string>.Is.Equal(Tests.SAMPLE_PASSWORD))).
                Repeat.Once();

            var controller = CreateDefaultMyAccountControllerWithCustomerUserMailer(mailer);
            //Act
            var viewModel = controller.RecoverPassword(model) as ViewResult;
            //Assert
            var returnmodel = viewModel.Model as RecoverPasswordViewModel;
            returnmodel.HasError.Should().BeFalse();
        }

        [Test]
        public void RecoverPasswordPost_ShouldSetTheHasErrorToTrueIfErrorThrown()
        {
            //Arrange
            var model = fixture.CreateAnonymous<RecoverPasswordViewModel>();

            var mailer = MockRepository.GenerateMock<IUserMailer>();
            mailer.Expect(
                x => x.RecoverPassword(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).
                Repeat.Once().Throw(new Exception());

            var controller = CreateDefaultMyAccountControllerWithCustomerUserMailer(mailer);
            //Act
            var viewModel = controller.RecoverPassword(model) as ViewResult;
            //Assert
            var returnmodel = viewModel.Model as RecoverPasswordViewModel;
            returnmodel.HasError.Should().BeTrue();
        }

        [Test]
        public void Login_ShouldRedirectToTheHomePageAfterASuccessfulLogin()
        {
            //Arrange
            var model = fixture.CreateAnonymous<SigninViewModel>();

            var customerAccountService = CustomerAccountServiceThatWhenAskedForRegularPasswordBasedValidationReturns(true);

            var controller = CreateDefaultMyAccountControllerWithCustomCustomerAccountService(customerAccountService);
            //Act
            var result = controller.Signin(model);
            //Assert
            result.AssertActionRedirect().ToController("Home").ToAction("Index");
        }

        [Test]
        public void Index_ShouldRenderTheRightViewModel()
        {
            //Arrange
            var authentication = MockRepository.GenerateStub<IAuthentication>();
            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            var userMailer = MockRepository.GenerateStub<IUserMailer>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            var customer = fixture.CreateAnonymous<ExtendedCustomer>();
            var customerData = fixture.CreateAnonymous<Customer>();

            orderRepository.Stub(x => x.GetOrdersByCustomerEmail(Arg<string>.Is.Anything));

            authentication.Stub(x => x.CustomerData).Return(customerData);

            customerAccountService.Stub(x => x.GetExtendedCustomerByEmail(Arg<string>.Is.Anything)).Return(customer);

            var controller = new MyAccountController(authentication, customerAccountService, userMailer, mapper, orderRepository);
            //Act
            var viewModel = controller.Index();
            //Assert
            viewModel.AssertViewRendered().WithViewData<MyAccountViewModel>();
        }

        private ICustomerAccountService CustomerAccountServiceThatWhenAskedForRegularPasswordBasedValidationReturns(bool returns)
        {
            var customerAccountService = MockRepository.GenerateMock<ICustomerAccountService>();

            customerAccountService.Stub(
                x => x.ValidateCustomer(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(returns);
            return customerAccountService;
        }


        private  ICustomerAccountService CustomerAccountServiceThatWhenAskedForValidationUsingOrderNumberReturns(bool returns)
        {
            var customerAccountService = MockRepository.GenerateMock<ICustomerAccountService>();
            customerAccountService.Stub(
                x => x.ValidateCustomerUsingOrderNumber(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(returns);
            return customerAccountService;
        }

        private  MyAccountController CreateDefaultMyAccountController()
        {
            var authentication = MockRepository.GenerateStub<IAuthentication>();
            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            var userMailer = MockRepository.GenerateStub<IUserMailer>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            var controller = new MyAccountController(authentication, customerAccountService, userMailer, mapper, orderRepository);
            return controller;
        }

        private  MyAccountController CreateDefaultMyAccountControllerWithCustomAuthentication(
            IAuthentication authentication)
        {

            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();
            var userMailer = MockRepository.GenerateStub<IUserMailer>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            var controller = new MyAccountController(authentication, customerAccountService, userMailer,mapper, orderRepository);
            return controller;
        }

        private  MyAccountController CreateDefaultMyAccountControllerWithCustomCustomerAccountService(
            ICustomerAccountService customerAccountService)
        {

            var authentication = MockRepository.GenerateStub<IAuthentication>();
            var userMailer = MockRepository.GenerateStub<IUserMailer>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            var controller = new MyAccountController(authentication, customerAccountService, userMailer,mapper, orderRepository);
            return controller;
        }

        private MyAccountController CreateDefaultMyAccountControllerWithCustomAuthAndAccount(IAuthentication authentication,
            ICustomerAccountService customerAccountService)
        {
            var userMailer = MockRepository.GenerateStub<IUserMailer>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();
            var controller = new MyAccountController(authentication, customerAccountService, userMailer, mapper, orderRepository);
            return controller;
        }

        private MyAccountController CreateDefaultMyAccountControllerWithCustomerUserMailer(IUserMailer userMailer)
        {
            var authentication = MockRepository.GenerateStub<IAuthentication>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();
            var customerAccountService = MockRepository.GenerateStub<ICustomerAccountService>();

            var controller = new MyAccountController(authentication, customerAccountService, userMailer, mapper, orderRepository);
            return controller;
        }

        private MyAccountController CreateDefaultMyAccountControllerWithCustomerUserMailerAndCustomCustomerAccountService(IUserMailer userMailer,ICustomerAccountService customerAccountService)
        {
            var authentication = MockRepository.GenerateStub<IAuthentication>();
            var orderRepository = MockRepository.GenerateStub<IOrderRepository>();

            var controller = new MyAccountController(authentication, customerAccountService, userMailer, mapper, orderRepository);
            return controller;
        }

    }
}