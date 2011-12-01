using JON.BackOffice.ImportDiamondCSV.Core.DB;
using JON.BackOffice.ImportDiamondCSV.Core.Suppliers;

namespace JON.BackOffice.ImportDiamondCSV.Core
{
    public interface IMapper<T> where T : IDiamond, new()
    {
        IDiamond Map(ISupplier supplierDiamond);
    }
}