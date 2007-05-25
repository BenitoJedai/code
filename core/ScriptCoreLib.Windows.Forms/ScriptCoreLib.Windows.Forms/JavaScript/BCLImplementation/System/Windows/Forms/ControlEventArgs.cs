using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements=typeof(ControlEventArgs))]
    public class __ControlEventArgs : EventArgs
    {
        // Fields
        private Control control;

        // Methods
        public __ControlEventArgs(Control control)
        {
            this.control = control;
        }

        // Properties
        public Control Control { get { return control; } }
    }

 

}
