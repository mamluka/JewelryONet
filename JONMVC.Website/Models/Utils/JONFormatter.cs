using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.Models.Utils
{
    public class JONFormatter : IJONFormatter
    {
        public string ToCaratWeight(double weight)
        {
            return FormatTwoDecimalPoints(weight, "Ct.");
        }

        public string ToCaratWeight(decimal weight)
        {
            return FormatTwoDecimalPoints((double) weight, "Ct.");
        }

        public string ToGramWeight(double weight)
        {
            return FormatTwoDecimalPoints(weight, "gr.");
        }
        public string ToGramWeight(decimal weight)
        {
            return FormatTwoDecimalPoints((double) weight, "gr.");
        }
        public string ToMilimeter(double length)
        {
            return FormatTwoDecimalPoints(length, "mm.");
        }
        public string ToMilimeter(decimal length)
        {
            return FormatTwoDecimalPoints((double) length, "mm.");
        }

        public string FormatTwoDecimalPoints(double number,string ext)
        {
            var result = String.Format("{0:N2}", number);
            if (!String.IsNullOrEmpty(ext))
            {
                result += " " + ext;
            }

            return result;

        }

        public string FormatTwoDecimalPoints(decimal number, string ext)
        {
            var result = String.Format("{0:N2}", number);
            if (!String.IsNullOrEmpty(ext))
            {
                result += " " + ext;
            }

            return result;

        }

        public string FormatTwoDecimalPoints(double number)
        {
            var result = String.Format("{0:N2}", number);


            return result;

        }

        public string FormatTwoDecimalPoints(decimal number)
        {
            var result = String.Format("{0:N2}", number);


            return result;

        }


    }
}