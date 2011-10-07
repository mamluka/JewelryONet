using JONMVC.Website.Models.Checkout;
using JONMVC.Website.ViewModels.Builders;

namespace JONMVC.Website.Mailers
{
    public class OrderConfirmationEmailTemplateViewModelBuilder
    {
        private readonly string orderNumber;
        private readonly CheckoutDetailsModel model;
        private readonly IShoppingCart shoppingCart;
        private readonly CartItemViewModelBuilder cartItemBuilder;

        public OrderConfirmationEmailTemplateViewModelBuilder(string orderNumber, CheckoutDetailsModel model, IShoppingCart shoppingCart, CartItemViewModelBuilder cartItemBuilder)
        {
            this.orderNumber = orderNumber;
            this.model = model;
            this.shoppingCart = shoppingCart;
            this.cartItemBuilder = cartItemBuilder;
        }

        public OrderConfirmationEmailTemplateViewModel Build()
        {
            var emailTemplate = new OrderConfirmationEmailTemplateViewModel()
                                    {
                                        Email = model.Email,
                                        OrderNumber = orderNumber,
                                        Name = model.FirstName + " " + model.LastName
                                    };

            emailTemplate.Items = cartItemBuilder.Build(shoppingCart.Items);
            emailTemplate.PaymentMethod = model.PaymentMethod;

            if (emailTemplate.PaymentMethod ==PaymentMethod.CraditCard)
            {
                emailTemplate.CCType = model.CreditCardViewModel.CreditCart;

                var length = model.CreditCardViewModel.CreditCardsNumber.Length;
                var zeroBaseIndex = length - 4;

                emailTemplate.CCLast4Digits = model.CreditCardViewModel.CreditCardsNumber.Substring(zeroBaseIndex, 4);
            
            }
            
            return emailTemplate;
        }
    }
}