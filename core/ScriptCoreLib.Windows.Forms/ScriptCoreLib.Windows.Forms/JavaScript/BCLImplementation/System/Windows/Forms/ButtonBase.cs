using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ButtonBase ))]
    internal class __ButtonBase : __Control
    {
        public bool UseVisualStyleBackColor { get; set;  }

    }
}
