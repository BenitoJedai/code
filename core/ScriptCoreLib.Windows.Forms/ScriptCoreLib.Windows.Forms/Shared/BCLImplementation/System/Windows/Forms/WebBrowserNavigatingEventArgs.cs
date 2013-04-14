using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.WebBrowserNavigatingEventArgs))]
    internal class __WebBrowserNavigatingEventArgs : EventArgs
    {
        public Uri Url { get; set; }
    }
}
