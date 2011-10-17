namespace JONMVC.Website.Models.Jewelry
{
    public class ItemInitializerParameterObject
    {
        
        public decimal Price { get; set; }
        public int ID { get; set; }
        public string ItemNumber { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int JewelryCategoryID { get; set; }
        public int JewelrySubCategoryID { get; set; }
        public string JewelryCategory { get; set; }
        public string JewelrySubCategory { get; set; }
        public string Title { get; set; }
        public double Weight { get; set; }
        public string Metal { get; set; }
        public double Width { get; set; }
        public bool OnBargain { get; set; }

        public decimal SpecialPrice { get; set; }

        public decimal DealerPrice { get; set; }

        public decimal RegularPrice { get; set; }

        public bool OnSpecial { get; set; }
    }
}