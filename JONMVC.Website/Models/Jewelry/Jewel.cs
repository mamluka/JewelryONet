using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Jewelry
{
    public class Jewel
    {
       
        public int ID { get; set; }
        public string JewelCategory { get; set; }
        public string JewelSubCategory { get; set; }
        public int JewelCategoryID { get; set; }
        public int JewelSubCategoryID { get; set; }

        public Media Media { get; private set; }
        public JewelMediaType MediaSetsOwnedByJewel { get; private set; }
        public JewelryExtra JewelryExtra { get; set; }
        

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

        public JewelType Type
        {
            get { return GetJewelType(); }
        }

        #region Public


        public Jewel(ItemInitializerParameterObject itemInitializerParameterObject, Media media, Metal metal, JewelryExtra extra,JewelMediaType jewelMediaSets)
        {
            Media = media;
            ID = itemInitializerParameterObject.ID;

            JewelCategory = itemInitializerParameterObject.JewelryCategory;
            JewelSubCategory = itemInitializerParameterObject.JewelrySubCategory;
            JewelCategoryID = itemInitializerParameterObject.JewelryCategoryID;
            JewelSubCategoryID = itemInitializerParameterObject.JewelrySubCategoryID;
            

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

       private JewelType GetJewelType()
       {
           switch (JewelCategoryID)
           {
               case 2:
                   return JewelType.Ring;
               case 3:
                   return JewelType.Earring;
               case 4:
                   return JewelType.Necklace;
               case 6:
                   return JewelType.Pendant;
               case 8:
                   return JewelType.Bracelet;
               case 10:
                   return JewelType.SemiMounting;
               case 11:
                   return JewelType.Stud;
               default :
                   return JewelType.Other;
           }

       }


        public override bool Equals(object obj)
        {

            var jewel2 = (Jewel) obj;
            if (obj.GetType().Name != "Jewel")
            {
                return false;
            }

            if (!(JewelCategory == jewel2.JewelCategory && JewelSubCategory == jewel2.JewelSubCategory && JewelCategoryID == jewel2.JewelCategoryID && JewelSubCategoryID == jewel2.JewelSubCategoryID))
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
