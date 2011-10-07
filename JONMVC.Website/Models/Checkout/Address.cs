using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JONMVC.Website.Models.Checkout
{
    public class Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string Country{ get; set; }
        public string State { get; set; }
        public int StateID { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
    }
}
