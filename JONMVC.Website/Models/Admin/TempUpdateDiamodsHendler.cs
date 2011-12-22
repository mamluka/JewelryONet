using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;

namespace JONMVC.Website.Models.Admin
{
    public class TempUpdateDiamodsHendler
    {
        private readonly UpdateDiamondsModel model;
        private readonly HttpContextBase httpContext;
        private readonly ICSVParser csvParser;
        private readonly IDatabasePersistence db;

        public TempUpdateDiamodsHendler(UpdateDiamondsModel model, HttpContextBase httpContext, ICSVParser csvParser,
                                        IDatabasePersistence db)
        {
            this.model = model;
            this.httpContext = httpContext;
            this.csvParser = csvParser;
            this.db = db;
        }

        public void ParseAndSave()
        {
            var file = httpContext.Request.Files[0];

            IEnumerable<ISupplier> list = null;

            Dictionary<string, Dictionary<string, int>> ConversionDictionary = null;

            switch (model.Supplier)
            {
                case "Igal":
                    list = csvParser.Parse<Igal>(file.InputStream);
                    ConversionDictionary = Igal.ConversionDictionary;
                    break;
                case "IgalGIA":
                    list = csvParser.Parse<IgalGIA>(file.InputStream);
                    ConversionDictionary = IgalGIA.ConversionDictionary;
                    break;
            }

            list = list.Distinct().ToList();

            

            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(ConversionDictionary);
            mapper.SetPricePolicy(PricePolicy.Calibrate);
            mapper.OurPriceCalibration((decimal)1.07);

            var dblist = list.Select(mapper.Map).ToList();

            var db = new DatabasePersistence();
            db.AddSupplierDiamondList(dblist);
            db.SaveOrUpdate();



        }
    }
}