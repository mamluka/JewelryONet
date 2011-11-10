using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.Helpers
{
    public class DynamicSQLWhereObject
    {
        public string Pattern { get; private set; }
        public List<object> Valuelist { get; private set; }

        public DynamicSQLWhereObject(string pattern, List<object> valuelist)
        {
            Pattern = pattern;
            Valuelist = valuelist;
        }

        public DynamicSQLWhereObject(string pattern)
        {
            Pattern = pattern;
            Valuelist = new List<object>();
        }

        public DynamicSQLWhereObject(string pattern,object single)
        {
            Pattern = pattern;
            Valuelist = new List<object>() { single };
        }

      


        public override bool Equals(object obj)
        {
            var other = (DynamicSQLWhereObject) obj;

            if (other.Pattern != this.Pattern)
            {
                return false;
            }

            foreach (var value in Valuelist)
            {
                if (!other.Valuelist.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Pattern.GetHashCode();
        }
    }
}