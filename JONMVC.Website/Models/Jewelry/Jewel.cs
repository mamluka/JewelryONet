using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Jewelry
{
    public class Jewel
    {
       
        public int ID { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public Media Media { get; private set; }
        public JewelMediaType MediaSetsOwnedByJewel { get; private set; }
        public JewelryExtra JewelryExtra { get; private set; }
        

        public string ItemNumber { get; private set; }

        public decimal Price { get; private set; }
        public decimal SpecialPrice { get; private set; }
        public decimal DealerPrice { get; private set; }

        public string Title { get; private set; }

        public double Weight { get; private set; }

        public double Width { get; private set; }

        private IMetal Metal { get; set; }

        public bool IsBestOffer { get; private set; }
        public bool IsSpecial { get; private set; }

        public decimal RegularPrice { get; internal set; }

        #region Public


        public Jewel(ItemInitializerParameterObject itemInitializerParameterObject, Media media, Metal metal, JewelryExtra extra,JewelMediaType jewelMediaSets)
        {
            Media = media;
            ID = itemInitializerParameterObject.ID;

            Category = itemInitializerParameterObject.JewelryCategory;
            SubCategory = itemInitializerParameterObject.JewelrySubCategory;
            CategoryID = itemInitializerParameterObject.JewelryCategoryID;
            SubCategoryID = itemInitializerParameterObject.JewelrySubCategoryID;

            ItemNumber = itemInitializerParameterObject.ItemNumber;

            Metal = metal;

            Price = itemInitializerParameterObject.Price;
            SpecialPrice = itemInitializerParameterObject.SpecialPrice;
            DealerPrice = itemInitializerParameterObject.DealerPrice;
            RegularPrice = itemInitializerParameterObject.RegularPrice;
            Title = itemInitializerParameterObject.Title;
            Weight = itemInitializerParameterObject.Weight;

            Width = itemInitializerParameterObject.Width;

            IsBestOffer = itemInitializerParameterObject.OnBargain;
            IsSpecial = itemInitializerParameterObject.OnSpecial;

            JewelryExtra = extra;

            

            MediaSetsOwnedByJewel = jewelMediaSets;

        }

        
       public string MetalFullName()
        {
            return Metal.GetFullName();
        }


        public override bool Equals(object obj)
        {

            var jewel2 = (Jewel) obj;
            if (obj.GetType().Name != "Jewel")
            {
                return false;
            }

            if (!(Category == jewel2.Category && SubCategory == jewel2.SubCategory && CategoryID == jewel2.CategoryID && SubCategoryID == jewel2.SubCategoryID))
            {
                return false;
            }

            return true;

        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
        #endregion

       

       
    }
}
