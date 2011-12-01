using System.Collections.Generic;

namespace JON.BackOffice.ImportDiamondCSV.Core.DB
{
    public interface IDatabasePersistence
    {
        void SaveOrUpdate();
        void AddSupplierDiamondList(List<IDiamond> list);
    }
}