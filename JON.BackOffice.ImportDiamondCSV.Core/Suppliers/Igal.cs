using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using CsvHelper.Configuration;

namespace JON.BackOffice.ImportDiamondCSV.Core.Suppliers
{
    public class Igal : ISupplier
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
        [TypeConverter(typeof(IgalDiamondIDTypeConvertor))]
        public string InventoryCode { get; set; }
        [CsvField(Index = 1)]
        public string Shape { get; set; }
        [CsvField(Index = 2)]
        public decimal Weight { get; set; }
        [CsvField(Index = 3)]
        public string Color { get; set; }
        [CsvField(Index = 4)]
        public string Clarity { get; set; }
        [CsvField(Index = 5)]
        public decimal Price { get; set; }
        [CsvField(Index = 8)]
        public string Report { get; set; }
        [CsvField(Ignore = true)]
        public string ReportURL { get; set; }

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
        public string Symmetry { get; set; }
        [CsvField(Index = 17)]
        public string Fluorescence { get; set; }
        [CsvField(Index = 22)]
        public string Cut { get; set; }

        public void ExecuteBeforeMapping()
        {
            
        }

        public PricePolicy SupplierPricePolicy
        {
            get { return PricePolicy.Calibrate; }
        }

        public Igal()
        {
            supplierCode = 1;
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
                                                                                                                 "BR", 1
                                                                                                                 },
                                                                                                             {
                                                                                                                 "CUSH",
                                                                                                                 2
                                                                                                                 },
                                                                                                             {
                                                                                                                 "Emerald"
                                                                                                                 ,
                                                                                                                 3
                                                                                                                 },
                                                                                                             {
                                                                                                                 "MQ"
                                                                                                                 , 4
                                                                                                                 },
                                                                                                             {"Oval", 5},
                                                                                                             {"PR", 6},
                                                                                                             {
                                                                                                                 "PS"
                                                                                                                 , 7
                                                                                                                 },
                                                                                                             {
                                                                                                                 "Radiant"
                                                                                                                 ,
                                                                                                                 8
                                                                                                                 },
                                                                                                             {
                                                                                                                 "Asscher"
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
                                                                                                                 "Baguette"
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
                                                                                                             {"VS", 2},
                                                                                                             {"M", 3},
                                                                                                             {"S", 4},
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
                                                                                                             {
                                                                                                                 "FAIT"
                                                                                                                 , 1
                                                                                                                 },
                                                                                                             {
                                                                                                                 "Medium"
                                                                                                                 ,
                                                                                                                 2
                                                                                                                 },
                                                                                                             {
                                                                                                                 "MB"
                                                                                                                 , 3
                                                                                                                 },
                                                                                                             {"N", 4},
                                                                                                             {
                                                                                                                 "SLIGHT"
                                                                                                                 ,
                                                                                                                 5
                                                                                                                 },
                                                                                                             {
                                                                                                                 "VSB"
                                                                                                                 ,
                                                                                                                 6
                                                                                                                 },
                                                                                                             {
                                                                                                                 "SB"
                                                                                                                 , 7
                                                                                                                 },
                                                                                                             {
                                                                                                                 "VSL"
                                                                                                                 , 8
                                                                                                                 },
                                                                                                             {
                                                                                                                 "VS"
                                                                                                                 , 9
                                                                                                                 },
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "polish",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {
                                                                                                                 "EX"
                                                                                                                 , 1
                                                                                                                 },
                                                                                                             {"VG", 2},
                                                                                                             {"G", 3},
                                                                                                             {
                                                                                                                 "G TO VG"
                                                                                                                 , 4
                                                                                                                 },
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "report",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"GIA", 1},
                                                                                                             {
                                                                                                                 "EGL IL"
                                                                                                                 ,
                                                                                                                 2
                                                                                                                 },
                                                                                                             {
                                                                                                                 "EGL USA"
                                                                                                                 ,
                                                                                                                 2
                                                                                                                 },
                                                                                                             {"HRD", 3},
                                                                                                             {"IGI", 4},
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }

                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "symmetry",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {
                                                                                                                 "EX"
                                                                                                                 , 1
                                                                                                                 },
                                                                                                             {"VG", 2},
                                                                                                             {
                                                                                                                 "G TO VG"
                                                                                                                 ,
                                                                                                                 2
                                                                                                                 },
                                                                                                             {"G", 3},
                                                                                                             {"Fair", 4},
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     },

                                                                                                 {
                                                                                                     "cut",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"None", 1},
                                                                                                             {"EX", 2},
                                                                                                             {"VG", 3},
                                                                                                             {
                                                                                                                 "G TO VG"
                                                                                                                 ,
                                                                                                                 4
                                                                                                                 },
                                                                                                             {"G", 5},
                                                                                                             {
                                                                                                                 "default"
                                                                                                                 ,
                                                                                                                 1
                                                                                                                 }
                                                                                                         }
                                                                                                     }
                                                                                             };


    }

    public class IgalDiamondIDTypeConvertor:TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return 1;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return ((string) value).Replace(" ", "");
        }
    }

    public class IgalWidthConvertor : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return 1;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return Convert.ToDecimal(Regex.Matches((string)value, "\\d+\\.\\d{2}?")[0].Value);
        }
    }

    public class IgalLegthConvertor : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return 1;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return Convert.ToDecimal(Regex.Matches((string)value, "\\d+\\.\\d{2}?")[1].Value);
        }
    }

    public class IgalDepthConvertor : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return 1;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return Convert.ToDecimal(Regex.Matches((string)value, "\\d+\\.\\d{2}?")[2].Value);
        }
    }

}
