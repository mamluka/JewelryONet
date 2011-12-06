using System.Collections.Generic;
using System.IO;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;

namespace JON.BackOffice.ImportDiamondCSV.Core
{
    public interface ICSVParser
    {
        IEnumerable<T> Parse<T>(string fileName) where T : class,ISupplier;
        //TODO add test for this
        IEnumerable<T> Parse<T>(Stream stream) where T : class,ISupplier;
    }
}