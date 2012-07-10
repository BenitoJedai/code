using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements=typeof(TabControlEventArgs))]
    public class __TabControlEventArgs : EventArgs
    {
        // fields
        public TabControlAction action;
        public TabPage tabPage;
        public int tabPageIndex;

        // properties
        public TabControlAction Action { get { return Action; } }

        public TabPage TabPage { get { return TabPage; } }

        public int TabPageIndex { get { return TabPageIndex; } }


        // Methods
        public __TabControlEventArgs(TabPage tabPage, int tabPageIndex, TabControlAction action)
        {
            this.tabPage = tabPage;
            this.tabPageIndex = tabPageIndex;
            this.action = action;
        }
  
    }

 

}
