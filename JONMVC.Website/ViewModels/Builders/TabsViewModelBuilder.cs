using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Jewelry;
using JONMVC.Website.Models.Tabs;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;
using MoreLinq;
using NMoneys;

namespace JONMVC.Website.ViewModels.Builders
{
    public class TabsViewModelBuilder:TabBase
    {


        private readonly ITabsRepository tabsRepository;
        private readonly IJewelRepository jewelRepository;
        private readonly IFileSystem fileSystem;

        private int itemsPerPage;
        private int page;
        private string tabKey;
        private string tabId;
        private MetalFilter JewelMediaTypeFilter;
        private OrderByPriceFilter OrderByPriceFilter;
        private List<JewelInTabContainer> jewelryInTabContainersCollection;
        private List<CustomTabFilterViewModel> CustomFiltersStateList = new List<CustomTabFilterViewModel>();
        private List<ICustomTabFilter> CustomFilters = new List<ICustomTabFilter>();

        public TabsViewModelBuilder(string tabKey, string tabId,XDocument tabsource,ITabsRepository tabsRepository,IJewelRepository jewelRepository,IFileSystem fileSystem)
        {
            this.tabKey = tabKey;
            this.tabId = tabId;

            this.tabsRepository = tabsRepository;
            this.jewelRepository = jewelRepository;
            this.fileSystem = fileSystem;
            this.tabsource = tabsource;

            //defaults
            itemsPerPage = 21;
            page = 1;
            JewelMediaTypeFilter = new MetalFilter(JewelMediaType.WhiteGold);
            OrderByPriceFilter =  new OrderByPriceFilter(OrderByPrice.LowToHigh);

           

        }

        public TabsViewModelBuilder(ITabsViewModel model, XDocument tabsource, ITabsRepository tabsRepository, IJewelRepository jewelRepository,IFileSystem fileSystem)
        {
            tabKey = model.TabKey;
            tabId = model.TabId;

            this.tabsRepository = tabsRepository;
            this.jewelRepository = jewelRepository;
            this.fileSystem = fileSystem;
            this.tabsource = tabsource;

            //extract from model
            itemsPerPage = model.ItemsPerPage;
            page = model.Page;

            JewelMediaTypeFilter = new MetalFilter(model.MetalFilter);
            OrderByPriceFilter = new OrderByPriceFilter(model.OrderByPrice);

            if (model.CustomFilters != null)
            {
                CustomFiltersStateList = model.CustomFilters;    
            }
            
        }
        

        #region Public
        /// <summary>
        /// Use this if you want to build a tab view for objects that implement the interface but extend upon regular tabs
        /// </summary>
        /// <typeparam name="TOut">The Object to Create with tahs are a part of it</typeparam>
        /// <returns></returns>
        public TOut Build<TOut>() where TOut : ITabsViewModel, new()
        {
            return ProcessBuild<TOut>();
        }

        public TabsViewModel Build()
        {
            return ProcessBuild <TabsViewModel>();
        }

        private TOut ProcessBuild<TOut>() where TOut : ITabsViewModel, new()
        {
            var viewModel = new TOut();
            viewModel.TabId = tabId;
            viewModel.TabKey = tabKey;


            viewModel = LoadPage(viewModel);
            viewModel = LoadInTabFilters(viewModel);

            viewModel = LoadJewelryCollection(viewModel);

            //defaults

            viewModel.Page = page;
            viewModel.ItemsPerPage = itemsPerPage;

            viewModel.MetalFilter = JewelMediaTypeFilter.Value;
            viewModel.OrderByPrice = OrderByPriceFilter.Value;

            viewModel.MetalFilterItems = MetalFilter.GetKeyValue();
            viewModel.OrderByPriceItems = OrderByPriceFilter.GetKeyValue();

            return viewModel;
        }

        private TOut LoadInTabFilters<TOut>(TOut viewModel) where TOut:ITabsViewModel
        {
            var tabpage = GetRawTabPageByKey(tabKey);

            var query = tabpage.Elements("tab").Attributes().Where(m => m.Value == tabId).FirstOrDefault();

            if (query == null)
            {
                new IndexOutOfRangeException("bad tab id");
            }

            var tab = query.Parent;

            if (tab.Element("customfilter") != null)
            {
                var filterName = tab.Element("customfilter").Value;
                var filterParams = tab.Element("customfilter").Attribute("params").Value.Split(',');
                var customFilter = CreateCustomFilterFromKey(filterName, filterParams);

                viewModel.CustomFilters.Add(customFilter.ViewModel);
                CustomFilters.Add(customFilter);
            }

            return viewModel;
        }

        private int CalculateTotalPages(int totalItems)
        {
            var pages = 0;
            if (totalItems % itemsPerPage == 0)
            {
                pages = totalItems / itemsPerPage;
            }
            else
            {
                pages = totalItems / itemsPerPage + 1;
            }

            return pages;
        }

        public List<Tab> Tabs(bool showTabs)
        {
            if (IsShowTabs(tabKey))
            {
                var tabs = tabsRepository.GetTabsCollectionByKey(tabKey);
                tabs.Where(t => t.Id == tabId).ForEach(t => t.ActivateTab());
                return tabs;
            }
            return new List<Tab>();
            
            

            
        }

