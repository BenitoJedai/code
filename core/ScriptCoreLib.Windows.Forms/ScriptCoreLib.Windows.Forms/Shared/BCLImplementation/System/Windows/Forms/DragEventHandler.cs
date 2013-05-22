using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.DragEventHandler))]
    internal delegate void __DragEventHandler(object sender, DragEventArgs e);
}
