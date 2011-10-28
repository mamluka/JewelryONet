using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using DataAnnotationsExtensions;

namespace JONMVC.Website.ViewModels.Views
{
    public class SigninViewModel:PageViewModelBase
    {
        [Required]
        [Email]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string RememberMe { get; set; }
        public bool HasError { get; set; }
        public string ReturnURL { get; set; }
        public RedirectMode RedirectMode { get; set; }
        public string RouteAction { get; set; }
        public string RouteController { get; set; }
        public string JSONEncodedRouteValues { get; set; }
        public string RouteValuesModelClassName { get; set; }
    }

    public enum RedirectMode
    {
        Route,
        Link
    }
}