        public List<JewelInTabContainer> JewelryCollection()
        {

            return jewelryInTabContainersCollection;

        }

        #endregion

        #region Private

        public TOut LoadPage<TOut>(TOut viewModel) where TOut:ITabsViewModel
        {
            try
            {
                var qTabpagexml = GetRawTabPageByKey(tabKey);

                string xsprite = "";
                if (qTabpagexml.Element("sprite") != null)
                {
                     xsprite = qTabpagexml.Element("sprite").Value;    
                }

                string xview = "RegularInTabView";
                if (qTabpagexml.Element("view") != null)
                {
                    xview = qTabpagexml.Element("view").Value;
                }


                if (qTabpagexml.Element("extratext") != null)
                {
                    viewModel.ExtraText = qTabpagexml.Element("extratext").Value;
                }

                if (qTabpagexml.Element("shorttitle") != null)
                {
                    viewModel.ShortTitle = qTabpagexml.Element("shorttitle").Value;
                }

                if (qTabpagexml.Element("customfilters") != null)
                {
                    qTabpagexml.Element("customfilters").Value.Split(' ').ToList().ForEach(x=> viewModel.CustomFilters.Add(AssignCustomFilterViewModelByKey(x)));
                    qTabpagexml.Element("customfilters").Value.Split(' ').ToList().ForEach(x => CustomFilters.Add(CreateCustomFilterFromKey(x)));
                }


                

                string xtitle = qTabpagexml.Element("title").Value;

                var xisShowTabs = IsShowTabs(tabKey);

                viewModel.Sprite = xsprite;
                viewModel.InTabPartialView = xview;
                viewModel.Title = xtitle;
                viewModel.IsShowTabs = xisShowTabs;

                viewModel.Tabs = Tabs(xisShowTabs);

                return viewModel;

                //tabpage = new TabPage(title, sprite, tabKey);

                //return tabpage;
            }
            catch (NullReferenceException ex)
            {
               throw new Exception("When trying to load the tab page with the key:" + tabKey + " an error occured:\r\n" + ex.Message);

            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("bad key");
            }


        }

        private CustomTabFilterViewModel AssignCustomFilterViewModelByKey(string filterkey,string[] filterParams = null)
        {
            return CreateCustomFilterFromKey(filterkey,filterParams).ViewModel;
        }

        private ICustomTabFilter CreateCustomFilterFromKey(string key,string[] filterParams = null)
        {
            switch (key)
            {
                case "gemstone-center-stone":
                    return new CustomTabFilterForGemstoneCenterStone(new CustomTabFilterEnumMetadataReader<GemstoneCenterStoneFilterValues>());
                case "subcategory":
                    return new CustomTabFilterForSubCategoryUsingDataBase(filterParams);
                default:
                    throw new Exception("When asked for key:" + key + " an error occured:\r\n no such key");
            }
        }

        private bool IsShowTabs(string tabKey)
        {
            var qTabpagexml = GetRawTabPageByKey(tabKey);
            bool xisShowTabs = true;
            if (qTabpagexml.Attribute("showtabs") != null)
            {
                xisShowTabs = Convert.ToBoolean(qTabpagexml.Attribute("showtabs").Value);
            }
            return xisShowTabs;
        }

        public TOut LoadJewelryCollection<TOut>(TOut viewModel) where TOut:ITabsViewModel
        {
            var dynamicSQLObject = GetDynamicSQLStringBytabKeyAndID(tabKey, tabId);

            jewelRepository.Page(page);
            jewelRepository.ItemsPerPage(itemsPerPage);



            var jewelMediaType = JewelMediaTypeFilter.Value;

            jewelRepository.FilterMediaByMetal(jewelMediaType);
            jewelRepository.OrderJewelryItemsBy(OrderByPriceFilter.DynamicOrderBy());

            if (CustomFiltersStateList.Count > 0)
            {
                var dynamicSQLList =
                    CustomFiltersStateList.Select(x => ConvertCustomFilterStateToDynamicSQL(x)).ToList();

                jewelRepository.AddFilterList(dynamicSQLList);
            }


            var jewelList =  jewelRepository.GetJewelsByDynamicSQL(dynamicSQLObject);

            viewModel.JewelryInTabContainersCollection = MapJewelsToInTabContainers(jewelList);

            var totalItems = jewelRepository.TotalItems;

            viewModel.TotalPages = CalculateTotalPages(totalItems);

            return viewModel;
        }

        private DynamicSQLWhereObject ConvertCustomFilterStateToDynamicSQL(CustomTabFilterViewModel customTabFilterViewModel)
        {
            return CustomFilters.Where(x => x.Key == customTabFilterViewModel.Name).SingleOrDefault().DynamicSQLByFilterValue(customTabFilterViewModel.Value);
        }

