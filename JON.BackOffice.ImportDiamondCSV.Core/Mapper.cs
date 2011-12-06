using System;
using System.Collections.Generic;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;

namespace JON.BackOffice.ImportDiamondCSV.Core
{
    public class Mapper<T> : IMapper<T> where T:IDiamond, new()
    {
        private readonly Dictionary<string, Dictionary<string, int>> conversionDictionary;
        private PricePolicy pricePolicy;
        private decimal priceCalibration;

        public Mapper(Dictionary<string, Dictionary<string, int>> conversionDictionary)
        {
            this.conversionDictionary = conversionDictionary;
            priceCalibration = 1;
            pricePolicy = PricePolicy.AsIs;
            
        }

        public IDiamond Map(ISupplier supplierDiamond)
        {
            
            var inv = new T();

            supplierDiamond.ExecuteBeforeMapping();

            switch (pricePolicy)
            {
                case PricePolicy.AsIs:
                    inv.totalprice = supplierDiamond.Price;
                    break;
                case PricePolicy.MultiplyByWeight:
                    inv.totalprice = supplierDiamond.Price*supplierDiamond.Weight;
                    break;
                case PricePolicy.MultiplyByWeightAndCalibrate:
                    inv.totalprice = supplierDiamond.Price*supplierDiamond.Weight*priceCalibration;
                    break;
                case PricePolicy.Calibrate:
                    inv.totalprice = supplierDiamond.Price * priceCalibration;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            

            inv.inventory_code  = supplierDiamond.InventoryCode;
            inv.supplier_code = supplierDiamond.SupplierCode;
            inv.clarity = MapOrDefault("clarity",supplierDiamond.Clarity);
            inv.color = MapOrDefault("color", supplierDiamond.Color);
            inv.depth = supplierDiamond.DepthPresentage;
            inv.fluorescence = MapOrDefault("fluorescence", supplierDiamond.Fluorescence);
            inv.length = supplierDiamond.Length;
            inv.polish = MapOrDefault("polish", supplierDiamond.Polish);
            inv.report = MapOrDefault("report", supplierDiamond.Report);
            inv.shape = MapOrDefault("shape", supplierDiamond.Shape);
            inv.symmetrical = MapOrDefault("symmetry", supplierDiamond.Symmetry);
            inv.table = supplierDiamond.Table;
            inv.width = supplierDiamond.Width;
            inv.weight = supplierDiamond.Weight;
            inv.height = supplierDiamond.Height;

            inv.report_number = supplierDiamond.ReportNumber ?? "0";
            inv.crown = 1;
            inv.culet = 1;
            inv.grindle = supplierDiamond.Girdle;
            inv.reportimg = "";

            inv.diamondid = supplierDiamond.DiamondID;
            inv.cut = MapOrDefault("cut", supplierDiamond.Cut);

            return inv;
        }

        private int MapOrDefault(string tablekey,string key)
        {
            return conversionDictionary[tablekey].ContainsKey(key.Trim()) ? conversionDictionary[tablekey][key.Trim()] : conversionDictionary[tablekey]["default"];
        }

        public void SetPricePolicy(PricePolicy pricePolicy)
        {
            this.pricePolicy = pricePolicy;
        }

        public void OurPriceCalibration(decimal cal)
        {
            this.priceCalibration = cal;
        }
    }

    public enum PricePolicy
    {
        AsIs=1,
        MultiplyByWeight=2,
        MultiplyByWeightAndCalibrate=3,
        Calibrate=4
    }
}
