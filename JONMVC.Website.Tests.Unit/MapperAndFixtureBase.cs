using AutoMapper;
using JONMVC.Website.Mailers;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using Mvc.Mailer;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit
{
    public class MapperAndFixtureBase
    {
        protected Fixture fixture;
        protected IMappingEngine mapper;

        /// <summary>
        /// Prepares mock repository
        /// </summary>

        [TestFixtureSetUp]
        public void FixtureInitialize()
        {
            mapper = Mapper.Engine;
            MapsContainer.CreateAutomapperMaps();
        }

        [SetUp]
        public void Initialize()
        {
            fixture = new Fixture();
            fixture.Customize<AskQuestionEmailTemplateViewModel>(t => t.With(x => x.Email, Tests.SAMPLE_EMAIL_ADDRESS));
            MailerBase.IsTestModeEnabled = true;


        }
    }
}