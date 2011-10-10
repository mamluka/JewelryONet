using System.Collections.Generic;
using JONMVC.Website.ViewModels.Json.Builders;

namespace JONMVC.Website.ViewModels.Json.Views
{
    public class DiamondsJsonModel
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<DiamondGridRow> rows { get; set; }
        public Dictionary<string, DiamondJsonUserData> userdata { get; set; }
    }
}