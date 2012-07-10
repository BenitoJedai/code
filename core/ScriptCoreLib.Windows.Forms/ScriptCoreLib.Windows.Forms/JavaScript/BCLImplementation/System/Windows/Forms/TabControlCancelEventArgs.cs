using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements=typeof(TabControlCancelEventArgs))]
    public class __TabControlCancelEventArgs : CancelEventArgs
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
        public __TabControlCancelEventArgs(TabPage tabPage, int tabPageIndex, bool cancel, TabControlAction action)
        {
            this.tabPage = tabPage;
            this.tabPageIndex = tabPageIndex;
            this.action = action;
        }
  
    }

 

}
