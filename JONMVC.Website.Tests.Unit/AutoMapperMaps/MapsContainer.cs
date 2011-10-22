using System;
using System.Text;
using AutoMapper;
using JONMVC.Website.Controllers;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models;
using JONMVC.Website.Models.AutoMapperMaps;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Tests.Unit.Diamonds;
using JONMVC.Website.Tests.Unit.Jewelry;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.ViewModels.Builders;
using JONMVC.Website.ViewModels.Json.Builders;
using JONMVC.Website.ViewModels.Json.Views;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Tests.Unit.AutoMapperMaps
{
    public static class MapsContainer
    {
        public static void CreateAutomapperMaps()
        {
            Mapper.CreateMap<DiamondSearchParametersGivenByJson, DiamondSearchParameters>()
             .ForMember(s => s.Shape,
                        opt =>
                        opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("shape")).FromMember(s => s.Shape))
             .ForMember(s => s.Color,
                        opt =>
                        opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("color")).FromMember(s => s.Color))
             .ForMember(s => s.Clarity,
                        opt =>
                        opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("clarity")).FromMember(s => s.Clarity))
             .ForMember(s => s.Cut,
                        opt =>
                        opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("cut")).FromMember(s => s.Cut))
             .ForMember(s => s.Report,
                        opt =>
                        opt.ResolveUsing<FromJsonToDataBase>().ConstructedBy(() => new FromJsonToDataBase("report")).FromMember(s => s.Report))
             .ForMember(s => s.ItemsPerPage, opt => opt.MapFrom(x => x.rows))
             .ForMember(s => s.OrderBy, opt => opt.ResolveUsing<DynamicSortFromStringResolver>())
             ;

            Mapper.CreateMap<v_jd_diamonds, Diamond>()
                .ForMember(dto => dto.DiamondID, opt => opt.MapFrom(s => s.diamondid))
                .ForMember(dto => dto.ReportURL, opt => opt.MapFrom(s => s.reportimg))
                .ForMember(dto => dto.Fluorescence, opt => opt.MapFrom(s => s.flourescence))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(s => s.totalprice))
                .ForMember(dto => dto.Girdle, opt => opt.MapFrom(s => s.grindle))
                .ForMember(dto => dto.Symmetry, opt => opt.MapFrom(s => s.symmetrical))
                .ForMember(dto => dto.ReportNumber, opt => opt.MapFrom(s => s.report_number))
                .ForMember(dto => dto.Cut, opt => opt.MapFrom(s => s.cut))
                .ForMember(dto => dto.Description, opt => opt.ResolveUsing<DiamondDesriptionResolver>())
                ;


            Mapper.CreateMap<Diamond, DiamondViewModel>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dto => dto.Depth, opt => opt.AddFormatter<PrecentFormatter>())
                .ForMember(dto => dto.DiamondHiResPicture, opt => opt.ResolveUsing(new DiamondMediaResolver(DiamondMediaType.HighResolutionPicture, new FakeSettingManager())))
                .ForMember(dto => dto.Dimensions, opt => opt.ResolveUsing(new DiamondDimensionsResolver()))
                .ForMember(dto => dto.ItemCode, opt => opt.MapFrom(x => x.DiamondID))
                .ForMember(dto => dto.MainDiamondPicture, opt => opt.ResolveUsing(new DiamondPrettyMediaResolver(DiamondMediaType.Picture, new FakeSettingManager())))
                .ForMember(dto => dto.Price, opt => opt.AddFormatter<PriceFormatter>())
                .ForMember(dto => dto.Table, opt => opt.AddFormatter<PrecentFormatter>())
                .ForMember(dto => dto.Weight, opt => opt.AddFormatter<WeightFormatter>())
                .ForMember(dto => dto.Fluorescence, opt => opt.MapFrom(x => x.Fluorescence))
                .ForMember(dto => dto.TabsForJewelDesignNavigation, opt => opt.Ignore())
                .ForMember(dto => dto.JewelPersistence, opt => opt.Ignore())
                .ForMember(dto=>dto.DiamondHelp,opt=>opt.Ignore())
                .ForMember(dto=>dto.PageTitle,opt=>opt.ResolveUsing<DiamondPageTitleResolver>())
                .ForMember(dto=> dto.PathBarItems,opt=>opt.Ignore())
                ;

            Mapper.CreateMap<MergeDiamondAndJewel, EndViewModel>()
                      .ForMember(dto => dto.TabsForJewelDesignNavigation, opt => opt.Ignore())
                      .ForMember(dto => dto.SettingPrice, opt => opt.AddFormatter<PriceFormatter>())
                      .ForMember(dto => dto.DiamondPrice, opt => opt.AddFormatter<PriceFormatter>())
                      .ForMember(dto => dto.Clarity, opt => opt.MapFrom(x => x.First.Clarity))
                      .ForMember(dto => dto.Color, opt => opt.MapFrom(x => x.First.Color))
                      .ForMember(dto => dto.DiamondID, opt => opt.MapFrom(x => x.First.DiamondID))
                      .ForMember(dto => dto.DiamondPrice, opt => opt.MapFrom(x => x.First.Price))
                      .ForMember(dto => dto.DiamondWeight, opt => opt.MapFrom(x => x.First.Weight))
                      .ForMember(dto => dto.Width, opt => opt.MapFrom(x => x.Second.Width))
                      .ForMember(dto => dto.Width, opt => opt.AddFormatter<DoubleTypeZeroToNAFormatter>())
                      .ForMember(dto => dto.ItemNumber, opt => opt.MapFrom(x => x.Second.ItemNumber))
                      .ForMember(dto => dto.Metal, opt => opt.MapFrom(x => x.Second.MetalFullName()))
                      .ForMember(dto => dto.SettingPrice, opt => opt.MapFrom(x => x.Second.Price))
                      .ForMember(dto => dto.Shape, opt => opt.MapFrom(x => x.First.Shape))
                      .ForMember(dto => dto.TotalPrice, opt => opt.ResolveUsing<TotalPriceResolver>())
                      .ForMember(dto => dto.Weight, opt => opt.MapFrom(x => x.Second.Weight))
                      .ForMember(dto => dto.DiamondIcon, opt =>
                                                         opt.ResolveUsing(
                                                             new DiamondMediaResolver(DiamondMediaType.Icon,
                                                                                            new FakeSettingManager())).FromMember(x => x.First))
                      .ForMember(dto => dto.SettingIcon, opt => opt.MapFrom(x => x.Second.Media.IconURLForWebDisplay))
                      .ForMember(dto => dto.Size, opt => opt.Ignore())
                      .ForMember(dto => dto.JewelPersistence, opt => opt.Ignore())
                      .ForMember(dto => dto.PageTitle, opt => opt.Ignore())
                       .ForMember(dto => dto.PathBarItems, opt => opt.Ignore())
                      ;

            Mapper.CreateMap<Jewel, JewelCartItemViewModel>()

                .ForMember(dto => dto.Icon, opt => opt.MapFrom(x => x.Media.IconURLForWebDisplay))
                .ForMember(dto => dto.CartID, opt => opt.Ignore())
                .ForMember(dto => dto.Desciption, opt => opt.MapFrom(x => x.Title))
                .ForMember(dto => dto.JewelSize, opt => opt.Ignore())
                .ForMember(dto => dto.Price, opt => opt.AddFormatter<PriceFormatter>())
                .ForMember(dto => dto.NoActionLinkDispalyOnly, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Diamond, DiamondCartItemViewModel>()
                .ForMember(dto => dto.Icon, opt =>
                                                         opt.ResolveUsing(
                                                             new DiamondMediaResolver(DiamondMediaType.Icon,
                                                                                            new FakeSettingManager())))
                .ForMember(dto => dto.CartID, opt => opt.Ignore())
                .ForMember(dto => dto.Desciption, opt => opt.MapFrom(x => x.Description))
                .ForMember(dto => dto.Price, opt => opt.AddFormatter<PriceFormatter>())
                .ForMember(dto => dto.ID, opt => opt.MapFrom(x => x.DiamondID))
                .ForMember(dto => dto.NoActionLinkDispalyOnly, opt => opt.Ignore())
                ;

            Mapper.CreateMap<MergeDiamondAndJewel, CustomJewelCartItemViewModel>()
                     .ForMember(dto => dto.CartID, opt => opt.Ignore())
                     .ForMember(dto => dto.SettingPrice, opt => opt.AddFormatter<PriceFormatter>())
                     .ForMember(dto => dto.DiamondPrice, opt => opt.AddFormatter<PriceFormatter>())
                     .ForMember(dto => dto.DiamondID, opt => opt.MapFrom(x => x.First.DiamondID))
                     .ForMember(dto => dto.DiamondPrice, opt => opt.MapFrom(x => x.First.Price))
                     .ForMember(dto => dto.Itemnumber, opt => opt.MapFrom(x => x.Second.ItemNumber))
                     .ForMember(dto => dto.ID, opt => opt.MapFrom(x => x.Second.ID))
                     .ForMember(dto => dto.SettingPrice, opt => opt.MapFrom(x => x.Second.Price))
                     .ForMember(dto => dto.Price, opt => opt.ResolveUsing<TotalPriceResolver>())
                     .ForMember(dto => dto.Icon, opt => opt.MapFrom(x => x.Second.Media.IconURLForWebDisplay))
                     .ForMember(dto => dto.SettingDesciption, opt => opt.MapFrom(x => x.Second.Title))
                     .ForMember(dto => dto.DiamondDesciption, opt => opt.MapFrom(x => x.First.Description))
                     .ForMember(dto => dto.Size, opt => opt.Ignore())
                     .ForMember(dto => dto.NoActionLinkDispalyOnly, opt => opt.Ignore())

                     ;

            Mapper.CreateMap<CheckoutDetailsModel, BillingViewModel>()
                .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dto => dto.PaymentMethod, opt => opt.MapFrom(x => x.PaymentMethod))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(x => x.Phone))
                .ForMember(dto => dto.CreditCardViewModel, opt => opt.MapFrom(x => x.CreditCardViewModel))
                ;

            Mapper.CreateMap<CheckoutDetailsModel, ReviewOrderViewModel>()
                .ForMember(dto => dto.TotalPrice, opt => opt.Ignore())
                .ForMember(dto => dto.CartItems, opt => opt.Ignore())
          
                

                ;

            Mapper.CreateMap<CheckoutDetailsModel, Order>()
                .ForMember(dto => dto.BillingAddress, opt => opt.ResolveUsing<AddressResolver>().FromMember(x => x.BillingAddress))
                .ForMember(dto => dto.ShippingAddress, opt => opt.ResolveUsing<AddressResolver>().FromMember(x => x.ShippingAddress))
                .ForMember(dto => dto.PaymentID, opt => opt.ResolveUsing<EnumToIntResolver>().FromMember(x => x.PaymentMethod))
                .ForMember(dto => dto.TotalPrice, opt => opt.Ignore())
                .ForMember(dto => dto.Items, opt => opt.Ignore())
                .ForMember(dto => dto.CreditCard, opt => opt.ResolveUsing<CreditCardResolver>().FromMember(x => x.CreditCardViewModel))
                .ForMember(dto => dto.OrderNumber, opt => opt.Ignore())
                .ForMember(dto => dto.TrackingNumber, opt => opt.Ignore())
                .ForMember(dto => dto.PaymentDate, opt => opt.Ignore())
                .ForMember(dto => dto.Status, opt => opt.Ignore())
                .ForMember(dto => dto.CreateDate, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Jewel, BestOfferEmailTemplateViewModel>()
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.TruePrice, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.TruePrice, opt => opt.AddFormatter<PriceFormatter>())
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.OfferDate, opt => opt.Ignore())
                .ForMember(x => x.OfferNumber, opt => opt.Ignore())
                .ForMember(x => x.OfferPrice, opt => opt.Ignore())


                ;

            Mapper.CreateMap<Jewel, WishListItemViewModel>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(x => x.Title))
                .ForMember(dto => dto.Icon, opt => opt.MapFrom(x => x.Media.IconURLForWebDisplay))
                .ForMember(dto => dto.Price, opt => opt.AddFormatter<PriceFormatter>())
                ;

            Mapper.CreateMap<RegisterCustomerViewModel, Customer>()
                    .ForMember(dto=> dto.Country,opt=>opt.Ignore())
                    .ForMember(dto => dto.State, opt => opt.Ignore())
                ;
            Mapper.CreateMap<usr_CUSTOMERS, Customer>()
                    .ForMember(dto => dto.Country, opt => opt.MapFrom(x=> x.sys_COUNTRYReference.Value.LANG1_LONGDESCR))
                    .ForMember(dto => dto.State, opt => opt.MapFrom(x => x.sys_STATEReference.Value.LANG1_LONGDESCR))
                    .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.email))
                    .ForMember(dto => dto.Firstname, opt => opt.MapFrom(x => x.firstname))
                    .ForMember(dto => dto.Lastname, opt => opt.MapFrom(x => x.lastname))
                    .ForMember(dto => dto.CountryID, opt => opt.MapFrom(x => x.country1_id))
                    .ForMember(dto => dto.StateID, opt => opt.MapFrom(x => x.state1_id))
                ;

            Mapper.CreateMap<usr_CUSTOMERS, ExtendedCustomer>()
                   .ForMember(dto => dto.Country, opt => opt.MapFrom(x => x.sys_COUNTRYReference.Value.LANG1_LONGDESCR))
                   .ForMember(dto => dto.State, opt => opt.MapFrom(x => x.sys_STATEReference.Value.LANG1_LONGDESCR))
                   .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.email))
                   .ForMember(dto => dto.Firstname, opt => opt.MapFrom(x => x.firstname))
                   .ForMember(dto => dto.Lastname, opt => opt.MapFrom(x => x.lastname))
                   .ForMember(dto => dto.CountryID, opt => opt.MapFrom(x => x.country1_id))
                   .ForMember(dto => dto.StateID, opt => opt.MapFrom(x => x.state1_id))
                   .ForMember(dto => dto.BillingAddress, opt => opt.ResolveUsing<CustomerBillingAddressDBResolver>())
                   .ForMember(dto => dto.ShippingAddress, opt => opt.ResolveUsing<CustomerShippingAddressDBResolver>())
                   .ForMember(dto => dto.MemeberSince, opt => opt.MapFrom(x => x.create_date))
               ;

            Mapper.CreateMap<acc_ORDERS, Order>()
                .ForMember(dto => dto.BillingAddress,opt=> opt.ResolveUsing<OrderBillingAddressDBResolver>())
                .ForMember(dto => dto.ShippingAddress, opt => opt.ResolveUsing<OrderShippingAddressDBResolver>())
                .ForMember(dto => dto.Comment, opt => opt.MapFrom(x => x.Customer_Notes))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.usr_CUSTOMERSReference.Value.email))
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(x => x.usr_CUSTOMERSReference.Value.firstname))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(x => x.usr_CUSTOMERSReference.Value.lastname))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(x => x.usr_CUSTOMERSReference.Value.phone1))
                .ForMember(dto => dto.PaymentID, opt => opt.MapFrom(x => x.payment_id))
                .ForMember(dto => dto.TotalPrice, opt => opt.MapFrom(x => x.amnt_grandtotal))
                .ForMember(dto => dto.CreditCard, opt => opt.ResolveUsing<OrderCraditCartResolver>())
                .ForMember(dto => dto.Items, opt => opt.ResolveUsing(new CartItemsResolver(new FakeJewelRepository(new FakeSettingManager()),new FakeDiamondRepository(Mapper.Engine))))
                .ForMember(dto=> dto.OrderNumber,opt=> opt.MapFrom(x=> x.OrderNumber))
                .ForMember(dto => dto.PaymentDate, opt => opt.MapFrom(x => x.sts4_order_confirmed_date))
                .ForMember(dto => dto.PaymentDate, opt => opt.AddFormatter<ShortDateFormatter>())
                .ForMember(dto => dto.TrackingNumber, opt => opt.MapFrom(x => x.shipping_tracking_no))
                .ForMember(dto => dto.CreateDate, opt => opt.MapFrom(x=> x.sts1_new_order_received_date))
                .ForMember(dto => dto.CreateDate, opt => opt.NullSubstitute(DateTime.Now))
                .ForMember(dto=> dto.Status,opt=> opt.ResolveUsing<OrderStatusResolver>())
                ;
            

            Mapper.CreateMap<Order, OrderStatusViewModel>()
                .ForMember(dto => dto.BillingAddress, opt => opt.ResolveUsing<AddressViewModelResolver>().FromMember(x=> x.BillingAddress))
                .ForMember(dto => dto.ShippingAddress, opt => opt.ResolveUsing<AddressViewModelResolver>().FromMember(x => x.ShippingAddress))
                .ForMember(dto => dto.OrderNumber, opt => opt.MapFrom(x => x.OrderNumber))
                .ForMember(dto => dto.PaymentMethod, opt => opt.ResolveUsing<PaymentMethodFromIntResolver>().FromMember(x=> x.PaymentID))
                .ForMember(dto => dto.SpecialInstructions, opt => opt.MapFrom(x => x.Comment))
                .ForMember(dto => dto.TotalPrice, opt => opt.MapFrom(x => x.TotalPrice))
                .ForMember(dto => dto.TotalPrice, opt => opt.AddFormatter<PriceFormatter>())
                .ForMember(dto => dto.Status, opt => opt.ResolveUsing<DescriptionFromEnumUsingAttributesResolver>().FromMember(x=> x.Status))
                .ForMember(dto=> dto.Items,opt=>opt.ResolveUsing(new CartItemsViewModelResolver(new CartItemViewModelBuilder(new FakeJewelRepository(new FakeSettingManager()),new FakeDiamondRepository(Mapper.Engine),Mapper.Engine))).FromMember(x=> x.Items))
                .ForMember(dto=> dto.PageTitle,opt=>opt.Ignore())
                .ForMember(dto => dto.PathBarItems, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Customer, usr_CUSTOMERS>()

                .ForMember(dto => dto.email, opt => opt.MapFrom(x => x.Email))
                .ForMember(dto => dto.firstname, opt => opt.MapFrom(x => x.Firstname))
                .ForMember(dto => dto.lastname, opt => opt.MapFrom(x => x.Lastname))
                .ForMember(dto => dto.state1_id, opt => opt.MapFrom(x => x.StateID))
                .ForMember(dto => dto.country1_id, opt => opt.MapFrom(x => x.CountryID))
                .ForMember(dto => dto.state2_id, opt => opt.MapFrom(x => x.StateID))
                .ForMember(dto => dto.country2_id, opt => opt.MapFrom(x => x.CountryID))
                .ForMember(dto => dto.password, opt => opt.MapFrom(x => x.Password))
                .ForMember(dto => dto.phone1, opt => opt.MapFrom(x => x.Phone))
                .ForMember(dto => dto.create_date, opt => opt.UseValue(DateTime.Now))
                .ForMember(dto => dto.EntityKey, opt => opt.Ignore())
                .ForMember(dto => dto.EntityKey, opt => opt.Ignore())
                .ForMember(dto => dto.acc_CASHFLOW, opt => opt.Ignore())
                .ForMember(dto => dto.acc_ORDERS, opt => opt.Ignore())
                .ForMember(dto => dto.active, opt => opt.Ignore())
                .ForMember(dto => dto.aid, opt => opt.Ignore())
                .ForMember(dto => dto.b_city, opt => opt.Ignore())
                .ForMember(dto => dto.b_country_id, opt => opt.Ignore())
                .ForMember(dto => dto.b_fax, opt => opt.Ignore())
                .ForMember(dto => dto.b_name, opt => opt.Ignore())
                .ForMember(dto => dto.b_phone, opt => opt.Ignore())
                .ForMember(dto => dto.b_pobox, opt => opt.Ignore())
                .ForMember(dto => dto.b_siteurl, opt => opt.Ignore())
                .ForMember(dto => dto.b_specialization, opt => opt.Ignore())
                .ForMember(dto => dto.b_state_id, opt => opt.Ignore())
                .ForMember(dto => dto.b_street, opt => opt.Ignore())
                .ForMember(dto => dto.b_tradingass, opt => opt.Ignore())
                .ForMember(dto => dto.b_type_id, opt => opt.Ignore())
                .ForMember(dto => dto.b_zip, opt => opt.Ignore())
                .ForMember(dto => dto.city1, opt => opt.Ignore())
                .ForMember(dto => dto.city2, opt => opt.Ignore())


                .ForMember(dto => dto.dateofbirth, opt => opt.Ignore())
                .ForMember(dto => dto.dealer, opt => opt.Ignore())
                .ForMember(dto => dto.default_currency, opt => opt.Ignore())
                .ForMember(dto => dto.fax1, opt => opt.Ignore())
                .ForMember(dto => dto.fax2, opt => opt.Ignore())
                .ForMember(dto => dto.historical_user, opt => opt.Ignore())
                .ForMember(dto => dto.id, opt => opt.Ignore())
                .ForMember(dto => dto.idex_percent, opt => opt.Ignore())
                .ForMember(dto => dto.inv_mail, opt => opt.Ignore())
                .ForMember(dto => dto.inv_update, opt => opt.Ignore())
                .ForMember(dto => dto.last_visit, opt => opt.Ignore())
                .ForMember(dto => dto.lastmodify_date, opt => opt.Ignore())
                .ForMember(dto => dto.lastmodify_user, opt => opt.Ignore())
                .ForMember(dto => dto.lastmodify_user_id, opt => opt.Ignore())
                .ForMember(dto => dto.old_id, opt => opt.Ignore())
                .ForMember(dto => dto.online_message, opt => opt.Ignore())
                .ForMember(dto => dto.phone1, opt => opt.Ignore())
                .ForMember(dto => dto.phone2, opt => opt.Ignore())
                .ForMember(dto => dto.pobox1, opt => opt.Ignore())
                .ForMember(dto => dto.pobox2, opt => opt.Ignore())
                .ForMember(dto => dto.prf_language_id, opt => opt.Ignore())
                .ForMember(dto => dto.prf_list_amount, opt => opt.Ignore())
                .ForMember(dto => dto.prf_srtkey_diamonds, opt => opt.Ignore())
                .ForMember(dto => dto.prf_srtkey_gemstones, opt => opt.Ignore())
                .ForMember(dto => dto.prf_srtkey_jewelry, opt => opt.Ignore())
                .ForMember(dto => dto.prf_srtkey_newitems, opt => opt.Ignore())
                .ForMember(dto => dto.prf_srtkey_search, opt => opt.Ignore())
                .ForMember(dto => dto.prf_srtkey_specials, opt => opt.Ignore())
                .ForMember(dto => dto.prf_timesvisited, opt => opt.Ignore())
                .ForMember(dto => dto.registration_ip, opt => opt.Ignore())

                .ForMember(dto => dto.street1, opt => opt.Ignore())
                .ForMember(dto => dto.street2, opt => opt.Ignore())
                .ForMember(dto => dto.sys_COUNTRY, opt => opt.Ignore())
                .ForMember(dto => dto.sys_COUNTRYReference, opt => opt.Ignore())
                .ForMember(dto => dto.sys_STATE, opt => opt.Ignore())
                .ForMember(dto => dto.sys_STATEReference, opt => opt.Ignore())
                .ForMember(dto => dto.sys_COUNTRY1, opt => opt.Ignore())
                .ForMember(dto => dto.sys_COUNTRY1Reference, opt => opt.Ignore())
                .ForMember(dto => dto.sys_STATE1, opt => opt.Ignore())
                .ForMember(dto => dto.sys_STATE1Reference, opt => opt.Ignore())
                .ForMember(dto => dto.userdeleted, opt => opt.Ignore())
                .ForMember(dto => dto.zip1, opt => opt.Ignore())
                .ForMember(dto => dto.zip2, opt => opt.Ignore())
                ;

            Mapper.CreateMap<ExtendedCustomer, BillingViewModel>()
                .ForMember(dto => dto.BillingAddress, opt => opt.ResolveUsing<AddressViewModelResolver>().FromMember(x => x.BillingAddress))
                .ForMember(dto => dto.ShippingAddress, opt => opt.ResolveUsing<AddressViewModelResolver>().FromMember(x => x.ShippingAddress))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(x => x.BillingAddress.Phone))
                .ForMember(dto => dto.CreditCardViewModel, opt => opt.Ignore())
                .ForMember(dto => dto.Comment, opt => opt.Ignore())
                .ForMember(dto => dto.PaymentMethod, opt => opt.Ignore())
                ;

            Mapper.CreateMap<usr_TESTIMONIALS, Testimonial>()
                .ForMember(dto=> dto.Body,opt=>opt.MapFrom(x=> x.body))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(x => x.name))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(x => x.title))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(x => x.sys_COUNTRYReference.Value.LANG1_LONGDESCR))
                ;



            Mapper.CreateMap<Testimonial, TestimonialViewModel>();

            Mapper.CreateMap<EmailRingModel, EmailRingEmailTemplateViewModel>()
                .ForMember(dto=> dto.Description,opt=>opt.Ignore())
                .ForMember(dto => dto.ItemNumber, opt => opt.Ignore())
                .ForMember(dto => dto.Price, opt => opt.Ignore())
                .ForMember(dto => dto.MediaSet, opt => opt.Ignore())
                .ForMember(dto => dto.Icon, opt => opt.Ignore())
                ;

            Mapper.CreateMap<MergeOrdersAndCustomer, MyAccountViewModel>()
                .ForMember(dto => dto.Orders, opt => opt.ResolveUsing<OrderSummeryResolver>().FromMember(x => x.First))
                .ForMember(dto => dto.BillingAddress, opt => opt.ResolveUsing<AddressViewModelResolver>().FromMember(x => x.Second.BillingAddress))
                .ForMember(dto => dto.ShippingAddress, opt => opt.ResolveUsing<AddressViewModelResolver>().FromMember(x=> x.Second.ShippingAddress))
                .ForMember(dto=> dto.Email,opt=> opt.MapFrom(x=> x.Second.Email))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(x => x.Second.Lastname))
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(x => x.Second.Firstname))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(x => x.Second.Country))
                .ForMember(dto => dto.State, opt => opt.MapFrom(x => x.Second.State))
                .ForMember(dto => dto.MemeberSince, opt => opt.MapFrom(x => x.Second.MemeberSince))
                .ForMember(dto => dto.MemeberSince, opt => opt.AddFormatter<ShortDateFormatter>())
                .ForMember(dto => dto.HasAddressInformation, opt => opt.ResolveUsing<MyAccountHasAddressInformationResolver>().FromMember(x=> x.Second))
                .ForMember(dto => dto.PageTitle, opt => opt.Ignore())
                .ForMember(dto => dto.PathBarItems, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Diamond, DiamondJsonUserData>()
                .ForMember(dto => dto.Depth, opt => opt.AddFormatter<PrecentFormatter>())
                .ForMember(dto => dto.Price, opt => opt.AddFormatter<PriceFormatter>())
                .ForMember(dto => dto.Table, opt => opt.AddFormatter<PrecentFormatter>())
                .ForMember(dto => dto.Fluorescence, opt => opt.MapFrom(x => x.Fluorescence))
                .ForMember(dto => dto.Dimensions, opt => opt.ResolveUsing(new DiamondDimensionsResolver()))
                .ForMember(dto=> dto.ViewURL,opt=> opt.Ignore())
                .ForMember(dto => dto.AddURL, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Jewel, SpecialOffersBannervViewModel>()
                .ForMember(dto=> dto.Icon,opt=>opt.MapFrom(x=> x.Media.HiResURLForWebDisplay))
                ;

        }
    }
}