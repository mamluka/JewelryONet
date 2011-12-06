using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;

namespace JON.BackOffice.ImportDiamondCSV.Core
{
    public class CSVParser : ICSVParser
    {
        private readonly IFileSystem fileSystem;

        public CSVParser(IFileSystem fileSystem)
        {

            this.fileSystem = fileSystem;
        }

        public IEnumerable<T> Parse<T>(string fileName) where T : class,ISupplier 
        {
            var csv = new CsvHelper.CsvHelper(fileSystem.File.Open(fileName,FileMode.OpenOrCreate));

            var list = csv.Reader.GetRecords<T>();

            return list;
        }

        public IEnumerable<T> Parse<T>(Stream stream) where T : class, ISupplier
        {
            throw new System.NotImplementedException();
        }

//        public T ParseFirst<T>() where T : ISupplier
//        {
//            var csv = new CsvHelper.CsvHelper(fileSystem.File.Open(fileName, FileMode.OpenOrCreate));
//            T record = default(T);
//            for (int i = 0; i < 25; i++)
//            {
//                csv.Reader.Read();
//                record = csv.Reader.GetRecord<T>();
//            }
//
//            return record;
//        }

//        public T ParseField<T>(int index) 
//        {
//            var csv = new CsvHelper.CsvHelper(fileSystem.File.Open(fileName, FileMode.OpenOrCreate));
//
//            csv.Reader.Read();
//
//            var field = csv.Reader.GetField<T>(index, new IgalDiamondIDTypeConvertor());
//            
//
//            return field;
//        }
    }
}