using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.WebBrowserNavigatedEventArgs))]
    internal class __WebBrowserNavigatedEventArgs : EventArgs
    {
        public Uri Url { get; set; }
    }
}
