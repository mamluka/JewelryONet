using AutoMapper;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.JewelryItem
{
    public class JewelryItemTestsBase
    {
        protected Fixture fixture;
        protected IMappingEngine mapper;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureInitialize()
        {
            fixture = new Fixture();
            MapsContainer.CreateAutomapperMaps();
            mapper = Mapper.Engine;
        }
    }
}