using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Tests.Unit.Utils
{
    class FakeSettingManager:ISettingManager
    {

        public string GetJewelryBaseWebPath()
        {
            return @"/jon-images/jewel/";
        }

        public string GetJewelryBaseDiskPath()
        {
            return @"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\";
        }

        public string GetDiamondBaseWebPath()
        {
            return @"/jon-images/diamond/";
        }

        public string AdminEmail()
        {
            return "david.mazvovsky@gmail.com";
        }
    }
}
