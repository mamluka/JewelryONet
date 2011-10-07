using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JONMVC.Website.Models.Tabs
{
    public class TabPage
    {
        public string Title { get; private set; }
        public string Sprite { get; private set; }
        public string TabKey { get; private set; }

        public TabPage(string title, string sprite, string tabKey)
        {
            Title = title;
            Sprite = sprite;
            TabKey = tabKey;
        }
    }
}
