using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper.Configuration;

namespace ImportDiamondCSV.Suppliers
{
    public class LotNet : ISupplier
    {
        private readonly int supplierCode;

        public int DiamondID
        {
            get
            {
                var diamondID = (supplierCode.ToString() + InventoryCode).GetHashCode();
                diamondID = Math.Abs(diamondID);
                return diamondID;
            }
        }

        public int SupplierCode
        {
            get { return supplierCode; }
        }

        [CsvField(Index = 0)]
        [TypeConverter(typeof(SpaceTrimmerAndRemoverForInventoryID))]
        public string InventoryCode { get; set; }

        [CsvField(Index = 1)]
        public string Shape { get; set; }

        [CsvField(Index = 2)]

        public decimal Weight { get; set; }

        [CsvField(Index = 3)]
        [TypeConverter(typeof(DashRomoverAndHigherColorRetainer))]
        public string Color { get; set; }

        [CsvField(Index = 4)]
        [TypeConverter(typeof(SpaceTrimmerAndRemoverForInventoryID))]
        public string Clarity { get; set; }

        [CsvField(Index = 5)]
        public decimal Price { get; set; }

        [CsvField(Index = 8)]
        public string Report { get; set; }

        [CsvField(Ignore = true)]
        public string ReportURL { get; set; }

        [CsvField(Index = 9)]
        public string ReportNumber { get; set; }

        [CsvField(Index = 10)]
        [TypeConverter(typeof(IgalWidthConvertor))]
        public decimal Width { get; set; }
        [CsvField(Index = 10)]
        [TypeConverter(typeof(IgalLegthConvertor))]
        public decimal Length { get; set; }
        [CsvField(Index = 10)]
        [TypeConverter(typeof(IgalDepthConvertor))]
        public decimal Height { get; set; }

        [CsvField(Index = 11)]
        public decimal DepthPresentage { get; set; }

        [CsvField(Index = 12)]
        public decimal Table { get; set; }

        [CsvField(Index = 13)]
        public string Girdle { get; set; }

        [CsvField(Index = 14)]
        public string Culet { get; set; }

        [CsvField(Index = 15)]
        public string Polish { get; set; }

        [CsvField(Index = 16)]
        [TypeConverter(typeof(DashRomoverAndHigherColorRetainer))]
        public string Symmetry { get; set; }

        [CsvField(Index = 17)]
        public string Fluorescence { get; set; }

        [CsvField(Index = 22)]
        public string Cut { get; set; }

        public void ExecuteBeforeMapping()
        {
            
        }


        public LotNet()
        {
            supplierCode = 3;
        
        }

