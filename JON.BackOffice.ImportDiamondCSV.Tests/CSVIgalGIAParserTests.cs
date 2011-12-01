using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;


namespace JON.BackOffice.ImportDiamondCSV.Tests
{
    [TestFixture]
    public class CSVIgalGIAGIAParserTests
    {

        [Test]
        public void Parse_ShouldOutputTheRightNumberOfLines()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            list.Should().HaveCount(1);
        }


        [Test]
        public void Parse_ShouldOutputTheInventoryCodeRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].InventoryCode.Should().Be("MK2-0087");
        }

        [Test]
        public void Parse_ShouldOutputTheWeightRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Weight.Should().Be((decimal)0.3);
        }

        [Test]
        public void Parse_ShouldOutputTheShapeRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Shape.Trim().Should().Be("RBC");
        }

        [Test]
        public void Parse_ShouldOutputTheColorRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Color.Trim().Should().Be("D");
        }

        [Test]
        public void Parse_ShouldOutputTheClarityRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Clarity.Trim().Should().Be("SI1");
        }

        [Test]
        public void Parse_ShouldOutputThePriceRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Price.Should().Be(495);
        }

       

        [Test]
        public void Parse_ShouldOutputTheWidthRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Width.Should().Be(0);
        }

        [Test]
        public void Parse_ShouldOutputTheLengthRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Length.Should().Be(0);
        }

        [Test]
        public void Parse_ShouldOutputTheHeightRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Height.Should().Be(0);
        }

        [Test]
        public void Parse_ShouldOutputTheDepthPresentageRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].DepthPresentage.Should().Be((decimal)62.5);
        }


        [Test]
        public void Parse_ShouldOutputTheTableRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Table.Should().Be((decimal)54);
        }

        [Test]
        public void Parse_ShouldOutputTheGirdleRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Girdle.Trim().Should().Be("THN TO STK");
        }

        [Test]
        public void Parse_ShouldOutputTheCuletRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Culet.Trim().Should().Be("NON");
        }

        [Test]
        public void Parse_ShouldOutputThePolishRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Polish.Trim().Should().Be("EX");
        }

        [Test]
        public void Parse_ShouldOutputTheSymmetryRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Symmetry.Trim().Should().Be("VG");
        }

        [Test]
        public void Parse_ShouldOutputTheFluorescenceRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Fluorescence.Trim().Should().Be("FNT");
        }

        [Test]
        public void Parse_ShouldOutputTheCutRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Cut.Trim().Should().Be("EX");
        }

        [Test]
        public void Parse_ShouldOutputTheReportToAlwaysBeGIA()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Report.Trim().Should().Be("GIA");
        }


        [Test]
        public void Parse_ShouldOutputTheMeasurmentsFromLengthNonStandartArray()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<IgalGIA>(fileName);
            //Assert
            var reallist = list.ToList();
            reallist[0].MeasurmentsFromLengthNonStandart.Should().HaveCount(3);
            reallist[0].MeasurmentsFromLengthNonStandart[0].Should().Be((decimal) 4.30);
            reallist[0].MeasurmentsFromLengthNonStandart[1].Should().Be((decimal) 4.32);
            reallist[0].MeasurmentsFromLengthNonStandart[2].Should().Be((decimal) 2.69);
        }

        //


        [Test]
        public void ConvertFrom_ShouldConvertToAList()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);

            //Act

            var list = parser.Parse<IgalGIA>(fileName);

            var reallist = list.ToList();

            //Assert
            
            
            var composed = reallist[0].SupplierCode.ToString() + reallist[0].InventoryCode;
            var uniqueDiamondIdFromHashCode = Math.Abs(composed.GetHashCode());

            reallist[0].DiamondID.Should().Be(uniqueDiamondIdFromHashCode);


        }

        [Test]
        public void ConvertFrom_ShouldConvertMoneyFormatToDecimal()
        {
            //Arrange
            var convertor = new FromCurrecntyFormatToDecimalConverter();

            var length = "1,545";

            //Act
            var price = (decimal)convertor.ConvertFrom(null, new CultureInfo(1), length);
            //Assert
            price.Should().Be(1545);
        }

        [Test]
        public void ConvertFrom_ShouldConvertMoneyFormatToDecimalWhenInputIsEmptyAndReturn0()
        {
            //Arrange
            var convertor = new FromCurrecntyFormatToDecimalConverter();

            var length = "";

            //Act
            var price = (decimal)convertor.ConvertFrom(null, new CultureInfo(1), length);
            //Assert
            price.Should().Be(0);
        }

        [Test]
        public void ConvertFrom_ShouldConvertDimentionStringToDecimal()
        {
            //Arrange
            var convertor = new IgalMeasurmentPartConvertor();

            var decimalString = "4.25";

            //Act
            var dimentionDecimal = (decimal)convertor.ConvertFrom(null, new CultureInfo(1), decimalString);
            //Assert
            dimentionDecimal.Should().Be((decimal) 4.25);
        }


        [Test]
        public void ConvertFrom_ShouldCreateAUniqueDiamondIdBasedOnSupplierAndInventoryID()
        {
            //Arrange
            var convertor = new FromCurrecntyFormatToDecimalConverter();

            var length = "";

            //Act
            var price = (decimal)convertor.ConvertFrom(null, new CultureInfo(1), length);
            //Assert
            price.Should().Be(0);
        }

        private IFileSystem FakeCSVFileSystemWith1SampleLine(string fileName)
        {
            var fileSystem = MockRepository.GenerateStub<IFileSystem>();

            MemoryStream stream = GetDataStream();

            fileSystem.Stub(x => x.File.Open(Arg<string>.Is.Equal(fileName), Arg<FileMode>.Is.Equal(FileMode.OpenOrCreate))).
                Repeat.Once().Return(stream);
            return fileSystem;
        }

        public MemoryStream GetDataStream()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);


            writer.WriteLine(",SERIA,SHAPE,WEIGHT,COL,CLARITY,CUT,POL,SYM,FLUORE,DEPTH,TABLE,GIRDLE,FACET,CULET,LENGTH,WIDTH,MDEPTH,GIA NO,LIST");
            writer.WriteLine("1,MK2-0087,RBC,0.30,D,SI1,EX,EX,VG,FNT,62.5,54,THN TO STK,Faceted,NON,4.30-4.32*2.69,/,/,2126385348,\" 2,200 \",25%, 495");
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

    }
}