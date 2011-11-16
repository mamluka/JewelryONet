using AutoMapper;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using Mvc.Mailer;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Services
{
    public class CheckoutTestsBase
    {
        protected Fixture fixture;
        protected IMappingEngine mapper;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [TestFixtureSetUp]
        public void InitializeFixture()
        {
            fixture = new Fixture();

            MapsContainer.CreateAutomapperMaps();

            mapper = Mapper.Engine;

            MailerBase.IsTestModeEnabled = true;
        }
    }
}