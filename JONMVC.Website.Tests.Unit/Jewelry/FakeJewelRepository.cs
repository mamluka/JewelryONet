using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.Tests.Unit.Utils;
using JONMVC.Website.Models.DB;
namespace JONMVC.Website.Tests.Unit.Jewelry
{
    public class FakeJewelRepository:IJewelRepository
    {
        private ISettingManager settingManager;


        private bool HasFilter = false;

        private DynamicSQLWhereObject filter;

        private DynamicOrderBy orderBy;
        private  int itemsPerPage;
        private  int page;

        private int currentPage;

        private int totalItems;
        private  JewelMediaType requestedJewelMediaTypeByUser;
        
       public FakeJewelRepository(ISettingManager settingManager)
       {
           this.settingManager = settingManager;

           //defaults
           itemsPerPage = 21;
           page = 1;
           orderBy = new DynamicOrderBy("price", "desc");
           currentPage = 1;
           requestedJewelMediaTypeByUser = JewelMediaType.All;

       }

       public Jewel GetJewelByID(int jewelryID)
       {
           var item = dbmock.Where(t => t.id == jewelryID).Where(ParseMetalFilter(requestedJewelMediaTypeByUser)).SingleOrDefault();

           if (item != null)
           {
               return JewelClassFactory(item);

           }

           return null;
       }

       public List<Jewel> GetJewelsByDynamicSQL(DynamicSQLWhereObject dynamicSQL)
       {

           var list = new List<Jewel>();

           var items = dbmock.Where(dynamicSQL.Pattern, dynamicSQL.Valuelist.ToArray());

           if (HasFilter)
           {
               items = items.Where(filter.Pattern, filter.Valuelist.ToArray());
           }

           items = items.Where(ParseMetalFilter(requestedJewelMediaTypeByUser));

           //must call orderby before skip
           items = items.OrderBy(ParseOrderBy());


           totalItems = items.ToList().Count;

           items = items.Skip(itemsPerPage * (page - 1)).Take(itemsPerPage);


           foreach (var jewel in items)
           {
               list.Add(JewelClassFactory(jewel));    
           }
           

           return list;
       }


       public int CurrentPage
       {
           get
           {
               return currentPage;
           }
       }

       public int TotalItems
       {
           get
           {
               return totalItems;
           }
       }

       protected string ParseMetalFilter(JewelMediaType jewelTypeFilter)
       {
           var metalTypeFilterDictionary = new Dictionary<JewelMediaType, string>()
                                                                        {
                                                                            {JewelMediaType.All,/*"has_platinum=true*/"has_yellow_gold=true or has_white_gold=true"},
//                                                                            {MetalMediaType.Platinum,"has_platinum=true"},
                                                                            {JewelMediaType.YellowGold,"has_yellow_gold=true"},
                                                                            {JewelMediaType.WhiteGold,"has_white_gold=true"}
                                                                        };

           return metalTypeFilterDictionary[jewelTypeFilter];
       }

