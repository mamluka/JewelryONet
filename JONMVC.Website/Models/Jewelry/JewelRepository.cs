using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Utils;
namespace JONMVC.Website.Models.Jewelry
{
    public class JewelRepository : IJewelRepository
    {
        protected  ISettingManager settingManager;


        protected bool HasFilter = false;

        protected DynamicSQLWhereObject filter;

        protected JewelryDynamicOrderBy orderBy;
        protected int itemsPerPage;
        protected int page;

        protected int currentPage;

        protected int totalItems;
        protected JewelMediaType requestedJewelMediaTypeByUser;

        public JewelRepository(ISettingManager settingManager)
        {
            this.settingManager = settingManager;

            //defaults
            itemsPerPage = 21;
            page = 1;
            orderBy = new JewelryDynamicOrderBy("price","desc");
            currentPage = 1;
            requestedJewelMediaTypeByUser = JewelMediaType.All;
        }

        public int CurrentPage
        {
            get {
                return currentPage;
            }
        }

        public int TotalItems
        {
            get {
                return totalItems;
            }
        }

        public virtual List<Jewel> GetJewelsByDynamicSQL(DynamicSQLWhereObject dynamicSQL)
        {
            var jewelrylist = new List<Jewel>();



            using (var db = new JONEntities())
            {

                var items = db.v_jewel_items.Where(dynamicSQL.Pattern, dynamicSQL.Valuelist.ToArray());


                if (HasFilter)
                {
                    items = items.Where(filter.Pattern, filter.Valuelist.ToArray());
                }

                items = items.Where(ParseMetalFilter(requestedJewelMediaTypeByUser));

                //must call orderby before skip
                items = items.OrderBy(ParseOrderBy());


                totalItems = items.ToList().Count;

                items = items.Skip(itemsPerPage*(page - 1)).Take(itemsPerPage);



                foreach (var item in items)
                {
                    var jewel = JewelClassFactory(item);

                    jewelrylist.Add(jewel);
                }

                currentPage = page;

            }

            return jewelrylist;
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

        public virtual Jewel GetJewelByID(int jewelryID)
        {
            using (var db = new JONEntities())
            {
                var item = GetJewelItemsObjectSet().Where(t => t.id == jewelryID).Where(ParseMetalFilter(requestedJewelMediaTypeByUser)).SingleOrDefault();

                if (item != null)
                {
                    return JewelClassFactory(item);
                    
                }

                return null;
            }
        }

        protected Jewel JewelClassFactory(v_jewel_items item)
        {
            var initObj = new ItemInitializerParameterObject()
            {
                ID = item.id,
                ItemNumber = item.ITEMNUMBER,
                JewelryCategory = item.jeweltype,
                JewelryCategoryID = item.JEWELTYPE_ID,
                JewelrySubCategory = item.jewelsubtype,
                JewelrySubCategoryID = item.JEWELSUBTYPE_ID,
                SpecialPrice = item.SPECIAL_SELL_PRICE ?? 0,
                DealerPrice = item.DEALER_PRICE ?? 0,
                OnSpecial = item.ONSPECIAL ?? false,
                RegularPrice = item.price ?? 0,
                Metal = item.metal,
                Title = !String.IsNullOrWhiteSpace(item.jeweltitle) ? item.jeweltitle : "Temp Title",
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

            var jewelryExtra = new JewelryExtra(initJewelExtra);


            initObj.Weight = Convert.ToDouble(item.WEIGHT);

            double tryParseJewelWidth;
            if (double.TryParse(item.ITEM_SIZE.Trim(), out tryParseJewelWidth))
            {
                initObj.Width = tryParseJewelWidth;
            }
            else
            {
                initObj.Width = 0;
            }

            initObj.Price = DecideWhichPriceToUseAsCurrent(initObj);


            var currrentJewelMediaType = WhichMediaDoesThisJewelHas(item.HAS_YELLOW_GOLD ?? false, item.HAS_WHITE_GOLD ?? false);

            var metal = new Metal(requestedJewelMediaTypeByUser, currrentJewelMediaType);

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

        public void OrderJewelryItemsBy(JewelryDynamicOrderBy orderBy)
        {
            if (orderBy != null)
            {
                this.orderBy = orderBy;
            }
        }

        public void FilterJewelryBy(DynamicSQLWhereObject dynamicFilter)
        {
            if (dynamicFilter != null && dynamicFilter.Pattern!="AnyMatch")
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
    }
}