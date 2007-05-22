using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TextBoxBase))]
    internal class __TextBoxBase : __Control
    {
        public bool Multiline { get; set; }

    }
}