        private List<JewelInTabContainer> MapJewelsToInTabContainers(List<Jewel> jewelList)
        {
            var list = new List<JewelInTabContainer>();

            if (jewelList == null)
            {
                return list;
            }

            foreach (var jewel in jewelList)
            {
                var tabContainer = new JewelInTabContainer();
                tabContainer.Description = jewel.Title;
                tabContainer.ID = jewel.ID;

                if (fileSystem.File.Exists(jewel.Media.MovieDiskPathForWebDisplay))
                {
                    tabContainer.HasMovie = true;
                    tabContainer.Movie = jewel.Media.MovieURLForWebDisplay;
                }

                tabContainer.Price = new Money(jewel.Price, Currency.Usd).Format("{1}{0:#,0}");
                tabContainer.Metal = jewel.MetalFullName();

                tabContainer.PictureURL = jewel.Media.IconURLForWebDisplay;
                tabContainer.MediaSet = jewel.Media.MediaSet;
                tabContainer.RegularPrice = new Money(jewel.RegularPrice, Currency.Usd).Format("{1}{0:#,0}");
                tabContainer.OnSpecial = jewel.IsSpecial;
                tabContainer.YouSave = String.Format("{0:0.##}%", Math.Round(100 - (jewel.Price / jewel.RegularPrice) * 100));
                
                list.Add(tabContainer);

            }

            return list;
        }

        #endregion

        #region Helpers

        public DynamicSQLWhereObject GetDynamicSQLStringBytabKeyAndID(string tabKey, string tabId)
        {

            var tabpage = GetRawTabPageByKey(tabKey);

            var query = tabpage.Elements("tab").Attributes().Where(m => m.Value == tabId).FirstOrDefault();

            if (query == null)
            {
                new IndexOutOfRangeException("bad tab id");
            }

            var tab = query.Parent;

            string pattern = tab.Element("where").Element("string").Value;

            var values = from value in tab.Element("where").Element("values").Elements("value")
                         select new
                                    {
                                        Value = value.Value,
                                        Type = value.Attribute("type").Value

                                    };

            var valuelist = new List<object>();

            foreach (var value in values)
            {

                switch (value.Type)
                {
                    case "int":
                        valuelist.Add(Convert.ToInt32(value.Value));
                        break;
                    case "bool":
                        valuelist.Add(Convert.ToBoolean(value.Value));
                        break;
                    default:
                        valuelist.Add(value.Value);
                        break;
                }


            }

            //check value cardenlity

            int exceptedvalues = Regex.Matches(pattern, "@").Count;
            if (exceptedvalues > valuelist.Count)
            {
                throw new ArgumentOutOfRangeException("not enough values given");

            }



            return new DynamicSQLWhereObject(pattern, valuelist);
        }
        
        #endregion

        #region Filters

        public IEnumerable<InMemoryFilterItem> MetalFilterItems()
        {
            List<InMemoryFilterItem> filterItems = new List<InMemoryFilterItem>();

            filterItems.Add(new InMemoryFilterItem(1,"-",null,null));
            filterItems.Add(new InMemoryFilterItem(2, "18k Yellow Gold",null,null));
            filterItems.Add(new InMemoryFilterItem(3, "18K White Gold",null,null));

            return filterItems;



        }  

        public IEnumerable<InMemoryOrderByItem>  OrderByPriceItems()
        {
            List<InMemoryOrderByItem> orderByItems = new List<InMemoryOrderByItem>();

            orderByItems.Add(new InMemoryOrderByItem(1,"High to low","price","desc"));
            orderByItems.Add(new InMemoryOrderByItem(2,"Low to high","price","asc"));

            return orderByItems;
        }



        #endregion

       
    }

    public class MetalFilter
    {
        private readonly JewelMediaType mediaType;

        public JewelMediaType Value
        {
            get { return mediaType; }
        }

        public MetalFilter(JewelMediaType mediaType)
        {
            this.mediaType = mediaType;
        }



        public static List<KeyValuePair<string, string>> GetKeyValue()
        {
            var list = new List<KeyValuePair<string, string>>();
            foreach (JewelMediaType filtername in Enum.GetValues(typeof(JewelMediaType)))
            {
                var keyvalue = new KeyValuePair<string, string>(CustomAttributes.GetDescription(filtername),filtername.ToString());
                list.Add(keyvalue);
            }

            return list;

        }
    }

    public class OrderByPriceFilter
    {
        private readonly OrderByPrice orderByPrice;

        public OrderByPriceFilter(OrderByPrice orderByPrice)
        {
            this.orderByPrice = orderByPrice;
        }

        public OrderByPrice Value
        {
            get {
                return orderByPrice;
            }
        }

        public DynamicOrderBy DynamicOrderBy()
        {
            var field = CustomAttributes.Get<OrderByField>(orderByPrice);
            var direction = CustomAttributes.Get<OrderByDirection>(orderByPrice);
            return new DynamicOrderBy(field,direction);
        }

        public static List<KeyValuePair<string, string>> GetKeyValue()
        {
            var list = new List<KeyValuePair<string, string>>();
            foreach (OrderByPrice filtername in Enum.GetValues(typeof(OrderByPrice)))
            {
                var keyvalue = new KeyValuePair<string, string>(CustomAttributes.GetDescription(filtername), filtername.ToString());
                list.Add(keyvalue);
            }

            return list;

        }
    }
}