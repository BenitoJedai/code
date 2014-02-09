using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCheckBoxColumn))]
    internal class __DataGridViewCheckBoxColumn : __DataGridViewColumn
    {
        public object FalseValue { get; set; }
        public object TrueValue { get; set; }
    }
}
