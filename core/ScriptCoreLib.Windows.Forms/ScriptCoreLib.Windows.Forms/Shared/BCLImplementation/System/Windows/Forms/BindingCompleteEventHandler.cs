using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.BindingCompleteEventHandler))]
    internal delegate void __BindingCompleteEventHandler(object sender, BindingCompleteEventArgs e);
}
