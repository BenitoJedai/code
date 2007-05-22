using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ListControl))]
    internal class __ListControl : __Control
    {
        public bool FormattingEnabled { get; set; }
    }
}
