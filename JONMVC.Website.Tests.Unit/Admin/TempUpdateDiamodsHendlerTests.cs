using System.IO;
using System.Text;
using System.Web;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;
using JONMVC.Website.Models.Admin;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace JONMVC.Website.Tests.Unit.Admin
{
    [TestFixture]
    public class TempUpdateDiamodsHendlerTests:MapperAndFixtureBase
    {

        [Test]
        public void ParseAndSave_ShouldReadTheFilesFromTheRequest()
        {
            //Arrange
            var model = fixture.Build<UpdateDiamondsModel>().With(x => x.Supplier, "SampleCSVModel").CreateAnonymous();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();

            var fileCollection = MockRepository.GenerateStub<HttpFileCollectionBase>();

            var file = MockRepository.GenerateStub<HttpPostedFileBase>();
            file.Expect(x => x.InputStream).Return(new MemoryStream());

            fileCollection.Stub(x => x[0]).Return(file);

            var request = MockRepository.GenerateMock<HttpRequestBase>();
            request.Expect(x => x.Files).Repeat.Once().Return(fileCollection);

            httpContext.Stub(x => x.Request).Return(request);

            var csvParser = MockRepository.GenerateStub<ICSVParser>();
            var db = MockRepository.GenerateStub<IDatabasePersistence>();

            var hendler = new TempUpdateDiamodsHendler(model,httpContext,csvParser,db);
            //Act
            hendler.ParseAndSave();
            
            //Assert
            request.VerifyAllExpectations();
        }

        [Test]
        public void ParseAndSave_ShouldReadTheStreamFromTheFile()
        {
            //Arrange
            var model = fixture.Build<UpdateDiamondsModel>().With(x => x.Supplier, "SampleCSVModel").CreateAnonymous();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();

            var fileCollection = MockRepository.GenerateStub<HttpFileCollectionBase>();

            var file = MockRepository.GenerateMock<HttpPostedFileBase>();
            file.Expect(x => x.InputStream).Return(new MemoryStream());

            fileCollection.Stub(x => x[0]).Return(file);

            var request = MockRepository.GenerateStub<HttpRequestBase>();
            request.Stub(x => x.Files).Repeat.Once().Return(fileCollection);

            httpContext.Stub(x => x.Request).Return(request);

            var csvParser = MockRepository.GenerateStub<ICSVParser>();
            var db = MockRepository.GenerateStub<IDatabasePersistence>();

            var hendler = new TempUpdateDiamodsHendler(model, httpContext, csvParser, db);
            //Act
            hendler.ParseAndSave();

            //Assert
            request.VerifyAllExpectations();
        }

        [Test]
        public void ParseAndSave_ShouldParseTheUploadedFile()
        {
            //Arrange

            var model = fixture.Build<UpdateDiamondsModel>().With(x => x.Supplier, "SampleCSVModel").CreateAnonymous();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();

            var fileCollection = MockRepository.GenerateStub<HttpFileCollectionBase>();

            var file = MockRepository.GenerateStub<HttpPostedFileBase>();
            file.Expect(x => x.InputStream).Return(new MemoryStream());

            fileCollection.Stub(x => x[0]).Return(file);

            var request = MockRepository.GenerateStub<HttpRequestBase>();
            request.Stub(x => x.Files).Repeat.Once().Return(fileCollection);

            httpContext.Stub(x => x.Request).Return(request);

            var csvParser = MockRepository.GenerateStub<ICSVParser>();
            csvParser.Expect(x => x.Parse<SampleCSVModel>(Arg<Stream>.Is.Anything)).Repeat.Once();
            var db = MockRepository.GenerateStub<IDatabasePersistence>();

            var hendler = new TempUpdateDiamodsHendler(model, httpContext, csvParser, db);
            //Act
            hendler.ParseAndSave();

            //Assert
            csvParser.VerifyAllExpectations();
        }

    }


    public class SampleCSVModel:ISupplier
    {
        public int DiamondID { get; private set; }
        public int SupplierCode { get; private set; }
        public string InventoryCode { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public decimal Price { get; set; }
        public string Report { get; set; }
        public string ReportURL { get; set; }
        public string ReportNumber { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public decimal DepthPresentage { get; set; }
        public decimal Table { get; set; }
        public string Girdle { get; set; }
        public string Culet { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public string Cut { get; set; }

        public void ExecuteBeforeMapping()
        {
            
        }

        public PricePolicy SupplierPricePolicy
        {
            get { return PricePolicy.AsIs; }
        }
    }
}