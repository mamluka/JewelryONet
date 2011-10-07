using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.ViewModels.Json.Views
{
    public class JsonModelBase
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}