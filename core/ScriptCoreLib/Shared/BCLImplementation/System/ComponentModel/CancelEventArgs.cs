using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.CancelEventArgs))]
    public class __CancelEventArgs : __EventArgs
    {
        public bool Cancel { get; set; }

        public __CancelEventArgs()
        {

        }

        public __CancelEventArgs(bool cancel)
        {
            this.Cancel = cancel;
        }
    }
}
