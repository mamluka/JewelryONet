using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JONMVC.Website.Models.Tabs
{
    public class Tab
    {
        public string Caption { get; private set; }
        public string Id { get; private set; }

        public bool Active {get; private set; }

        public int TabIndex { get; private set; }

        public Tab(string caption, string id, int tabIndex)
        {
            Caption = caption;
            Id = id;
            TabIndex = tabIndex;
        }

        public void ActivateTab()
        {
            Active = true;
        }

    }
}