        public static Dictionary<string, Dictionary<string, int>> ConversionDictionary = new Dictionary
            <string, Dictionary<string, int>>()
                                                                                             {
                                                                                                 {
                                                                                                     "shape",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {
                                                                                                                 "RD", 1
                                                                                                                 },
                                                                                                             {
                                                                                                                 "CU",
                                                                                                                 2
                                                                                                                 },
                                                                                                             {
                                                                                                                 "EM"
                                                                                                                 ,
                                                                                                                 3
                                                                                                                 },
                                                                                                             {
                                                                                                                 "MQ"
                                                                                                                 , 4
                                                                                                                 },
                                                                                                             {"OV", 5},
                                                                                                             {"PR", 6},
                                                                                                             {
                                                                                                                 "PS"
                                                                                                                 , 7
                                                                                                                 },
                                                                                                             {
                                                                                                                 "RAD"
                                                                                                                 ,
                                                                                                                 8
                                                                                                                 },
                                                                                                             {
                                                                                                                 "AS"
                                                                                                                 ,
                                                                                                                 9
                                                                                                                 },
                                                                                                             {
                                                                                                                 "HS",
                                                                                                                 10
                                                                                                                 },
                                                                                                             {
                                                                                                                 "Triangular"
                                                                                                                 , 11
                                                                                                                 },
                                                                                                             {
                                                                                                                 "BAG"
                                                                                                                 , 12
                                                                                                                 },
                                                                                                             {
                                                                                                                 "Shield"
                                                                                                                 ,
                                                                                                                 13
                                                                                                                 },
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "clarity",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {

                                                                                                             {"I2", 2},
                                                                                                             {"IF", 3},
                                                                                                             {"SI1", 4},
                                                                                                             {"SI2", 5},
                                                                                                             {"SI3", 6},
                                                                                                             {"VS1", 7},
                                                                                                             {"VS2", 8},
                                                                                                             {"VVS1", 9},
                                                                                                             {
                                                                                                                 "VVS2",
                                                                                                                 10
                                                                                                                 },
                                                                                                             {"I1", 1},


                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "color",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"D", 1},
                                                                                                             {"E", 2},
                                                                                                             {"F", 3},
                                                                                                             {"G", 4},
                                                                                                             {"H", 5},
                                                                                                             {"I", 6},
                                                                                                             {"J", 7},
                                                                                                             {"K", 8},
                                                                                                             {"L", 9},
                                                                                                             {"M", 10},
                                                                                                             {"N", 11},
                                                                                                             {
                                                                                                                 "Fancy"
                                                                                                                 ,
                                                                                                                 12
                                                                                                                 },
                                                                                                             {
                                                                                                                 "CAPE",
                                                                                                                 13
                                                                                                                 },
                                                                                                             {
                                                                                                                 "None",
                                                                                                                 14
                                                                                                                 },
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "culet",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"N", 1},
                                                                                                             {"NON", 1},
                                                                                                             {"NONE", 1},
                                                                                                             {"VSM", 2},
                                                                                                             {"MED", 3},
                                                                                                             {"MD", 3},
                                                                                                             {"LGE", 3},
                                                                                                             {"SML", 4},
                                                                                                             {
                                                                                                                 "POINTED"
                                                                                                                 ,
                                                                                                                 5
                                                                                                                 },
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "fluorescence",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"FNT", 1},
                                                                                                             {"Faint", 1},
                                                                                                             {"NON", 4},
                                                                                                             {"NONE", 4},
                                                                                                             {"N", 4},
                                                                                                             {"MEDIUM", 2},
                                                                                                             {"MED", 2},
                                                                                                             {"MODERATE", 2},
                                                                                                             {"MB", 3},
                                                                                                             {"MODERATE BLUE", 3},
                                                                                                             {"Medium Blue", 3},
                                                                                                             {"SLIGHT", 5},
                                                                                                             {"STG", 6},
                                                                                                             {"SB", 7},
                                                                                                             {"V.SLIGHT", 8},
                                                                                                             {"V.SL", 8},
                                                                                                             {"V SLIGHT B", 8},
                                                                                                             {"VERY SLIGHT", 8},
                                                                                                             {"VST", 9},
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 4
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "polish",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"EX", 1},
                                                                                                             {"VG", 2},
                                                                                                             {"GD", 3},
                                                                                                             {"G TO VG", 3},
                                                                                                             {"G-VG", 3},
                                                                                                             {"G", 4},
                                                                                                             {"GOOD ", 4},
                                                                                                             {"F", 5},
                                                                                                             {"P", 6},
                                                                                                             { "default",4 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "report",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"GIA", 1},
                                                                                                             {"EGL", 2},
                                                                                                             {"HRD", 3},
                                                                                                             {"IGI", 4},
                                                                                                             {"EGI", 5},
                                                                                                             { "default",1 }
                                                                                                         }
                                                                                                        
                                                                                                     },
                                                                                                 {
                                                                                                     "symmetry",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                              {"EX", 1},
                                                                                                             {"VG", 2},
                                                                                                             {"GD", 3},
                                                                                                             {"G TO VG", 3},
                                                                                                             {"G-VG", 3},
                                                                                                             {"G", 4},
                                                                                                             {"GOOD ", 4},
                                                                                                             {"F", 5},
                                                                                                             {"P", 6},
                                                                                                             
                                                                                                             { "default",4 }
                                                                                                         }
                                                                                                     },

                                                                                                 {
                                                                                                     "cut",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"EX", 1},
                                                                                                             {"VG", 2},
                                                                                                             {"GD", 3},
                                                                                                             {"G", 4},
                                                                                                             {"F", 5},
                                                                                                             {"P", 6},
                                                                                                             
                                                                                                             { "default",4 }
                                                                                                         }
                                                                                                     }
                                                                                             };


        public override bool Equals(object obj)
        {
            var compareTo = (LotNet)obj;
            if (compareTo.InventoryCode == this.InventoryCode)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return InventoryCode.GetHashCode();
        }


    }

    public class DashRomoverAndHigherColorRetainer : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return 1;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return ((string) value).Split('-')[0];
        }
    }

    public class LotNetMeasurmentsFromLengthConvertor : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return 1;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var list = new List<decimal>();
            if (Regex.Matches((string)value, "\\d+(\\.\\d{1,2})?").Count >= 3)
            {
                list.Add(Convert.ToDecimal(Regex.Matches((string)value, "\\d+(\\.\\d{1,2})?")[0].Value));
                list.Add(Convert.ToDecimal(Regex.Matches((string)value, "\\d+(\\.\\d{1,2})?")[1].Value));
                list.Add(Convert.ToDecimal(Regex.Matches((string)value, "\\d+(\\.\\d{1,2})?")[2].Value));

                return list;
            }
            return list;
        }
    }

   
}
