using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements=typeof(CancelEventArgs))]
    public class __CancelEventArgs : EventArgs
    {
        // fields
        public bool cancel;

        // properties
        public bool Cancel 
        { 
            get{return cancel; }
            set { cancel = value;; }
        }

        // Methods
        public __CancelEventArgs()
        {
        }

        public __CancelEventArgs(bool cancel)
        {
            this.cancel = cancel;
        }

    }

 

}
