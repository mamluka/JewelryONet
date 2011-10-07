using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JONMVC.Website.Models.Diamonds;
using Ninject.Modules;

namespace JONMVC.Website.Models.Ninject
{
    public class StandardModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IDiamondRepository>().To<DiamondRepository>();
        }
    }
}