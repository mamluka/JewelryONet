using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JONMVC.Website.Models.DB;

namespace JONMVC.Website.ViewModels.Views
{
    public static class SelectSourceFactory
    {
        
   


        public static IEnumerable<KeyValuePair<string, string>> CraditCards
        {
            get
            {
                using (var db = new JONEntities())
                {
                    var cardtypes = from s in db.acc_CREDITCARD orderby s.ID ascending select s;
                    return cardtypes.ToList().Select(x => new KeyValuePair<string, string>(x.LANG1_LONGDESCR, x.ID.ToString()));
                }
            }
        }

        public static IEnumerable<KeyValuePair<string, string>> Countries
        {
            get
            {
                using (var db = new JONEntities())
                {
                    var countries = from s in db.sys_COUNTRY orderby s.LANG1_LONGDESCR ascending select s;
                    return countries.ToList().Select(x => new KeyValuePair<string, string>(x.LANG1_LONGDESCR, x.ID.ToString()));
                }
            }
        }

        public static IEnumerable<KeyValuePair<string, string>> States
        {
            get
            {
                using (var db = new JONEntities())
                {
                    var states = from s in db.sys_STATE orderby s.LANG1_LONGDESCR ascending select s;
                    return states.ToList().Select(x => new KeyValuePair<string, string>(x.LANG1_LONGDESCR, x.ID.ToString()));
                }
            }
        }
        public static IEnumerable<KeyValuePair<string, string>> RingSizes
        {
            get { return ringSizes; }
        }

        public static List<KeyValuePair<string, string>> CcYears
        {
            get { return ccYears; }
        }

        public static List<KeyValuePair<string, string>> CcMonths
        {
            get { return ccMonths; }
        }

        private static readonly List<KeyValuePair<string, string>> ccYears = new List<KeyValuePair<string, string>>()
                                                                                   {
                                                                                       new KeyValuePair<string, string>("2011","2011"),
                                                                                       new KeyValuePair<string, string>("2012","2012"),
                                                                                       new KeyValuePair<string, string>("2013","2013"),
                                                                                       new KeyValuePair<string, string>("2014","2014"),
                                                                                       new KeyValuePair<string, string>("2015","2015"),
                                                                                       new KeyValuePair<string, string>("2016","2016"),
                                                                                       new KeyValuePair<string, string>("2017","2017"),
                                                                                   };

        private static readonly List<KeyValuePair<string, string>> ccMonths = new List<KeyValuePair<string, string>>()
                                                                                   {
                                                                                       new KeyValuePair<string, string>("1","1"),
                                                                                       new KeyValuePair<string, string>("2","2"),
                                                                                       new KeyValuePair<string, string>("3","3"),
                                                                                       new KeyValuePair<string, string>("4","4"),
                                                                                       new KeyValuePair<string, string>("5","5"),
                                                                                       new KeyValuePair<string, string>("6","6"),
                                                                                       new KeyValuePair<string, string>("7","7"),
                                                                                       new KeyValuePair<string, string>("8","8"),
                                                                                       new KeyValuePair<string, string>("9","9"),
                                                                                       new KeyValuePair<string, string>("10","10"),
                                                                                       new KeyValuePair<string, string>("11","11"),
                                                                                       new KeyValuePair<string, string>("12","12"),
                                                                                       
                                                                                   };

        private static readonly List<KeyValuePair<string, string>> ringSizes = new List<KeyValuePair<string, string>>()
                                                             {
                                                                new KeyValuePair<string, string>("3","3"),
                                                                new KeyValuePair<string, string>("3.25","3.25"),
                                                                new KeyValuePair<string, string>("3.5","3.5"),
                                                                new KeyValuePair<string, string>("3.75","3.75"),
                                                                new KeyValuePair<string, string>("4","4"),
                                                                new KeyValuePair<string, string>("4.25","4.25"),
                                                                new KeyValuePair<string, string>("4.5","4.5"),
                                                                new KeyValuePair<string, string>("4.75","4.75"),
                                                                new KeyValuePair<string, string>("5","5"),
                                                                new KeyValuePair<string, string>("5.25","5.25"),
                                                                new KeyValuePair<string, string>("5.5","5.5"),
                                                                new KeyValuePair<string, string>("5.75","5.75"),
                                                                new KeyValuePair<string, string>("6","6"),
                                                                new KeyValuePair<string, string>("6.25","6.25"),
                                                                new KeyValuePair<string, string>("6.75","6.75"),
                                                                new KeyValuePair<string, string>("7","7"),
                                                                new KeyValuePair<string, string>("7.25","7.25"),
                                                                new KeyValuePair<string, string>("7.5","7.5"),
                                                                new KeyValuePair<string, string>("7.75","7.75"),
                                                                new KeyValuePair<string, string>("8","8"),
                                                                new KeyValuePair<string, string>("8.25","8.25"),
                                                                new KeyValuePair<string, string>("8.5","8.5"),
                                                                new KeyValuePair<string, string>("8.75","8.75"),
                                                                new KeyValuePair<string, string>("9","9"),
                                                                new KeyValuePair<string, string>("9.25","9.25"),
                                                                new KeyValuePair<string, string>("9.5","9.5"),
                                                                new KeyValuePair<string, string>("9.75","9.75"),
                                                                new KeyValuePair<string, string>("10","10"),
                                                                new KeyValuePair<string, string>("10.25","10.25"),
                                                                new KeyValuePair<string, string>("10.5","10.5"),
                                                                new KeyValuePair<string, string>("10.75","10.75"),
                                                                new KeyValuePair<string, string>("11","11"),
                                                                new KeyValuePair<string, string>("11.25","11.25"),
                                                                new KeyValuePair<string, string>("11.5","11.5"),
                                                                new KeyValuePair<string, string>("11.75","11.75")
                                                             };

        
    }
}