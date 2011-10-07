using System.Collections.Generic;
using JONMVC.Website.Models.Utils;

namespace JONMVC.Website.Models.Helpers
{
    public abstract class PathBarResolver<TViewModel>
    {
        protected IWebHelpers webHelpers;

        public IWebHelpers WebHelpers
        {
            set { webHelpers = value; }
        }


        public abstract List<KeyValuePair<string, string>> GeneratePathBarDictionary(TViewModel model);
    }
}