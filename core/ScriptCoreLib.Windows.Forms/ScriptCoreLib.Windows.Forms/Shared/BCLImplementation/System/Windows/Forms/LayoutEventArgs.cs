using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(LayoutEventArgs))]
    public class __LayoutEventArgs : EventArgs
    {
        public __LayoutEventArgs(Control affectedControl, string affectedProperty)
        {
            this.AffectedControl = affectedControl;
        }

        public Control AffectedControl { get; set; }
    }



}