       protected Jewel JewelClassFactory(v_jewel_items item)
       {
           var initObj = new ItemInitializerParameterObject()
           {
               ID = item.id,
               ItemNumber = item.ITEMNUMBER,
               CategoryID = item.CATEGORY_ID,
               SubCategoryID = item.SUBCATEGORY_ID,
               JewelryCategory = item.jeweltype,
               JewelryCategoryID = item.JEWELTYPE_ID,
               JewelrySubCategory = item.jewelsubtype,
               JewelrySubCategoryID = item.JEWELSUBTYPE_ID,
               SpecialPrice = item.SPECIAL_SELL_PRICE ?? 0,
               DealerPrice = item.DEALER_PRICE ?? 0,
               OnSpecial = item.ONSPECIAL ?? false,
               RegularPrice =  item.price ?? 0,
               Metal = item.metal,
               Title = item.jeweltitle,
               OnBargain = item.ONBARGAIN ?? false
           };


           var initJewelExtra = new JewelryExtraInitializerParameterObject()
           {
               CS_Clarity = item.clarity,
               CS_ClarityFreeText = item.clarity_freetxt,
               CS_Color = item.color,
               CS_ColorFreeText = item.color_freetxt,
               CS_Count = item.cs_count ?? 0,
               CS_Cut = item.cs_cut,
               CS_Description = item.cs_desc,
               CS_Type = item.cs_type,
               HasSideStones = item.has_sidestones ?? false,
               SS_Clarity = item.ss_clarity,
               SS_ClarityFreeText = "",
               SS_Color = item.ss_color,
               SS_ColorFreeText = "",
               SS_Count = item.ss_count ?? 0,
               SS_Cut = item.ss_cut,
               SS_Description = item.ss_desc,
               SS_Type = item.ss_type
           };

           initJewelExtra.TotalWeight = Convert.ToDouble(item.total_weight ?? 0);
           initJewelExtra.CS_Weight = Convert.ToDouble(item.cs_weight ?? 0);
           initJewelExtra.SS_Weight = Convert.ToDouble(item.ss_weight ?? 0);

           var jewelryExtra = new JewelryExtra(initJewelExtra,initObj);

          
           initObj.Weight = Convert.ToDouble(item.WEIGHT);

           double tryParseJewelWidth;
           if (double.TryParse(item.ITEM_SIZE.Trim(), out tryParseJewelWidth))
           {
               initObj.Width = tryParseJewelWidth;
           } else
           {
               initObj.Width = 0;
           }

           initObj.Price = DecideWhichPriceToUseAsCurrent(initObj);
           

           var currrentJewelMediaType = WhichMediaDoesThisJewelHas(item.HAS_YELLOW_GOLD ?? false, item.HAS_WHITE_GOLD ?? false);

           var metal = new Metal(requestedJewelMediaTypeByUser, currrentJewelMediaType, item.metal);

           var mediaFactory = new MediaFactory(initObj.ItemNumber, settingManager);

           mediaFactory.ChangeMediaSet(requestedJewelMediaTypeByUser, currrentJewelMediaType);



           var media = mediaFactory.BuildMedia();



           var jewel = new Jewel(initObj, media, metal, jewelryExtra, currrentJewelMediaType);
           return jewel;
       }

        private decimal DecideWhichPriceToUseAsCurrent(ItemInitializerParameterObject initObj)
        {
            if (initObj.OnSpecial)
            {
                return initObj.SpecialPrice;
            }
            return initObj.RegularPrice;
        }


        protected string ParseOrderBy()
       {
           return string.Format("{0} {1}", orderBy.Field, orderBy.Direction);
       }

       public void OrderJewelryItemsBy(DynamicOrderBy orderBy)
       {
           if (orderBy != null)
           {
               this.orderBy = orderBy;
           }
       }

       public void FilterJewelryBy(DynamicSQLWhereObject dynamicFilter)
       {
           if (dynamicFilter != null && dynamicFilter.Pattern != "AnyMatch")
           {
               HasFilter = true;
               filter = dynamicFilter;
           }
       }


       public void ItemsPerPage(int itemsperpage)
       {
           itemsPerPage = itemsperpage;
       }

       public void Page(int currentpage)
       {
           page = currentpage;
       }


       public void FilterMediaByMetal(JewelMediaType jewelMediaType)
       {
           requestedJewelMediaTypeByUser = jewelMediaType;
       }

        public void AddFilter(DynamicSQLWhereObject filter)
        {
            throw new NotImplementedException();
        }

        public void AddFilterList(List<DynamicSQLWhereObject> filters)
        {
            throw new NotImplementedException();
        }

        protected ObjectSet<v_jewel_items> GetJewelItemsObjectSet()
       {
           return new JONEntities().v_jewel_items;
       }

       protected JewelMediaType WhichMediaDoesThisJewelHas(bool hasYellowGold, bool hasWhiteGold)
       {
           if (hasWhiteGold && hasYellowGold)
           {
               return JewelMediaType.All;
           }
           if (hasYellowGold)
           {
               return JewelMediaType.YellowGold;
           }
           return JewelMediaType.WhiteGold;
       }

