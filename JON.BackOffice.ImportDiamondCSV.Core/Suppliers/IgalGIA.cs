using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using CsvHelper.Configuration;

namespace JON.BackOffice.ImportDiamondCSV.Core.Suppliers
{
    public class IgalGIA:ISupplier
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

        [CsvField(Index = 1)]
        [TypeConverter(typeof(IgalDiamondIDTypeConvertor))]
        public string InventoryCode { get; set; }

        [CsvField(Index = 2)]
        public string Shape { get; set; }

        [CsvField(Index = 3)]

        public decimal Weight { get; set; }

        [CsvField(Index = 4)]
        public string Color { get; set; }

        [CsvField(Index = 5)]
        public string Clarity { get; set; }

        [CsvField(Index = 21)]
        [TypeConverter(typeof(FromCurrecntyFormatToDecimalConverter))]
        public decimal Price { get; set; }

        [CsvField(Ignore = true)]
        public string Report { get; set; }

        [CsvField(Ignore = true)]
        public string ReportURL { get; set; }

        [CsvField(Index = 18)]
        public string ReportNumber { get; set; }

        [CsvField(Index = 16)]
        [TypeConverter(typeof(IgalMeasurmentPartConvertor))]
        public decimal Width { get; set; }
        [CsvField(Index = 15)]
        [TypeConverter(typeof(IgalMeasurmentPartConvertor))]
        public decimal Length { get; set; }
        [CsvField(Index = 17)]
        [TypeConverter(typeof(IgalMeasurmentPartConvertor))]
        public decimal Height { get; set; }
        [CsvField(Index = 10)]
        public decimal DepthPresentage { get; set; }

        [CsvField(Index = 15)]
        [TypeConverter(typeof(IgalGIAMeasurmentsFromLengthConvertor))]
        public List<decimal> MeasurmentsFromLengthNonStandart { get; set; }

        [CsvField(Index = 11)]
        public decimal Table { get; set; }

        [CsvField(Index = 12)]
        public string Girdle { get; set; }

        [CsvField(Index = 14)]
        public string Culet { get; set; }

        [CsvField(Index = 7)]
        public string Polish { get; set; }

        [CsvField(Index = 8)]
        public string Symmetry { get; set; }

        [CsvField(Index = 9)]
        public string Fluorescence { get; set; }

        [CsvField(Index = 6)]
        public string Cut { get; set; }

        public void ExecuteBeforeMapping()
        {
            if (MeasurmentsFromLengthNonStandart.Count == 3)
            {
                Length = MeasurmentsFromLengthNonStandart[1];
                Width = MeasurmentsFromLengthNonStandart[2];
                Height = MeasurmentsFromLengthNonStandart[2];
            }
        }

        public PricePolicy SupplierPricePolicy
        {
            get { return PricePolicy.MultiplyByWeightAndCalibrate; }
        }

        public IgalGIA()
        {
            supplierCode = 2;
            Report = "GIA";
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
                                                                                                                 "RBC", 1
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
                                                                                                             {"NON", 1},
                                                                                                             {"/", 1},
                                                                                                             {"", 1},
                                                                                                             {"VSM", 2},
                                                                                                             {"SML", 4},
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
                                                                                                             {"NON", 4},
                                                                                                             {"MED", 2},
                                                                                                             {"MB", 3},
                                                                                                             {"STG", 6},
                                                                                                             
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
                                                                                                             {"G", 4},
                                                                                                             {"F", 5},
                                                                                                             
                                                                                                             { "default",4 }
                                                                                                         }
                                                                                                     },
                                                                                                 {
                                                                                                     "report",
                                                                                                     new Dictionary
                                                                                                     <string, int>()
                                                                                                         {
                                                                                                             {"GIA", 1},
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
                                                                                                             {"G", 4},
                                                                                                             {"F", 5},
                                                                                                             
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
                                                                                                             
                                                                                                             { "default",4 }
                                                                                                         }
                                                                                                     }
                                                                                             };


        public override bool Equals(object obj)
        {
            var compareTo = (IgalGIA) obj;
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

    public class IgalGIAMeasurmentsFromLengthConvertor:TypeConverter
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

    public class IgalMeasurmentPartConvertor:TypeConverter
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

            decimal number;
            if (Decimal.TryParse(((string)value).Trim(), out number))
            {
                return Convert.ToDecimal(number);
            }
            return Convert.ToDecimal(0);
        }
    }

    public class FromCurrecntyFormatToDecimalConverter : TypeConverter
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

            var priceString = ((string) value).Trim().Replace(",", String.Empty);
            if ( String.IsNullOrWhiteSpace(priceString))
            {
                return (decimal)0;
            }
            return Convert.ToDecimal(priceString);

        }
    }
}
