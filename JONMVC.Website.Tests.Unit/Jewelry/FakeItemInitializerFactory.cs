using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Tests.Unit.Jewelry
{
    class FakeItemInitializerFactory
    {
        public static ItemInitializerParameterObject ItemInitializerParameterObject = new ItemInitializerParameterObject()
        {
            ID = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
            ItemNumber = "0101-15421",
            JewelryCategory = "category",
            JewelryCategoryID = 2,
            JewelrySubCategory = "subcategory",
            JewelrySubCategoryID = 7,
            Metal = "Platinum",
            Price = (decimal) 9999.99,
            Title = "title",
            Weight = 10.50,
            Width = 5
        };

        public static JewelryExtraInitializerParameterObject JewelryExtraInitializerParameterObject = new JewelryExtraInitializerParameterObject()
        {
            
                                         CS_Clarity = "IF",
                                         CS_ClarityFreeText = "CSFreeClarity",
                                         CS_Color = "F",
                                         CS_ColorFreeText = "CSFreeColor",
                                         CS_Count = 1,
                                         CS_Cut = "Round",
                                         CS_Description = "CSDescription",
                                         CS_Type = "Diamond",
                                         CS_Weight = 2,
                                         HasSideStones = true,
                                         TotalWeight = 3.5,
                                         SS_Clarity = "VVS1",
                                         SS_ClarityFreeText = "SSFreeClarity",
                                         SS_Color = "H",
                                         SS_ColorFreeText = "SSFreeColor",
                                         SS_Count = 8,
                                         SS_Cut = "Princess",
                                         SS_Description = "SSDescription",
                                         SS_Type = "Diamond",
                                         SS_Weight = 1.5
                                     
        };


    }
}
