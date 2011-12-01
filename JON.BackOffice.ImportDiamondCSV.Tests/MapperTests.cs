using System;
using System.Collections.Generic;
using System.Text;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace JON.BackOffice.ImportDiamondCSV.Tests
{
    [TestFixture]
    public class MapperTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Map_ShouldMapThePriceCorrectyWithAsIsPolicy()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
                                  {
                                      Clarity = "SI2",
                                      Culet = "",
                                      Color = "H",
                                      Cut = "VG",
                                      DepthPresentage = (decimal) 4.25,
                                      InventoryCode = "12345",
                                      Fluorescence = "MB",
                                      Girdle = "",
                                      Length = (decimal) 3.5,
                                      Polish = "G",
                                      Price = 9999,
                                      Report = "EGL IL",
                                      ReportURL ="",
                                      Shape = "BR",
                                      Symmetry = "EX",
                                      Table = (decimal) 54.5,
                                      Weight = (decimal) 2.41,
                                      Width = (decimal) 6.35

                                  };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.totalprice.Should().Be(9999);

        }

        [Test]
        public void Map_ShouldMapThePriceCorrectyWithMultiplyByWeight()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            mapper.SetPricePolicy(PricePolicy.MultiplyByWeight);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.totalprice.Should().Be((decimal) 24097.59);

        }

        [Test]
        public void Map_ShouldMapThePriceCorrectyWithMultiplyByWeightAndCalibration()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            mapper.SetPricePolicy(PricePolicy.MultiplyByWeightAndCalibrate);
            mapper.OurPriceCalibration((decimal) 1.17);

            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.totalprice.Should().Be((decimal)28194.1803);

        }

        [Test]
        public void Map_ShouldMapTheDiamondID()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage = (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.diamondid.Should().Be(1456730945);

        }

        [Test]
        public void Map_ShouldMapTheInventoryCode()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.inventory_code.Should().Be("12345");

        }

        [Test]
        public void Map_ShouldMapTheClarityCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.clarity.Should().Be(5);

        }

        [Test]
        public void Map_ShouldMapTheColorCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.color.Should().Be(5);

        }

        [Test]
        public void Map_ShouldMapTheGradeCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.cut.Should().Be(3);

        }

        [Test]
        public void Map_ShouldMapTheDepthCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.depth.Should().Be((decimal) 4.25);

        }

        [Test]
        public void Map_ShouldMapTheFluorescenceCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.fluorescence.Should().Be(3);

        }

        [Test]
        public void Map_ShouldMapTheLengthCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.length.Should().Be((decimal) 3.5);

        }

        [Test]
        public void Map_ShouldMapTheHeightCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage = (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Height = (decimal) 3.25,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.height.Should().Be((decimal)3.25);

        }

        [Test]
        public void Map_ShouldMapThePolishCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.polish.Should().Be(3);

        }

        [Test]
        public void Map_ShouldMapTheReportCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.report.Should().Be(2);

        }

        [Test]
        public void Map_ShouldMapTheShapeCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.shape.Should().Be(1);

        }

        [Test]
        public void Map_ShouldMapTheSymmetryCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.symmetrical.Should().Be(1);

        }

        [Test]
        public void Map_ShouldMapTheTableCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.table.Should().Be((decimal) 54.5);

        }

        [Test]
        public void Map_ShouldMapTheWeightCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.weight.Should().Be((decimal)2.41);

        }

        [Test]
        public void Map_ShouldMapTheWidthCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.width.Should().Be((decimal)6.35);

        }

        [Test]
        public void Map_ShouldMapReportNumber()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "SI2",
                Culet = "",
                Color = "H",
                Cut = "VG",
                DepthPresentage = (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "MB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "G",
                Price = 9999,
                Report = "EGL IL",
                ReportURL = "",
                Shape = "BR",
                Symmetry = "EX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35,
                ReportNumber = "123"
                

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.report_number.Should().Be("123");

        }


        [Test]
        public void Map_ShouldMapDefaultCorrecty()
        {
            //Arrange
            var mapper = new Mapper<INV_DIAMONDS_INVENTORY>(Igal.ConversionDictionary);
            var igalDiamond = new Igal()
            {
                Clarity = "mSI2",
                Culet = "",
                Color = "mH",
                Cut = "mVG",
                DepthPresentage =  (decimal)4.25,
                InventoryCode = "12345",
                Fluorescence = "mMB",
                Girdle = "",
                Length = (decimal)3.5,
                Polish = "mG",
                Price = 9999,
                Report = "mEGL IL",
                ReportURL = "",
                Shape = "mBR",
                Symmetry = "mEX",
                Table = (decimal)54.5,
                Weight = (decimal)2.41,
                Width = (decimal)6.35

            };
            //Act
            var inv_entry = mapper.Map(igalDiamond);
            //Assert
            inv_entry.clarity.Should().Be(1);
            inv_entry.color.Should().Be(1);
            inv_entry.cut.Should().Be(1);
            inv_entry.fluorescence.Should().Be(1);
            inv_entry.polish.Should().Be(1);
            inv_entry.report.Should().Be(1);
            inv_entry.shape.Should().Be(1);
            inv_entry.symmetrical.Should().Be(1);

        }
    }
}