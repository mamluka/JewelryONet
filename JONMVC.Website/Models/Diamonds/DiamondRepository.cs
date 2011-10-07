using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using JONMVC.Website.Models.DB;
using JONMVC.Website.Extensions;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Diamonds
{
    public class 
        DiamondRepository : IDiamondRepository
    {
        private readonly IMappingEngine mapper;
        private int lastOporationTotalPages;
        private int totalRecords;

        public int LastOporationTotalPages
        {
            get { return lastOporationTotalPages; }
        }

        public int TotalRecords
        {
            get { return totalRecords; }
        }

       

        public DiamondRepository(IMappingEngine mapper)
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

            using (var db = new JONEntities())
            {
                var dbdiamonds = db.v_jd_diamonds
                .ExtWhereIn(cond => cond.color, mappedSearchParameters.Color)
                .ExtWhereIn(cond => cond.clarity, mappedSearchParameters.Clarity)
                .ExtWhereIn(cond => cond.shape, mappedSearchParameters.Shape)
                .ExtWhereIn(cond => cond.report, mappedSearchParameters.Report)
                .ExtWhereIn(cond => cond.cut, mappedSearchParameters.Cut)
                .ExtWhereFromToRangeAndIgnoreZero(x => x.totalprice,mappedSearchParameters.PriceFrom,mappedSearchParameters.PriceTo)
                .ExtWhereFromToRangeAndIgnoreZero(x => x.weight, mappedSearchParameters.WeightFrom, mappedSearchParameters.WeightTo)
                ;



                if (mappedSearchParameters.ItemsPerPage == 0)
                {
                    mappedSearchParameters.ItemsPerPage = 10;
                }

                totalRecords = dbdiamonds.Count();
                if (TotalRecords % mappedSearchParameters.ItemsPerPage == 0)
                {
                    lastOporationTotalPages = TotalRecords / mappedSearchParameters.ItemsPerPage;
                }
                else
                {
                    lastOporationTotalPages = TotalRecords / mappedSearchParameters.ItemsPerPage + 1;
                }

            
            var dbdiamondslist = dbdiamonds.OrderBy(s=> s.inventory_code).Skip((mappedSearchParameters.Page - 1) * mappedSearchParameters.ItemsPerPage).Take(mappedSearchParameters.ItemsPerPage).ToList();

            var diamonds = mapper.Map<IList<v_jd_diamonds>, List<Diamond>>(dbdiamondslist);

            return diamonds;
            
            }

           
            
        }

        public Diamond GetDiamondByID(int diamondID)
        {
            using (var db = new JONEntities())
            {
                var diamond = db.v_jd_diamonds.Where(d => d.diamondid == diamondID).SingleOrDefault();
                if (diamond != null)
                {
                    return mapper.Map<v_jd_diamonds, Diamond>(diamond);
                }
                throw new Exception("Diamond not found is was: " + diamondID.ToString());
            }
            
        }

        private bool MakeEmptyListActLikeAnAllFilter(string value, List<string> filter)
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

        private bool MakeZeroActAsIgnoreParameter(Func<v_jd_diamonds, bool> func, v_jd_diamonds value, decimal filter)
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
    }
}