using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JONMVC.Website.Models.Jewelry;
using NMoneys;

namespace JONMVC.Website.Tests.Unit
{
    public static class Tests
    {
        public static JewelMediaType MEDIA_TYPE_IS_NOT_IMPORANT = JewelMediaType.WhiteGold;
        public static int FIRST_ITEM_IN_A_ZERO_BASED_LIST = 0;
        public static int NUMBER_HAS_NO_MEANING_IN_THIS_CONTEXT = 1;
        public static int NUMBER_THAT_IS_ASSERTED_WITH_BUT_HAS_NO_MEANING  = 1111;
        public static string STRING_THAT_IS_ASSERTED_BUT_HAS_NO_MEANING = "foo";
        public static string STRING_THAT_HAS_NO_MEANING_IN_THIS_CONTEXT = "foo";

        public static int FAKE_JEWELRY_REPOSITORY_FIRST_ITEM_ID = 1111;
        public static int FAKE_JEWELRY_WITH_ALL_NON_DEFAULT_BEHAVIER = 1112;
        public static int FAKE_GEMSTONE_JEWELRY = 1113;

        public static int FAKE_DIAMOND_REPOSITORY_FIRST_ITEM_ID = 1;

        public static string SAMPLE_EMAIL_ADDRESS = "email@google.com";
        public static string SAMPLE_PASSWORD = "12345";

        public static string FAKE_JEWEL_ITEMNUMBER = "0101-15421";
        public static string FAKE_JEWEL_ICON_WG =   "/jon-images/jewel/0101-15421-icon-wg.jpg";

        public static int BAD_FAKE_JEWELRY_ID = 9999;
        public static int FAKE_ORDERNUMBER=111;
        public static string SAMPLE_COUNTRY="Israel";
        public static string SAMPLE_STATE="New York";
        public static string SAMPLE_JEWEL_SIZE_725="7.25";
        


        public static string AsMoney(decimal totalPrice)
        {
            return new Money(totalPrice, Currency.Usd).Format("{1}{0:#,0}");
        }

        public static string AsDecimalPrecent(decimal value)
        {
            return String.Format("{0:0.00}%",value);
        }

        public static string AsDecimalPrecentRounded(decimal value)
        {
            return String.Format("{0:0.##}%", value);
        }

        public static string AsDecimal(decimal value)
        {
            return String.Format("{0:0.00}", value);
        }

       
    }
    
}
