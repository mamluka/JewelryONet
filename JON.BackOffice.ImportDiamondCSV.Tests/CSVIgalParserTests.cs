using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Linq;
using JON.BackOffice.ImportDiamondCSV.Core;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace JON.BackOffice.ImportDiamondCSV.Tests
{
    [TestFixture]
    public class CSVIgalParserTests
    {

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void Parse_ShouldOutputTheRightNumberOfLines()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);
            
            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            list.Should().HaveCount(1);
        }


        [Test]
        public void Parse_ShouldOutputTheDiamondIDRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].InventoryCode.Should().Be("DY-029");
        }

        [Test]
        public void Parse_ShouldOutputTheWeightRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Weight.Should().Be((decimal) 1.16);
        }

        [Test]
        public void Parse_ShouldOutputTheShapeRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Shape.Trim().Should().Be("BR");
        }

        [Test]
        public void Parse_ShouldOutputTheColorRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Color.Trim().Should().Be("H");
        }

        [Test]
        public void Parse_ShouldOutputTheClarityRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Clarity.Trim().Should().Be("SI2");
        }

        [Test]
        public void Parse_ShouldOutputThePriceRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Price.Should().Be(2867);
        }

        [Test]
        public void Parse_ShouldOutputTheReportRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Report.Trim().Should().Be("EGL IL");
        }

        [Test]
        public void Parse_ShouldOutputTheWidthRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Width.Should().Be((decimal) 4.3);
        }

        [Test]
        public void Parse_ShouldOutputTheLengthRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Length.Should().Be((decimal)5.24);
        }

        [Test]
        public void Parse_ShouldOutputTheHeightRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Height.Should().Be((decimal)2.32);
        }

        [Test]
        public void Parse_ShouldOutputTheDepthPresentageRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].DepthPresentage.Should().Be((decimal)65.8);
        }


        [Test]
        public void Parse_ShouldOutputTheTableRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Table.Should().Be((decimal) 58.5);
        }

        [Test]
        public void Parse_ShouldOutputTheGirdleRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Girdle.Trim().Should().Be("");
        }

        [Test]
        public void Parse_ShouldOutputTheCuletRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Culet.Trim().Should().Be("");
        }

        [Test]
        public void Parse_ShouldOutputThePolishRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Polish.Trim().Should().Be("VG");
        }

        [Test]
        public void Parse_ShouldOutputTheSymmetryRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
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

            var parser = new CSVParser( fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Fluorescence.Trim().Should().Be("NIL");
        }

        [Test]
        public void Parse_ShouldOutputTheCutRight()
        {
            //Arrange
            var fileName = @"c:\csv.csv";

            var fileSystem = FakeCSVFileSystemWith1SampleLine(fileName);

            var parser = new CSVParser(fileSystem);
            //Act
            var list = parser.Parse<Igal>(fileName);
            //Assert
            var reallist = list.ToList();

            reallist[0].Cut.Trim().Should().Be("VG");
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


            writer.WriteLine("stock #,shape,weight,color,clarity,rapnet price,rap-price,rap%,lab,certificate #,measurements,depth %,table %,girdle,culet,polish,sym,fluorescence intensity,comment,rapcode,pair,pairsep,Cut Grade,Crown Angle,Crown Height,Pavilion Angle,Pavilion Depth,Fancy Color,Fancy Color Intensity,Certificate filename");
            writer.WriteLine("DY    -029   ,BR    ,1.16,H        ,SI2      ,2867,6100,-0.53,EGL IL  ,            , 4.30* 5.24* 2.32,65.8,58.5,               ,          ,VG        ,VG        ,NIL                 , , ,             , ,VG        ,0,0,0,0,         ,          ,");
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        

    }
}