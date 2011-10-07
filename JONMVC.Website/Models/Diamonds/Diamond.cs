namespace JONMVC.Website.Models.Diamonds
{
    public class Diamond:IDiamond    
    {
        public int DiamondID { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public decimal Price { get; set; }
        public string Report { get; set; }
        public string ReportURL { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public decimal Depth { get; set; }
        public decimal Table { get; set; }
        public string Girdle { get; set; }
        public string Culet { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public string Cut { get; set; }
        public string ReportNumber { get; set; }
        public string Description { get; set; }
    }
}