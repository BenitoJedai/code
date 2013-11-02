using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellValidatingEventHandler))]
    internal delegate void __DataGridViewCellValidatingEventHandler(object sender, DataGridViewCellValidatingEventArgs e);
}
