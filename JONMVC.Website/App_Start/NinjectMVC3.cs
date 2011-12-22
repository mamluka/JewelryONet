using System.IO.Abstractions;
using System.Web;
using AutoMapper;
using AutoMapper.Mappers;
using ComLib.Authentication;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JONMVC.Website.Controllers;
using JONMVC.Website.Mailers;
using JONMVC.Website.Models.Checkout;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.JewelryItem;
using JONMVC.Website.Models.Ninject;
using JONMVC.Website.Models.Services;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Models.Utils;
using Ninject.Modules;

[assembly: WebActivator.PreApplicationStartMethod(typeof(JONMVC.Website.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(JONMVC.Website.App_Start.NinjectMVC3), "Stop")]

namespace JONMVC.Website.App_Start
{
    using System.Reflection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<NinjectModule>().To<StandardModule>();

            kernel.Bind<ITabsRepository>().To<TabsRepository>();
            kernel.Bind<IFileSystem>().To<FileSystem>();
            kernel.Bind<IJewelRepository>().To<JewelRepository>();
            kernel.Bind<IXmlSourceFactory>().To<XmlSourceFactory>();
            kernel.Bind<ISettingManager>().To<SettingManager>();
            kernel.Bind<IMediaSetBuilder>().To<MediaSetBuilder>();
            kernel.Bind<IDiamondRepository>().To<DiamondRepository>();
            kernel.Bind<IJONFormatter>().To<JONFormatter>();
            kernel.Bind<IWebHelpers>().To<WebHelpers>();
            //kernel.Bind<IShoppingCart>().ToMethod(x => ShoppingCart.GetShoppingCart(new HttpContextWrapper(HttpContext.Current) ));  //.To<ShoppingCart>().ShoppingCart.FromSession((HttpContext.Current));
            kernel.Bind<IShoppingCart>().To<ShoppingCart>();
            //kernel.Bind<HttpContextBase>().ToMethod(x => new HttpContextWrapper(HttpContext.Current));
            kernel.Bind<IShoppingCartWrapper>().To<ShoppingCartWrapper>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<IUserMailer>().To<UserMailer>();
            kernel.Bind<IBestOffer>().To<BestOffer>();
            kernel.Bind<IWishListPersistence>().To<CookieWishListPersistence>();
            kernel.Bind<IPathBarGenerator>().To<PathBarGenerator>();
            kernel.Bind<IAuthentication>().To<CookieAuthentication>();
            kernel.Bind<ICustomerAccountService>().To<DataBaseCustomerAccountService>();
            kernel.Bind<ITestimonialRepository>().To<TestimonialRepository>();
            kernel.Bind<ICSVParser>().To<CSVParser>();
            kernel.Bind<IDatabasePersistence>().To<DatabasePersistence>();

            kernel.Bind<IViewModelBuilder>().To<WishListViewModelBuilder>().WhenInjectedInto<ServicesController>();

            kernel.Load<AutoMapperModule>();
            



        }


        public class AutoMapperModule : NinjectModule
        {
            public override void Load()
            {
                //Bind<ITypeMapFactory>().To<TypeMapFactory>();
               // Bind<Configuration>().ToConstant(new Configuration(Kernel.Get<ITypeMapFactory>(), MapperRegistry.AllMappers())).InSingletonScope();
               // Bind<IConfiguration>().ToMethod(c => c.Kernel.Get<Configuration>());
               // Bind<IConfigurationProvider>().ToMethod(c => c.Kernel.Get<Configuration>());
                //Bind<IMappingEngine>().To<MappingEngine>();
                Bind<IMappingEngine>().ToConstant(Mapper.Engine);
            }
        }

    }
}
