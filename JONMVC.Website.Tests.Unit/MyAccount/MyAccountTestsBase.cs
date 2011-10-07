using AutoMapper;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Mvc.Mailer;
namespace JONMVC.Website.Tests.Unit.MyAccount
{
    public class MyAccountTestsBase
    {
        protected IMappingEngine mapper;
        protected Fixture fixture;

        /// <summary>
        /// Prepares mock repository
        /// </summary>

        [TestFixtureSetUp]
        public void InitializeFixture()
        {
            //Bootstrapper.Excluding.Assembly("JONMVC.Core.Configurations").With.AutoMapper().Start();
            MapsContainer.CreateAutomapperMaps();
            mapper = Mapper.Engine;

            fixture = new Fixture();
            MailerBase.IsTestModeEnabled = true;
        }
    }
}