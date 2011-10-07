using System.Collections.Generic;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Helpers
{
    public class PathBarGenerator : IPathBarGenerator
    {
        private readonly IWebHelpers webHelpers;

        public PathBarGenerator(IWebHelpers webHelpers)
        {
            this.webHelpers = webHelpers;
        }

        public List<KeyValuePair<string, string>> GenerateUsing<TResolver, TViewModel>(TViewModel model) where TResolver : PathBarResolver<TViewModel>, new()
        {
            var resolver = new TResolver();
            resolver.WebHelpers = webHelpers;
            return resolver.GeneratePathBarDictionary(model);
        }

        public List<KeyValuePair<string, string>> GenerateUsingSingleTitle<TResolver>(string title) where TResolver : PathBarResolver<PageViewModelBase>,new()
        {
            var viewModel = new PageViewModelBase() {PageTitle = title};
            var resolver = new TResolver();
            return resolver.GeneratePathBarDictionary(viewModel);
        }

    }
}