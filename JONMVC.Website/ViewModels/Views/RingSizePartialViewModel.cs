using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using JONMVC.Website.Models.Jewelry;
using MvcContrib.FluentHtml.Expressions;

namespace JONMVC.Website.ViewModels.Views
{
    public class RingSizePartialViewModel
    {
        public string Selected { get; set; }
        public JewelType JewelType { get; set; }

        public string ID { get; set; }

        public string SelectName { get; set; }
        public string Class { get; set; }

        public Dictionary<string, object> Attributes { get; set; }


        public RingSizePartialViewModel()
        {
            SelectName = "Size";
        }

        public RingSizePartialViewModel For<TModel>(Expression<Func<TModel, object>> expression) where TModel : class
        {
            try
            {
                SelectName =  expression.GetNameFor();
                return this;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "When asked to parse the For method in the ring size partial the member was not a property\r\n"+ ex.Message);
                
            }
            
        }
    }

}