       private IQueryable<v_jewel_items> dbmock = new List<v_jewel_items>()
                                                 {
                                                     {new v_jewel_items()
                                                            {
                                                                id = Tests.FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID,
                                                                ITEMNUMBER = "0101-15421",
                                                                jeweltype = "category",
                                                                JEWELTYPE_ID = 2,
                                                                jewelsubtype = "subcategory",
                                                                JEWELSUBTYPE_ID = 7,
                                                                CATEGORY_ID = 7,
                                                                metal = "White Gold 18 Karat",
                                                                jeweltitle = "title",
                                                                price = (decimal?) 9999.99,
                                                                WEIGHT = (decimal?) 10.50,
                                                                ITEM_SIZE = "5",
                                                                color = "F",
                                                                clarity_freetxt = "CSFreeClarity",
                                                                clarity = "VS1",
                                                                color_freetxt = "CSFreeColor",
                                                                cs_count = 1,
                                                                cs_cut = "Round",
                                                                cs_desc = "CSDescription",
                                                                cs_type = "Diamond",
                                                                cs_weight = 2,
                                                                has_sidestones = true,
                                                                total_weight = (decimal?) 3.5,
                                                                ss_clarity = "VVS1",
                                                                ss_clarity_free = "SSFreeClarity",
                                                                ss_color = "H",
                                                                ss_color_free = "SSFreeColor",
                                                                ss_count = 8,
                                                                ss_cut = "Princess",
                                                                ss_desc = "SSDescription",
                                                                ss_type = "Diamond",
                                                                ss_weight = (decimal?) 1.5,
                                                                HAS_WHITE_GOLD = true,
                                                                HAS_YELLOW_GOLD = true,
                                                                DEALER_PRICE = 7000,
                                                                SPECIAL_SELL_PRICE = 8000,
                                                                ONSPECIAL = false
                                                                
                                

                                

                                
                                                            }},
                                                            {new v_jewel_items()
                                                            {
                                                                id = 1112,
                                                                ITEMNUMBER = "0101-15422",
                                                                jeweltype = "category",
                                                                JEWELTYPE_ID = 2,
                                                                jewelsubtype = "subcategory",
                                                                JEWELSUBTYPE_ID = 7,
                                                                CATEGORY_ID = 7,
                                                                metal = "White Gold 18 Karat",
                                                                jeweltitle = "title",
                                                                price = (decimal?) 9999.99,
                                                                WEIGHT = 0,
                                                                ITEM_SIZE = "",
                                                                color = "F",
                                                                clarity_freetxt = "CSFreeClarity",
                                                                clarity = "IF",
                                                                color_freetxt = "CSFreeColor",
                                                                cs_count = 1,
                                                                cs_cut = "Round",
                                                                cs_desc = "CSDescription",
                                                                cs_type = "Diamond",
                                                                cs_weight = 2,
                                                                has_sidestones = true,
                                                                total_weight = (decimal?) 3.5,
                                                                ss_clarity = "VVS1",
                                                                ss_clarity_free = "SSFreeClarity",
                                                                ss_color = "H",
                                                                ss_color_free = "SSFreeColor",
                                                                ss_count = 8,
                                                                ss_cut = "Princess",
                                                                ss_desc = "SSDescription",
                                                                ss_type = "Diamond",
                                                                ss_weight = (decimal?) 1.5,
                                                                HAS_WHITE_GOLD = false,
                                                                HAS_YELLOW_GOLD = true,
                                                                ONBARGAIN = true,
                                                                ONSPECIAL = true,
                                                                SPECIAL_SELL_PRICE = 8000
                                                                
                                                                
                                

                                

                                
                                                            }},
                                                            {new v_jewel_items()
                                                            {
                                                                id = 1113,
                                                                ITEMNUMBER = "0101-15423",
                                                                jeweltype = "category",
                                                                JEWELTYPE_ID = 2,
                                                                jewelsubtype = "subcategory",
                                                                JEWELSUBTYPE_ID = 7,
                                                                CATEGORY_ID = 8,
                                                                metal = "White Gold 18 Karat",
                                                                jeweltitle = "title",
                                                                price = (decimal?) 9999.99,
                                                                WEIGHT = (decimal?) 10.50,
                                                                ITEM_SIZE = "5",
                                                                color = "Ocean Blue",
                                                                clarity_freetxt = "CSFreeClarity",
                                                                clarity = "Eye Clean",
                                                                color_freetxt = "CSFreeColor",
                                                                cs_count = 1,
                                                                cs_cut = "Round",
                                                                cs_desc = "CSDescription",
                                                                cs_type = "Diamond",
                                                                cs_weight = 2,
                                                                has_sidestones = true,
                                                                total_weight = (decimal?) 3.5,
                                                                ss_clarity = "VVS1",
                                                                ss_clarity_free = "SSFreeClarity",
                                                                ss_color = "H",
                                                                ss_color_free = "SSFreeColor",
                                                                ss_count = 8,
                                                                ss_cut = "Princess",
                                                                ss_desc = "SSDescription",
                                                                ss_type = "Diamond",
                                                                ss_weight = (decimal?) 1.5,
                                                                HAS_WHITE_GOLD = true,
                                                                HAS_YELLOW_GOLD = false
                                

                                

                                
                                                            }}
                                                 }.AsQueryable();

    }
}
