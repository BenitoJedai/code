using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.WebBrowserNavigatedEventHandler))]
    internal delegate void __WebBrowserNavigatedEventHandler(object sender, WebBrowserNavigatedEventArgs e);

}
