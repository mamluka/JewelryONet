using System.Data;
using NDbUnit.Core.SqlClient;
using NUnit.Framework;

namespace JONMVC.Website.Tests.Integration.DBFixtures
{
    [TestFixture]
    public class CreateFixture
    {
        private const string connectionString = "Data Source=(local);Initial Catalog=JONet;Persist Security Info=True;User ID=jon;Password=0953acb";
        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
      [Ignore]
        public void CreateFixtureNow()
        {


            SqlDbUnitTest dbUnit = new SqlDbUnitTest(connectionString);

            dbUnit.ReadXmlSchema(@"DBFixtures/JewelryItems.xsd");

            DataSet ds = dbUnit.GetDataSetFromDb();

            ds.WriteXml(@"DBFixtures/JewelryItemsAllCategories.xml");

        }

    }
}