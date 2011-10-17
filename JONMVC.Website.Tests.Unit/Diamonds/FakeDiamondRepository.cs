using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Tests.Unit.Diamonds
{
    public class FakeDiamondRepository:IDiamondRepository 
    {
        private readonly IMappingEngine mapper;

        public FakeDiamondRepository(IMappingEngine mapper)
        {
            this.mapper = mapper;
        }

        public List<Diamond> DiamondsBySearchParameters(DiamondSearchParameters mappedSearchParameters)
        {
            if (mappedSearchParameters.PriceFrom > mappedSearchParameters.PriceTo && mappedSearchParameters.PriceTo != 0)
            {
                var temp = mappedSearchParameters.PriceFrom;
                mappedSearchParameters.PriceFrom = mappedSearchParameters.PriceTo;
                mappedSearchParameters.PriceTo = temp;
            }

            if (mappedSearchParameters.WeightFrom > mappedSearchParameters.WeightTo && mappedSearchParameters.WeightTo != 0)
            {
                decimal temp2 = mappedSearchParameters.WeightFrom;
                mappedSearchParameters.WeightFrom = mappedSearchParameters.WeightTo;
                mappedSearchParameters.WeightTo = temp2;
            }

            var dbdiamonds = dbmock
                .Where(cond => MakeEmptyListActLikeAnAllFilter(cond.color, mappedSearchParameters.Color))
                .Where(cond => MakeEmptyListActLikeAnAllFilter(cond.clarity, mappedSearchParameters.Clarity))
                .Where(cond => MakeEmptyListActLikeAnAllFilter(cond.shape, mappedSearchParameters.Shape))
                .Where(cond => MakeEmptyListActLikeAnAllFilter(cond.report, mappedSearchParameters.Report))
                .Where(cond => MakeEmptyListActLikeAnAllFilter(cond.cut, mappedSearchParameters.Cut))
                .Where(x => MakeZeroActAsIgnoreParameter(p => p.totalprice >= mappedSearchParameters.PriceFrom, x, mappedSearchParameters.PriceFrom) && MakeZeroActAsIgnoreParameter(p => p.totalprice <= mappedSearchParameters.PriceTo, x, mappedSearchParameters.PriceTo))
                .Where(x => MakeZeroActAsIgnoreParameter(p => p.weight >= mappedSearchParameters.WeightFrom, x, mappedSearchParameters.WeightFrom) && MakeZeroActAsIgnoreParameter(p => p.weight <= mappedSearchParameters.WeightTo, x, mappedSearchParameters.WeightTo))
                ;


            if (mappedSearchParameters.ItemsPerPage == 0)
            {
                mappedSearchParameters.ItemsPerPage = 10;
            }

            totalRecords = dbdiamonds.Count();
            if (TotalRecords % mappedSearchParameters.ItemsPerPage == 0)
            {
                lastOporationTotalPages = TotalRecords/mappedSearchParameters.ItemsPerPage;
            } else
            {
                lastOporationTotalPages = TotalRecords/mappedSearchParameters.ItemsPerPage + 1;
            }

            if (mappedSearchParameters.OrderBy != null)
            {
                dbdiamonds = dbdiamonds.OrderBy(mappedSearchParameters.OrderBy.SQLString);    
            }
            
           
            var dbdiamondslist  = dbdiamonds.Skip((mappedSearchParameters.Page-1)*mappedSearchParameters.ItemsPerPage).Take(mappedSearchParameters.ItemsPerPage).ToList();

            var diamonds = mapper.Map<IList<v_jd_diamonds>, List<Diamond>>(dbdiamondslist);

            return diamonds;
        }

        public Diamond GetDiamondByID(int diamondID)
        {
            var diamond = dbmock.Where(d => d.diamondid == diamondID).SingleOrDefault();
            if (diamond != null)
            {
                return mapper.Map<v_jd_diamonds, Diamond>(diamond);
            }
            else
            {
                throw new Exception("Diamond not found is was " + diamondID.ToString());
            }
        }

        public int LastOporationTotalPages
        {
            get { return lastOporationTotalPages; }
        }

        public int TotalRecords
        {
            get { return totalRecords; }
        }

        

        private bool MakeEmptyListActLikeAnAllFilter(string value,List<string> filter )
        {
            if (filter == null)
            {
                return true;
            }
            if (filter.Count == 0)
            {
                return true;
            }
            
            if (filter.Contains(value))
            {
                return true;
            }

            return false;

        }

        private bool MakeZeroActAsIgnoreParameter(Func<v_jd_diamonds,bool> func ,v_jd_diamonds value,decimal filter )
        {
            if (filter == 0)
            {
                return true;
            }
            if (func.Invoke(value))
            {
                return true;
            }

            return false;

        }

        private readonly IQueryable<v_jd_diamonds> dbmock = new List<v_jd_diamonds>()
                                                          {
                                                              new v_jd_diamonds()
                                                                  {
                                                                      diamondid = 1,
                                                                      clarity = "VS1",
                                                                      totalprice = 25478,
                                                                      weight = (decimal) 1.25,
                                                                      color = "H",
                                                                      inventory_code = "1",
                                                                      height = (decimal) 5.36,
                                                                      length = (decimal) 4.25,
                                                                      percaratprice = 0,
                                                                      polish = "VG",
                                                                      reportimg = "",
                                                                      culet = "",
                                                                      report = "GIA",
                                                                      shape = "Round",
                                                                      crown = 0,
                                                                      symmetrical = "VG",
                                                                      table = (decimal) 25.8,
                                                                      width = (decimal) 4.87,
                                                                      flourescence = "VG",
                                                                      depth = (decimal) 58.2,
                                                                      grindle = "",
                                                                      cut = "VG",
                                                                      id = 1
                                                                  },
                                                              new v_jd_diamonds()
                                                                  {
                                                                      diamondid = 2,
                                                                      clarity = "VVS1",
                                                                      totalprice = 58796,
                                                                      weight = (decimal) 1.27,
                                                                      color = "F",
                                                                      inventory_code ="2",
                                                                      height = (decimal) 5.36,
                                                                      length = (decimal) 4.25,
                                                                      percaratprice = 0,
                                                                      polish = "VG",
                                                                      reportimg = "",
                                                                      culet = "",
                                                                      report = "IGI",
                                                                      shape = "Princess",
                                                                      crown = 0,
                                                                      symmetrical = "VG",
                                                                      table = (decimal) 25.8,
                                                                      width = (decimal) 4.87,
                                                                      flourescence = "VG",
                                                                      depth = (decimal) 58.2,
                                                                      grindle = "",
                                                                      cut = "EX",
                                                                      id = 1
                                                                  },
                                                              new v_jd_diamonds()
                                                                  {
                                                                      diamondid = 3,
                                                                      clarity = "VS1",
                                                                      totalprice = 26587,
                                                                      weight = (decimal) 1.35,
                                                                      color = "G",
                                                                      inventory_code = "3",
                                                                      height = (decimal) 5.36,
                                                                      length = (decimal) 4.25,
                                                                      percaratprice = 0,
                                                                      polish = "VG",
                                                                      reportimg = "",
                                                                      culet = "",
                                                                      report = "GIA",
                                                                      shape = "Princess",
                                                                      crown = 0,
                                                                      symmetrical = "VG",
                                                                      table = (decimal) 25.8,
                                                                      width = (decimal) 4.87,
                                                                      flourescence = "VG",
                                                                      depth = (decimal) 58.2,
                                                                      grindle = "",
                                                                      cut = "EX",
                                                                      id = 1
                                                                  },
                                                              new v_jd_diamonds()
                                                                  {
                                                                      diamondid = 4,
                                                                      clarity = "SI2",
                                                                      totalprice = 25478,
                                                                      weight = (decimal) 1.47,
                                                                      color = "M",
                                                                      inventory_code = "4",
                                                                      height = (decimal) 5.36,
                                                                      length = (decimal) 4.25,
                                                                      percaratprice = 0,
                                                                      polish = "VG",
                                                                      reportimg = "",
                                                                      culet = "",
                                                                      report = "GIA",
                                                                      shape = "Pear",
                                                                      crown = 0,
                                                                      symmetrical = "VG",
                                                                      table = (decimal) 25.8,
                                                                      width = (decimal) 4.87,
                                                                      flourescence = "VG",
                                                                      depth = (decimal) 58.2,
                                                                      grindle = "",
                                                                      cut = "G",
                                                                      id = 1
                                                                  },
                                                              new v_jd_diamonds()
                                                                  {
                                                                      diamondid = 5,
                                                                      clarity = "VS2",
                                                                      totalprice = 28471,
                                                                      weight = (decimal) 2.05,
                                                                      color = "G",
                                                                      inventory_code = "5",
                                                                      height = (decimal) 5.36,
                                                                      length = (decimal) 4.25,
                                                                      percaratprice = 0,
                                                                      polish = "VG",
                                                                      reportimg = "",
                                                                      culet = "",
                                                                      report = "GIA",
                                                                      shape = "Princess",
                                                                      crown = 0,
                                                                      symmetrical = "VG",
                                                                      table = (decimal) 25.8,
                                                                      width = (decimal) 4.87,
                                                                      flourescence = "VG",
                                                                      depth = (decimal) 58.2,
                                                                      grindle = "",
                                                                      cut = "VG",
                                                                      id = 1
                                                                  },
                                                              new v_jd_diamonds()
                                                                  {
                                                                      diamondid = 6,
                                                                      clarity = "VVS2",
                                                                      totalprice = 12547,
                                                                      weight = (decimal) 1.85,
                                                                      color = "H",
                                                                      inventory_code = "6",
                                                                      height = (decimal) 5.36,
                                                                      length = (decimal) 4.25,
                                                                      percaratprice = 0,
                                                                      polish = "VG",
                                                                      reportimg = "",
                                                                      culet = "",
                                                                      report = "GIA",
                                                                      shape = "Round",
                                                                      crown = 0,
                                                                      symmetrical = "VG",
                                                                      table = (decimal) 25.8,
                                                                      width = (decimal) 4.87,
                                                                      flourescence = "VG",
                                                                      depth = (decimal) 58.2,
                                                                      grindle = "",
                                                                      cut = "G",
                                                                      id = 1
                                                                  }
                                                          }.AsQueryable();

        private int lastOporationTotalPages;
        private int totalRecords;
    }


}