using System;
using System.Collections.Generic;
using JONMVC.Website.Models.Helpers;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Tabs
{
    public class CustomTabFilterEnumMetadataReader<T> 
    {
        public List<KeyValuePair<string, int>> Values()
        {
            var list = new List<KeyValuePair<string, int>>();
            var type = typeof (T);

            foreach (var enumName in Enum.GetNames(typeof(T)))
            {
                var enumValue =  (int)Enum.Parse(typeof (T), enumName);
                var memInfo = type.GetMember(enumName);


                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description),
                                                                false);

                if (attrs.Length > 0)

                    list.Add(new KeyValuePair<string, int>(((Description)attrs[0]).Text, enumValue)); 
            
            }

            

            return list;

        }

        public DynamicSQLWhereObject ReadDynamicSQLByValue(int filterValue)
        {
            var type = typeof (T);
            var enumName = Enum.GetName(type, filterValue);

            var memInfo = type.GetMember(enumName);
            var filterAttrs = memInfo[0].GetCustomAttributes(typeof (FilterFieldAndValue), false);

            if (filterAttrs.Length > 0 )
            {
                var filterAttr = (FilterFieldAndValue)filterAttrs[0];

                return new DynamicSQLWhereObject(filterAttr.Field + " = @0",filterAttr.Value);
            }

            var dynamic = new DynamicSQLWhereObject();
            dynamic.DoNothing();
            return dynamic;
        }
    }
}