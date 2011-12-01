namespace JON.BackOffice.ImportDiamondCSV.Core.Suppliers
{
    public interface ISupplier
    {

        int DiamondID { get; }

        int SupplierCode { get; }

        string InventoryCode { get; set; }

        string Shape { get; set; }

        decimal Weight { get; set; }

        string Color { get; set; }

        string Clarity { get; set; }

        decimal Price { get; set; }

        string Report { get; set; }

        string ReportURL { get; set; }

        string ReportNumber { get; set; }

        decimal Width { get; set; }

        decimal Length { get; set; }

        decimal Height { get; set; }

        decimal DepthPresentage { get; set; }

        decimal Table { get; set; }

        string Girdle { get; set; }

        string Culet { get; set; }

        string Polish { get; set; }

        string Symmetry { get; set; }

        string Fluorescence { get; set; }

        string Cut { get; set; }

        void ExecuteBeforeMapping();

        PricePolicy SupplierPricePolicy { get; }

    }
}