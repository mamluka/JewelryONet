using System.Reflection;
using AutoMapper;
using JONMVC.Website.Tests.Unit.AutoMapperMaps;
using NUnit.Framework;

namespace JONMVC.Website.Tests.Unit.JewelDesign
{
    public class JewelDesignTestsBase
    {
        protected IMappingEngine mapper;
        public static int SETTING_ID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID;
        public static int FIRST_DIAMOND_IN_REP = 1;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        /// 
        [TestFixtureSetUp]
        public void InitializeFixture()
        {
            //Bootstrapper.Excluding.Assembly("JONMVC.Core.Configurations").With.AutoMapper().Start();
            MapsContainer.CreateAutomapperMaps();

                

            mapper = Mapper.Engine;
        }

        [SetUp]
        public void Initialize()
        {
           
        }
    }
}