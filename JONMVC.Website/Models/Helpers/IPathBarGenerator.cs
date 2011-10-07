using System.Collections.Generic;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.Helpers
{
    public interface IPathBarGenerator
    {
        List<KeyValuePair<string, string>> GenerateUsing<TResolver, TViewModel>(TViewModel model) where TResolver : PathBarResolver<TViewModel>, new();

        List<KeyValuePair<string, string>> GenerateUsingSingleTitle<TResolver>(string title)
            where TResolver : PathBarResolver<PageViewModelBase>, new();
    }
}