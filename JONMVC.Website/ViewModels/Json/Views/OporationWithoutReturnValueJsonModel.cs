using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JONMVC.Website.ViewModels.Json.Views
{
    public class OporationWithoutReturnValueJsonModel:JsonModelBase
    {
        public OporationWithoutReturnValueJsonModel()
        {
            this.HasError = false;
        }

        public OporationWithoutReturnValueJsonModel(bool hasErorr,string errorMessage)
        {
            this.HasError = hasErorr;
            this.ErrorMessage = errorMessage;
        }
    }
}