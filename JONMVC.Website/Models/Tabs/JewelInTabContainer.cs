using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JONMVC.Website.Models.Jewelry;

namespace JONMVC.Website.Models.Tabs
{
    public class JewelInTabContainer
    {
        public string Price { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public int ID { get; set; }
        public string Movie { get; set; }
        public string Metal { get; set; }
        public bool HasMovie { get; set; }

        public JewelMediaType MediaSet { get; set; }

        public bool OnSpecial { get; set; }

        public string RegularPrice { get; set; }

        public string YouSave { get; set; }

    }
}
